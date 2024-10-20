using UnityEngine;
using UnityEngine.SceneManagement; 

public class PLayerHealth : MonoBehaviour
{
    public int health = 3; 
    public HeartManager heartManager; // Referencia ao HeartManager

    private void Start()
    {
        heartManager.SetHealth(health); // Inicializa os corações
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        heartManager.SetHealth(health); // Atualiza os corações
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player morreu!");
       
          GameOver();
    }

    private void GameOver()
    {
       
        SceneManager.LoadScene("GameOver"); 
    }
}
