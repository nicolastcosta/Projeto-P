using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene_Variables : MonoBehaviour
{
    public static Scene_Variables instance;

    public int playerLevel;
    public int enemyLevel;

    public int exp;

    // Start is called before the first frame update
    void Awake()
    {
        if (Scene_Variables.instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this);
    }
}
