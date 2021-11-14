using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Open_Panel : MonoBehaviour
{
    [SerializeField]
    private GameObject panel;

    // Disable or enable the panel
    public void ButtonInteraction()
    {
        if (panel.activeSelf == false)
            panel.SetActive(true);
        else
            panel.SetActive(false);
    }
}
