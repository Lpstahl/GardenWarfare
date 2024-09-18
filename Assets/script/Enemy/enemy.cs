using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 2f;
    public int health = 3;

    private void Update()
    {
        if (gameObject == null) return;  

        transform.Translate(Vector2.left * speed * Time.deltaTime);
        
        if (transform.position.x < -10) 
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        if (gameObject == null) return; 
        
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
