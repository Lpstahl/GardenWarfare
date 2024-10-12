using UnityEngine;
using System.Collections;

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
        StartCoroutine(Attack()); // inicia o ataque
    }

    public void Initialize()
    {
        // Adicione aqui o código de inicialização necessário para a torre
        StartCoroutine(Attack());
    }

    IEnumerator Attack()
    {
        while (true) // tiros infinitos
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, attackRange);
            foreach (var enemy in hitEnemies)
            {
                if (enemy.CompareTag("Enemy"))
                {
                    Shoot(enemy.transform);
                    break;
                }
            }
            yield return new WaitForSeconds(attackInterval);
        }
    }

    private void OnDrawGizmosSelected()
    {
        // alcance de disparo
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    void Shoot(Transform target)
    {
        // projetil dispara os tiros da torre
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        Vector2 direction = (target.position - firePoint.position).normalized;
        rb.velocity = direction * projectileSpeed;
    }
}
