using System;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public static Action OnEndReached; // Define a ação OnEndReached.
    public static Action OnEnemyKilled; // Define a ação OnEnemyKilled.

    [SerializeField] public float movespeed = 2f;
    
    [Header("Health")]
    [SerializeField] public float initialHealth; // saúde do inimigo
    [SerializeField] public float maxHealth; // saúde máxima do inimigo
    
    [SerializeField] private GameObject healthBarPrefab; // Prefab da barra de vida.
    [SerializeField] private Transform barPosition; // Posição da barra de vida.

    public float CurrentHealth { get; set; }

    private Image _helthBar; // Barra de vida do inimigo.

    public Waypoint Waypoint { get; set; } // Define o waypoint.

    private Vector3 CurrentPosition => Waypoint.GetwaypointPosition(_currentWaypointIndex);  // Define a posição atual.

    private int _currentWaypointIndex; // Define o índice do waypoint atual.

    private PlayerHealth playerHealth; // Referência ao script de saúde do jogador.

    private void Start()
    {
        _currentWaypointIndex = 0; // Define o índice do waypoint atual como 0.
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>(); // Obtém a referência ao script de saúde do jogador.

        CreateHealthBar(); // Cria a barra de vida.
        CurrentHealth = initialHealth; // Define a saúde atual como a saúde inicial.
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
            EndPointReached(); // Retorna o inimigo para o pool.
        }
    }

    private void EndPointReached()
    {
        OnEndReached?.Invoke(); // Invoca a ação OnEndReached.
        playerHealth.TakeDamage(10); // Causa dano ao jogador.
        ResetHealth(); // Reseta a saúde do inimigo.
        ObjectPooler.ReturnToPool(gameObject); // Retorna o inimigo para o pool.
    }

    public void ResetEnemy()
    {
        _currentWaypointIndex = 0; // Define o índice do waypoint atual como 0.
    }

    private void CreateHealthBar()
    {
        GameObject newBar = Instantiate(healthBarPrefab, barPosition.position, Quaternion.identity); // Instancia uma nova barra de vida.
        newBar.transform.SetParent(transform); // Define a barra de vida como filha do inimigo.

        EnemyHealthContainer healthContainer = newBar.GetComponent<EnemyHealthContainer>(); // Obtém o componente EnemyHealthContainer da barra de vida.
        _helthBar = healthContainer.FillAmountImage; // Define a barra de vida.
    }

    public void TakeDamage(float amount)
    {
        CurrentHealth -= amount; // Decrementa a saúde do inimigo.
        _helthBar.fillAmount = CurrentHealth / maxHealth; // Atualiza a barra de vida.
        if (CurrentHealth <= 0) // Se a saúde do inimigo for menor ou igual a 0,
        {
            Die(); // Morre.
        }
    }

    public void ResetHealth()
    {
        CurrentHealth = initialHealth; // Define a saúde atual como a saúde inicial.
        _helthBar.fillAmount = 1f; // Atualiza a barra de vida.
    }

    private void Die()
    {
        ResetHealth(); // Reseta a saúde do inimigo.
        OnEnemyKilled?.Invoke(); // Invoca a ação OnEnemyKilled.
        ObjectPooler.ReturnToPool(gameObject); // Retorna o inimigo para o pool.
    }
}
