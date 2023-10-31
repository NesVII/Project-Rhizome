using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public int attackDamage = 1;

    public Vector3 attackOffset;
    public float attackRange = 3f;
    public LayerMask attackMask;
    Vector3 pos;
    public void Attack()
    {
        pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
        if (colInfo != null)
        {
            colInfo.GetComponent<Health>().TakeDamage(attackDamage);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        //Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.DrawWireSphere(transform.position, attackOffset.x);
    }

}
