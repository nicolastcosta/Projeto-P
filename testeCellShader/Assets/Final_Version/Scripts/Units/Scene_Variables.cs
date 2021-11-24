using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene_Variables : MonoBehaviour
{
    public static Scene_Variables instance;

    public int playerLevel;
    public int enemyLevel;

    public int exp;

    public int enemyIndex;

    public Vector3 playerCurrentPosition;
    public Vector3 playerInitialPosition = new Vector3(15, 1, 81);

    public Vector3 companion1CurrentPosition;
    public Vector3 companion1InitialPosition = new Vector3(40, 0.25f, 70);

    public Vector3 companion2CurrentPosition;
    public Vector3 companion2InitialPosition = new Vector3(65, 0.25f, 52);

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
