using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int lives = 10; // Define o número de vidas.

    public int TotalLives { get; set; } // Define o número total de vidas.

    private void Start()
    {
        TotalLives = lives; // Define o número total de vidas.
    }

    private void ReduceLives()
    {
        TotalLives--; // Reduz o número de vidas.

        if (TotalLives <= 0) // Se o número de vidas for menor ou igual a zero,
        {
            Debug.Log("Game Over!"); // Exibe a mensagem de Game Over.
        }
    }

    private void OnEnable()
    {
        Enemy.OnEndReached += ReduceLives; // Adiciona o evento de fim de caminho.
    }

    private void OnDisable()
    {
        Enemy.OnEndReached -= ReduceLives;
    }
}
