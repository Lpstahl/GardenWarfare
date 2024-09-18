using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]public float movespeed = 2f; // velocidade do inimigo
    [SerializeField] private Waypoint waypoint; // caminho que o inimigo vai seguir

    private Vector3 CurrentPosition => waypoint.GetwaypointPosition(_currentWaypointIndex); // posição do waypoint atual

    private int _currentWaypointIndex; // index do waypoint atual

    private void Start()
    {
        _currentWaypointIndex = 0; // inicia o index do waypoint
    }

    private void Update()
    {
        Move(); // chama a função de movimento
        if (CurrentPositionReached()) // se a posição atual for alcançada
        {
            UpdateCurrentPointIndex(); // atualiza o index do waypoint      
        }
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, CurrentPosition, movespeed * Time.deltaTime); // move o inimigo para a posição do waypoint
    }

    private bool CurrentPositionReached()
    {
        float distanceToNextPosition = (transform.position - CurrentPosition).magnitude; // distancia do inimigo para o waypoint

        if (distanceToNextPosition < 0.1f) // se a distancia for menor que 0.1
        {
            return true; // retorna verdadeiro
        }
        return false; // se não, retorna falso
    }

    private void UpdateCurrentPointIndex()
    {
        int lastWaypointIndex = waypoint.Points.Length - 1; // index do ultimo waypoint
        if (_currentWaypointIndex < lastWaypointIndex) // se o index do waypoint atual for menor que o index do ultimo waypoint
        {
            _currentWaypointIndex++; // incrementa o index do waypoint
        }
    }
}
