using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public GameObject prefab;  // O prefab que será instanciado.
        public int quantity;  // Quantidade de objetos que deseja no pool.
        public float startDelay;  // O delay antes de começar a spawnar esse pool.

        [HideInInspector]
        public float delayTimer;  // Temporizador interno para controlar o delay entre spawns.
    }

    [SerializeField] public Pool[] pools;  // Lista de pools configuráveis.
    private List<GameObject> _pool;  // Lista de objetos criados para reuso.
    private GameObject _poolContainer;  // Contêiner pai para organizar os objetos no pool.

    private void Awake()
    {
        _pool = new List<GameObject>();
        _poolContainer = new GameObject("Pool Container");

        CreatePooler();  // Cria os objetos no pool com base na quantidade definida.
    }

    private void CreatePooler()
    {
        foreach (Pool pool in pools)
        {
            for (int i = 0; i < pool.quantity; i++)  // Gera a quantidade de objetos definidos no pool.
            {
                GameObject newInstance = CreateInstance(pool.prefab);
                _pool.Add(newInstance);  // Adiciona o novo objeto ao pool.
            }
        }
    }

    private GameObject CreateInstance(GameObject prefab)
    {
        GameObject newInstance = Instantiate(prefab);  // Instancia um novo objeto a partir do prefab.
        newInstance.transform.SetParent(_poolContainer.transform);  // Define o contêiner como pai do objeto.
        newInstance.SetActive(false);  // Desativa o objeto inicialmente.
        return newInstance;
    }

    public GameObject GetInstanceFromPool(int poolIndex)
    {
        if (poolIndex < 0 || poolIndex >= pools.Length)
        {
            Debug.LogError("Índice de pool inválido");
            return null;
        }

        Pool pool = pools[poolIndex];

        // Verifica se existe algum objeto inativo no pool para reutilização.
        for (int i = 0; i < _pool.Count; i++)
        {
            if (!_pool[i].activeInHierarchy)
            {
                return _pool[i];
            }
        }

        // Se nenhum objeto estiver disponível, cria um novo e o adiciona ao pool.
        GameObject newInstance = CreateInstance(pool.prefab);
        _pool.Add(newInstance);
        return newInstance;
    }

    // Função para remover permanentemente o objeto do pool.
    public void RemoveFromPool(GameObject instance)
    {
        if (_pool.Contains(instance))
        {
            _pool.Remove(instance);  // Remove o objeto da lista do pool.
            Destroy(instance);  // Destrói o objeto permanentemente.
        }
    }

    public static void ReturnToPool(GameObject instance)
    {
        instance.SetActive(false);  // Apenas desativa o objeto, mas não o remove permanentemente.
    }
}
