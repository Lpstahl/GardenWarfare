using System.Collections;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public float attackRange = 5f;
    public float attackInterval = 1f;
    public int damage = 1;
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float projectileSpeed = 10f;

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
                    Shoot(enemy.transform);  // Chama o método Shoot
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

    void Shoot(Transform target)
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        Vector2 direction = (target.position - firePoint.position).normalized;
        rb.velocity = direction * projectileSpeed;
    }
}
