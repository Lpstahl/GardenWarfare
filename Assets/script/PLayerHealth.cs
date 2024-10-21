using UnityEngine;
<<<<<<< HEAD
using UnityEngine.SceneManagement; 

public class PLayerHealth : MonoBehaviour
{
    public int health = 3; 
    public HeartManager heartManager; // Referencia ao HeartManager

=======
using UnityEngine.SceneManagement;

public class PLayerHealth : MonoBehaviour
{
    public int health = 3;
    public HeartManager heartManager; // Referencia ao HeartManager

    [SerializeField] private GameObject gameOverUI;  // Referência à tela de Game Over.

>>>>>>> 927fae5a092df0aa69757c981fbfafa85f21c156
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
<<<<<<< HEAD
       
          GameOver();
=======

        GameOver();
>>>>>>> 927fae5a092df0aa69757c981fbfafa85f21c156
    }

    private void GameOver()
    {
<<<<<<< HEAD
       
        SceneManager.LoadScene("GameOver"); 
=======
        gameOverUI.SetActive(true);  // Ativa a tela de Game Over.
        Time.timeScale = 0f;  // Pausa o tempo do jogo.
>>>>>>> 927fae5a092df0aa69757c981fbfafa85f21c156
    }
}
