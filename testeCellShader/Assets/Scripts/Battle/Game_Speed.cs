using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Game_Speed : MonoBehaviour
{
    [SerializeField]
    private GameObject[] units;

    [SerializeField]
    private Button[] buttons;

    [SerializeField]
    private TextMeshProUGUI[] text;

    [SerializeField]
    private float gameSpeed = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Increases the animation speed and changes the text of the button
    public void IncreassesButton()
    {
        gameSpeed ++;

        switch (gameSpeed)
        {
            case 2:
                {
                    text[0].text = "2x";
                    break;
                }
            case 3:
                {
                    buttons[0].interactable = false;
                    text[0].text = "3x";
                    break;
                }
        }

        for (int u = 0; u < units.Length; u++)
        {
            units[u].GetComponent<Unit_Info>().animator.SetFloat("animationSpeed", gameSpeed);
        }
    }

    public void DecreassesButton()
    {
        gameSpeed --;

        switch (gameSpeed)
        {
            case 1:
                {
                    buttons[1].interactable = false;
                    text[0].text = "1x";
                    break;
                }
            case 2:
                {
                    text[0].text = "2x";
                    break;
                }
        }

        for (int u = 0; u < units.Length; u++)
        {
            units[u].GetComponent<Unit_Info>().animator.SetFloat("animationSpeed", gameSpeed);
        }
    }
}
