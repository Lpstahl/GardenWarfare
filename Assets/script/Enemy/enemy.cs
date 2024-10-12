using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static Action OnEndReached; 
    public static Action OnEnemyKilled;

    [SerializeField] public float movespeed = 2f;

    [Header("Health")]
    [SerializeField] public float initialHealth;
    [SerializeField] public float maxHealth;

    public float CurrentHealth { get; set; }

    public Waypoint Waypoint { get; set; }

    private Vector3 CurrentPosition => Waypoint.GetwaypointPosition(_currentWaypointIndex);

    private int _currentWaypointIndex;

    private PLayerHealth playerHealth; 
    private GameManager gameManager; // Referência ao GameManager

    private void Start()
    {
        _currentWaypointIndex = 0;
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PLayerHealth>();
        gameManager = FindObjectOfType<GameManager>(); // Obter referência ao GameManager
        CurrentHealth = initialHealth;
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
            EndPointReached();
        }
    }

    private void EndPointReached()
    {
        OnEndReached?.Invoke();
        playerHealth.TakeDamage(1); // Reduz a saúde do jogador em 1 coração
        ResetHealth();
        ObjectPooler.ReturnToPool(gameObject);
    }

    public void ResetEnemy()
    {
        _currentWaypointIndex = 0;
    }

    public void TakeDamage(float amount)
    {
        CurrentHealth -= amount;
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    public void ResetHealth()
    {
        CurrentHealth = initialHealth;
    }

    private void Die()
    {
        ResetHealth();
        OnEnemyKilled?.Invoke();
        gameManager.AddPoints(10); // Concede 10 pontos ao jogador por derrotar o inimigo
        ObjectPooler.ReturnToPool(gameObject);
    }
}
