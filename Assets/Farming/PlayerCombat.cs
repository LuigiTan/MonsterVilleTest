using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using System.Collections;
public class PlayerCombat : MonoBehaviour
{
    public Transform attackOrigin;
    public float attackRange = 1.5f;
    public bool isAttacking = false;
    public float playerDamage = 25.0f;
    public float attackCooldown = 0.5f;
    public void OnShockwave(InputValue value)
    {
        Debug.Log("Se presiono el boton de atacar");
        if (!isAttacking)
        {
            isAttacking = true;
            Collider[] hits = Physics.OverlapSphere(attackOrigin.position, attackRange);

            foreach (var hit in hits)
            {
                if (hit.TryGetComponent(out BasicScav ScavInfo))
                {
                    ScavInfo.GetHit(playerDamage);
                }
            }
            StartCoroutine(AttackCooldown());
        }
        else
        {
            Debug.Log("Attack on cooldown!");
        }
        
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackOrigin.position, attackRange);
    }
    private IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        isAttacking = false;
    }
}
