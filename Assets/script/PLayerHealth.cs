using UnityEngine;

public class PLayerHealth : MonoBehaviour
{
    public int health = 3; // Número de corações
    public HeartManager heartManager; // Referência ao HeartManager

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
        // Adicione aqui a lógica para a morte do jogador, como reiniciar o jogo ou mostrar uma tela de game over.
    }
}
