using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static Action OnEndReached; // Define a ação OnEndReached.

    [SerializeField] public float movespeed = 2f;
    [SerializeField] public float health = 100f; // saúde do inimigo

    public Waypoint Waypoint { get; set; } // Define o waypoint.

    private Vector3 CurrentPosition => Waypoint.GetwaypointPosition(_currentWaypointIndex);  // Define a posição atual.

    private int _currentWaypointIndex; // Define o índice do waypoint atual.

    private void Start()
    {
        _currentWaypointIndex = 0; // Define o índice do waypoint atual como 0.
        
    }

    private void Update()
    {
        Move();
        if (CurrentPositionReached()) // Se a posição atual for alcançada,
        {
            UpdateCurrentPointIndex();  // Atualiza o índice do ponto atual.
        }
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, CurrentPosition, movespeed * Time.deltaTime);  // Move o inimigo.
    }

    private bool CurrentPositionReached()
    {
        float distanceToNextPosition = (transform.position - CurrentPosition).magnitude; // Define a distância até a próxima posição.
        return distanceToNextPosition < 0.1f; // Retorna se a distância até a próxima posição for menor que 0.1f.
    }

    private void UpdateCurrentPointIndex()
    {
        int lastWaypointIndex = Waypoint.Points.Length - 1; // Define o último índice do waypoint.
        if (_currentWaypointIndex < lastWaypointIndex) // Se o índice do waypoint atual for menor que o último índice do waypoint,
        {
            _currentWaypointIndex++; // Incrementa o índice do waypoint atual.
        }
        else
        {
            ReturnEnemyToPool(); // Retorna o inimigo para o pool.
        }
    }

    private void ReturnEnemyToPool()
    {
        OnEndReached?.Invoke(); // Invoca a ação OnEndReached.
        ObjectPooler.ReturnToPool(gameObject); // Retorna o inimigo para o pool.
    }

    public void ResetEnemy()
    {
        _currentWaypointIndex = 0; // Define o índice do waypoint atual como 0.
    }

    public void TakeDamage(float amount)
    {
        health -= amount; // Decrementa a saúde do inimigo.
        if (health <= 0) // Se a saúde do inimigo for menor ou igual a 0,
        {
            Die(); // Morre.
        }
    }

    private void Die()
    {
        // Adicione efeitos de morte aqui
      ObjectPooler.ReturnToPool(gameObject); // Retorna o inimigo para o pool.
    }
}
