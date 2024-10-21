using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int lives = 10; // Define o número de vidas.

    [SerializeField] private GameObject gameOverUI;  // Referência à tela de Game Over.

    public int TotalLives { get; set; } // Define o número total de vidas.
    public int CurrentWave { get; set; } // Define a wave atual.

    private void Start()
    {
        TotalLives = lives; // Define o número total de vidas.
    }


    private void ReduceLives()
    {
        TotalLives--; // Reduz o número de vidas.

        if (TotalLives <= 0) // Se o número de vidas for menor ou igual a zero,
        {
            gameOverUI.SetActive(true);  // Ativa a tela de Game Over.
                                         // Pause o jogo, se necessário:
            Time.timeScale = 0f;  // Pausa o tempo do jogo.
        }
    }

    private void OnEnable()
    {
        Enemy.OnEndReached += ReduceLives; // Adiciona o evento de fim de caminho.
    }

    private void OnDisable()
    {
        Enemy.OnEndReached -= ReduceLives; // Remove o evento de fim de caminho.
    }
}
