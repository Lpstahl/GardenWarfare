using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static Action OnEndReached; // evento que � chamado quando o inimigo chega ao final do caminho

    [SerializeField]public float movespeed = 2f; // velocidade do inimigo

    public Waypoint Waypoint { get; set; } // propriedade do tipo Waypoint

    private Vector3 CurrentPosition => Waypoint.GetwaypointPosition(_currentWaypointIndex); // posi��o do waypoint atual

    private int _currentWaypointIndex; // index do waypoint atual

    private void Start()
    {
        _currentWaypointIndex = 0; // inicia o index do waypoint
    }

    private void Update()
    {
        Move(); // chama a fun��o de movimento
        if (CurrentPositionReached()) // se a posi��o atual for alcan�ada
        {
            UpdateCurrentPointIndex(); // atualiza o index do waypoint      
        }
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, CurrentPosition, movespeed * Time.deltaTime); // move o inimigo para a posi��o do waypoint
    }

    private bool CurrentPositionReached()
    {
        float distanceToNextPosition = (transform.position - CurrentPosition).magnitude; // distancia do inimigo para o waypoint

        if (distanceToNextPosition < 0.1f) // se a distancia for menor que 0.1
        {
            return true; // retorna verdadeiro
        }
        return false; // se n�o, retorna falso
    }

    private void UpdateCurrentPointIndex()
    {
        int lastWaypointIndex = Waypoint.Points.Length - 1; // index do ultimo waypoint
        if (_currentWaypointIndex < lastWaypointIndex) // se o index do waypoint atual for menor que o index do ultimo waypoint
        {
            _currentWaypointIndex++; // incrementa o index do waypoint
        }
        else
        {
            ReturnEnemyToPool(); // se n�o, retorna o inimigo para o pool
        }
    }

    private void ReturnEnemyToPool()
    {
       OnEndReached?.Invoke(); // chama o evento de fim de caminho
        ObjectPooler.ReturnToPool(gameObject); // retorna o inimigo para o pool
    }

    public void ResetEnemy()
    {
       _currentWaypointIndex = 0; // reseta o index do waypoint
    }
}
