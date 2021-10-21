using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Battle : MonoBehaviour
{
    [SerializeField]
    private GameObject[] playerUnits;

    [SerializeField]
    private GameObject[] enemyUnits;


    private GameObject selected;
    private GameObject target;

    public bool selecting = true;
    public bool targeting;

    [SerializeField]
    private GameObject commandCard;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Selecionado o jogador
    public void SelectUnit(GameObject host)
    {
        if (selected != null)
        {
            selected.transform.GetChild(0).GetComponent<Select_Unit>().isSelected = false;
            selected.transform.GetChild(0).GetComponent<Select_Unit>().rend.material.color = selected.transform.GetChild(0).GetComponent<Select_Unit>().col;
        }

        selected = host;
        selected.transform.GetChild(0).GetComponent<Select_Unit>().hover = null;

        commandCard.SetActive(true);


    }

    //ao clicar no botao de ataque vai liberar para selecionar o inimigo
    public void AttackButton()
    {
        selecting = false;
        targeting = true;
    }
}
