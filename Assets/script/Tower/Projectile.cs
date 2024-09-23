using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 1;

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.CompareTag("Enemy"))
        {
            hitInfo.GetComponent<Enemy>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}

