using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Select_Unit : MonoBehaviour
{
    [SerializeField]
    private GameObject battleSystem;

    [HideInInspector]
    public Renderer rend;

    [SerializeField]
    private GameObject host;

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
        //ao clicar e estiver com o mouse em cima do objeto vai selecionar e mandar a informacao para o battle system
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (hover != null && battleSystem.GetComponent<Battle>().selecting == true)
            {
                isSelected = true;
                rend.material.color = Color.green;
                battleSystem.GetComponent<Battle>().SelectUnit(host);
            }
        }
    }

    //passar o mouse em cima muda a cor e fala que pode selecionar o objeto hover caso nao esteja selecionado
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
                            rend.material.color = Color.yellow;
                            hover = gameObject;
                        }
                        break;
                    }
                    case "Enemy":
                    {
                        if (battleSystem.GetComponent<Battle>().targeting == true)
                        {
                            rend.material.color = Color.yellow;
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
            rend.material.color = col;
            hover = null;
        }
    }
}
