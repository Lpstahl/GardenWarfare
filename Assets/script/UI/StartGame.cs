using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    // Nome da cena do jogo a ser carregada
    public string gameSceneName = "GameScene"; // Troque por sua cena de jogo

    // Update é chamado a cada frame
    void Update()
    {
        // Verifica se qualquer tecla foi pressionada
        if (Input.anyKeyDown)
        {
            // Carrega a cena do jogo
            SceneManager.LoadScene(gameSceneName);
        }
    }
}
