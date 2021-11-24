using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Select_Unit : MonoBehaviour
{
    [SerializeField]
    private GameObject battleSystem;

    public GameObject unitInPos;
    public GameObject selectIcon;

    public Color posColor;

    [HideInInspector]
    public GameObject hover;
    public bool isSelected;

    public GameObject[] neighbor;

    private void Start()
    {
        transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = posColor;
    }

    // Update is called once per frame
    void Update()
    {
        // Ao clicar e estiver com o mouse em cima do objeto vai selecionar e mandar a informacao para o battle system
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (hover != null && battleSystem.GetComponent<New_Battle_System>().playerIsSelecting == true && tag == "Player")
            {
                isSelected = true;
                battleSystem.GetComponent<New_Battle_System>().SelectUnit(gameObject);
            }
            else if (hover != null && battleSystem.GetComponent<New_Battle_System>().playerIsTargetingEnemy == true && tag == "Enemy")
            {
                battleSystem.GetComponent<New_Battle_System>().SelectEnemyUnit(gameObject);
            }
            else if (hover != null && battleSystem.GetComponent<New_Battle_System>().playerIsTargetingSelf == true && tag == "Player")
            {
                battleSystem.GetComponent<New_Battle_System>().SelectPlayerUnit(gameObject);
            }
        }
    }

    // Passar o mouse em cima muda a cor e fala que pode selecionar o objeto hover caso nao esteja selecionado
    private void OnMouseEnter()
    {
            if (isSelected == false && unitInPos.GetComponent<Unit_Info>().isDead == false)
            {
                switch (tag)
                {
                    case "Player":
                    {
                        if (battleSystem.GetComponent<New_Battle_System>().playerIsSelecting == true || battleSystem.GetComponent<New_Battle_System>().playerIsTargetingSelf == true)
                        {
                            selectIcon.SetActive(true);
                            hover = gameObject;
                        }
                        break;
                    }
                    case "Enemy":
                    {
                        if (battleSystem.GetComponent<New_Battle_System>().playerIsTargetingEnemy == true)
                        {
                            selectIcon.SetActive(true);
                            hover = gameObject;
                        }
                        break;
                    }
                }
            }
    }

    //muda a cor e o objeto hover caso nao esteja selecionado
    private void OnMouseExit()
    {
        if(isSelected == false)
        {
            selectIcon.SetActive(false);
            hover = null;
        }
    }
}
