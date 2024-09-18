using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Pooler é como uma fábrica de objetos que mantém uma reserva (pool) de objetos prontos para uso, em vez de criar e descartar objetos o tempo todo.
public class ObjectPooler : MonoBehaviour
{
    [SerializeField] private GameObject prefab; // Define o prefab a ser instanciado.
    [SerializeField] private int poolSize = 10; // Define o tamanho do pool.

    private List<GameObject> _pool; // Define a lista de GameObjects.
    private GameObject _poolContainer; // Define o container do pool.

    private void Awake()
    {
        _pool = new List<GameObject>(); // Inicializa a lista de GameObjects.
        _poolContainer = new GameObject($"Pool - {prefab.name}"); // Cria o container do pool.
        CreatePooler(); // Cria o pooler.
    }

    private void CreatePooler()
    {
       for (int i = 0; i < poolSize; i++) // Para cada GameObject no pool,
        {
            _pool.Add(CreateInstance()); // Adiciona um novo GameObject.
        }
    }

    private GameObject CreateInstance()
    {
        GameObject newInstance = Instantiate(prefab); // Instancia um novo GameObject.
        newInstance.transform.SetParent(_poolContainer.transform); // Define o container do pool como o pai do GameObject.
        newInstance.SetActive(false); // Desativa o GameObject.
        return newInstance; // Retorna o GameObject.
    }

    public GameObject GetInstanceFromPool()
    {
        for(int i = 0; i < _pool.Count; i++) // Para cada GameObject no pool,
        {
            if (!_pool[i].activeInHierarchy) // Se o GameObject não estiver ativo,
            {
                return _pool[i]; // Retorna o GameObject.
            }
        }
                return CreateInstance(); // Retorna um novo GameObject.
    }
    public static void ReturnToPool(GameObject instance)
    {
        instance.SetActive(false); // Desativa o GameObject.
    } 
}
