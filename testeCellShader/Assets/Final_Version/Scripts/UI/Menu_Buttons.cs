using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Buttons : MonoBehaviour
{
    public void QuitButton()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
