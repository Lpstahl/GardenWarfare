using System.Collections;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public float attackRange = 5f;
    public float attackInterval = 1f;
    public int damage = 1;

    private void Start()
    {
        StartCoroutine(Attack());
    }

    IEnumerator Attack()
    {
        while (true)
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, attackRange);
            foreach (var enemy in hitEnemies)
            {
                if (enemy.CompareTag("Enemy"))  
                {
                    enemy.GetComponent<Enemy>().TakeDamage(damage);
                    break;  
                }
            }
            yield return new WaitForSeconds(attackInterval);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
