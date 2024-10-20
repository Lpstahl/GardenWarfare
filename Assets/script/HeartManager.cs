using UnityEngine;
using UnityEngine.UI;

public class HeartManager : MonoBehaviour
{
    public Image[] hearts; // Array de imagens de coração
    public Sprite fullHeart; // Sprite de coração cheio
    public Sprite emptyHeart; // Sprite de coração vazio

    private int health;

    public void SetHealth(int health)
    {
        this.health = health;
        UpdateHearts();
    }

    private void UpdateHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
        }
    }
}
