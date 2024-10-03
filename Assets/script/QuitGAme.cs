using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGAme : MonoBehaviour
{
    // Update é chamado a cada frame
    void Update()
    {
        // Verifica se a tecla Esc foi pressionada
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
