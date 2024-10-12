using UnityEngine;
using UnityEngine.UI;

public class TowerPurchaseButton : MonoBehaviour
{
    public GameManager gameManager; // Referência ao GameManager
    public string towerAssetPath; // Caminho do asset da torre
    public int cost; // Custo da torre

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => {
            Debug.Log("Botão de compra de torre clicado.");
            gameManager.BuyTower(towerAssetPath, cost);
        });
    }
}
