using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    private MaiActions actions;
    public float speed = 2;

    public LayerMask groundMask;//  "Ground"
    public LayerMask enemyMask;//  "Enemy"

    void Start()
    {
        gameObject.TryGetComponent(out controller);
        actions = new MaiActions();

        actions.Gameplay.Enable();
    }

    public void Update()
    {
        Vector2 input = actions.Gameplay.Move.ReadValue<Vector2>();

        controller.Move(new Vector3(input.x, 0, input.y) * Time.deltaTime * speed);

        RotateTowardsMouse();
    }

    private void RotateTowardsMouse()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mousePos);

        // Primero checa hit contra enemigos
        if (Physics.Raycast(ray, out RaycastHit hitEnemy, 100f, enemyMask))
        {
            Vector3 direction = hitEnemy.collider.transform.position - transform.position;
            direction.y = 0;

            if (direction.sqrMagnitude > 0.001f) // seguridad
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction.normalized, Vector3.up);
                transform.rotation = targetRotation;
            }
            return; // ya rotamos, no hace falta revisar el suelo
        }

        // Luego, si no hay enemigos, usar el piso
        if (Physics.Raycast(ray, out RaycastHit hitGround, 100f, groundMask))
        {
            Vector3 direction = hitGround.point - transform.position;
            direction.y = 0;

            if (direction.sqrMagnitude > 0.001f) // seguridad
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction.normalized, Vector3.up);
                transform.rotation = targetRotation;
            }
        }
    }
}
//El if antes de hacer la rotacion es por esto:
//Si el objetivo esta muy cerca del jugador, el vector direction puede ser casi (0,0,0).
//Si se intenta hacer la linea Quaternion.LookRotation(Vector3.zero) va a sacar error, en mejor de los casos evita que el personaje tiemble si el cursor queda justo encima
//o demasiado cerca


