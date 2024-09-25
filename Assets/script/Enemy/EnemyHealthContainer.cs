using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthContainer : MonoBehaviour
{
    [SerializeField] private Image fillAmountImage; // Imagem do preenchimento da barra de vida.
    public Image FillAmountImage => fillAmountImage; // Propriedade da imagem do preenchimento da barra de vida.
}
