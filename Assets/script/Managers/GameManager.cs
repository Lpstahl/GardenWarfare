using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int points = 100; // Pontos iniciais do jogador
    public Text pointsText; // UI Text para mostrar os pontos

    private void Start()
    {
        UpdatePointsText(); // Atualiza o texto dos pontos no início
    }

    // Funcao para gastar pontos
    public bool SpendPoints(int amount)
    {
        if (points >= amount)
        {
            points -= amount;
            UpdatePointsText();
            return true;
        }
        return false;
    }

    // Funcao para conceder pontos
    public void AddPoints(int amount)
    {
        points += amount;
        UpdatePointsText();
    }

    // Funcao para comprar torres
    public void BuyTower(string towerAssetPath, int cost)
    {
        Debug.Log("Tentando comprar a torre: " + towerAssetPath + " por " + cost + " pontos.");
        
        if (SpendPoints(cost))
        {
            // Carregar o prefab da torre a partir do caminho do asset
            GameObject towerPrefab = Resources.Load<GameObject>(towerAssetPath);
            if (towerPrefab != null)
            {
                Debug.Log("Prefab da torre carregado com sucesso.");
                Instantiate(towerPrefab, new Vector3(0, 0, 0), Quaternion.identity); // Ajuste a posicaoo conforme necessario
            }
            else
            {
                Debug.LogError("Não foi possível carregar o prefab da torre: " + towerAssetPath);
            }
        }
        else
        {
            Debug.Log("Não há pontos suficientes para comprar a torre.");
        }
    }

    // Funcao para atualizar o texto dos pontos
    private void UpdatePointsText()
    {
        pointsText.text = "Points: " + points.ToString();
    }
}
