using System;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public static Action OnEndReached;
    public static Action OnEnemyKilled;

    [SerializeField] public float movespeed = 2f;

    [Header("Health")]
    [SerializeField] public float initialHealth;
    [SerializeField] public float maxHealth;

    [SerializeField] private GameObject healthBarPrefab; // Prefab da barra de vida.
    [SerializeField] private Transform barPosition; // Posição da barra de vida.


    public float CurrentHealth { get; set; }

    private Image _helthBar; // Barra de vida do inimigo.

    public Waypoint Waypoint { get; set; }

    private Vector3 CurrentPosition => Waypoint.GetwaypointPosition(_currentWaypointIndex);

    private int _currentWaypointIndex;

    private PLayerHealth playerHealth;
    private GameManager gameManager; // Referencia ao GameManager

    private void Start()
    {
        _currentWaypointIndex = 0;
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PLayerHealth>();
        gameManager = FindObjectOfType<GameManager>(); // Obter referencia ao GameManager
        CreateHealthBar(); // Cria a barra de vida.
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
        playerHealth.TakeDamage(1);
        ResetHealth();
        ObjectPooler.ReturnToPool(gameObject);
    }

    public void ResetEnemy()
    {
        _currentWaypointIndex = 0;
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
        CurrentHealth -= amount;
        _helthBar.fillAmount = CurrentHealth / maxHealth; // Atualiza a barra de vida.
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    public void ResetHealth()
    {
        CurrentHealth = initialHealth;
        _helthBar.fillAmount = 1f; // Atualiza a barra de vida.
    }

    private void Die()
    {
        ResetHealth();
        OnEnemyKilled?.Invoke();
        gameManager.AddPoints(10);
        ObjectPooler.ReturnToPool(gameObject);
    }
}
