using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Select_Unit : MonoBehaviour
{
    [SerializeField]
    private GameObject battleSystem;

    [HideInInspector]
    public Renderer rend;

    public GameObject host;

    [HideInInspector]
    public GameObject hover;
    public bool isSelected;

    [HideInInspector]
    public Color col;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<MeshRenderer>();
        col = rend.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        //Ao clicar e estiver com o mouse em cima do objeto vai selecionar e mandar a informacao para o battle system
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (hover != null && battleSystem.GetComponent<Battle>().selecting == true && tag == "Player")
            {
                isSelected = true;
                battleSystem.GetComponent<Battle>().SelectUnit(host);
            }
            else if (hover != null && battleSystem.GetComponent<Battle>().targeting == true && tag == "Enemy")
            {
                battleSystem.GetComponent<Battle>().SelectTarget(host);
            }
        }
    }

    //Passar o mouse em cima muda a cor e fala que pode selecionar o objeto hover caso nao esteja selecionado
    private void OnMouseEnter()
    {
        
            if (isSelected == false)
            {
                switch (tag)
                {
                    case "Player":
                    {
                        if (battleSystem.GetComponent<Battle>().selecting == true)
                        {
                            host.GetComponent<Unit_Info>().selectIcon.SetActive(true);
                            hover = gameObject;
                        }
                        break;
                    }
                    case "Enemy":
                    {
                        if (battleSystem.GetComponent<Battle>().targeting == true)
                        {
                            host.GetComponent<Unit_Info>().selectIcon.SetActive(true);
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
            host.GetComponent<Unit_Info>().selectIcon.SetActive(false);
            rend.material.color = col;
            hover = null;
        }
    }
}
