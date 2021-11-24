using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Buttons : MonoBehaviour
{
    public void PlayButton()
    {
        Debug.Log("Play Game");

        for (int e = 0; e < 12; e++)
        {
            PlayerPrefs.SetInt("isDead" + e.ToString(), 0);
            PlayerPrefs.SetInt("playerLevel", 1);

            Scene_Variables.instance.playerCurrentPosition = Scene_Variables.instance.playerInitialPosition;
        }
    }

    public void QuitButton()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
