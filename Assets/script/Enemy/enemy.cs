using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static Action OnEndReached;

    [SerializeField] public float movespeed = 2f;
    [SerializeField] public float health = 100f; // saúde do inimigo

    public Waypoint Waypoint { get; set; }

    private Vector3 CurrentPosition => Waypoint.GetwaypointPosition(_currentWaypointIndex);

    private int _currentWaypointIndex;

    private void Start()
    {
        _currentWaypointIndex = 0;
        
    }

    private void Update()
    {
        Move();
        if (CurrentPositionReached())
        {
            UpdateCurrentPointIndex();
        }
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, CurrentPosition, movespeed * Time.deltaTime);
    }

    private bool CurrentPositionReached()
    {
        float distanceToNextPosition = (transform.position - CurrentPosition).magnitude;
        return distanceToNextPosition < 0.1f;
    }

    private void UpdateCurrentPointIndex()
    {
        int lastWaypointIndex = Waypoint.Points.Length - 1;
        if (_currentWaypointIndex < lastWaypointIndex)
        {
            _currentWaypointIndex++;
        }
        else
        {
            ReturnEnemyToPool();
        }
    }

    private void ReturnEnemyToPool()
    {
        OnEndReached?.Invoke();
        ObjectPooler.ReturnToPool(gameObject);
    }

    public void ResetEnemy()
    {
        _currentWaypointIndex = 0;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Adicione efeitos de morte aqui
      ObjectPooler.ReturnToPool(gameObject);
    }
}
