using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{  
    [SerializeField] private Vector3[] points; // Array de pontos que o objeto irá criar.

    public Vector3[] Points => points; // Propriedade que retorna os pontos.

    public Vector3 CurrentPos => _currentPos; // Propriedade que retorna a posição atual do objeto.

    private Vector3 _currentPos; // Armazena a posição atual do objeto.
    private bool _gameStarted; // Verifica se o jogo foi iniciado.

    void Start()
    {
        _gameStarted = true; // Inicia o jogo.
        _currentPos = transform.position; // Armazena a posição atual do objeto.
    }

    public Vector3 GetwaypointPosition(int index) // Retorna a posição do waypoint.
    {
        return CurrentPos + Points[index];
    }

    private void OnDrawGizmos()
    {   
        if (!_gameStarted && transform.hasChanged) // Se o jogo não começou e a posição do objeto foi alterada,
        {
            _currentPos = transform.position; // atualiza _currentPos para a nova posição do objeto.
        }

        // Percorre o array de pontos para desenhar os gizmos.
        for (int i = 0; i < points.Length; i++)
        {        
            Gizmos.color = Color.green; // Define a cor do gizmo como verde
            Gizmos.DrawWireSphere(points[i] + _currentPos, 0.5f); //Desenha uma WireSphere no ponto com o offset de _currentPos.

            // Se não for o último ponto, desenha uma linha cinza até o próximo ponto.
            if (i < points.Length - 1)
            {
                Gizmos.color = Color.grey;
                Gizmos.DrawLine(points[i] + _currentPos, points[i + 1] + _currentPos);
            }
        }
    }
}
