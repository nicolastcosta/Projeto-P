using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Info : MonoBehaviour
{
    [Header("Essencials")]
    //objeto que carrega o modelo
    public GameObject model;
    //o animator que o modelo carrega
    [HideInInspector]
    public Animator animator;
    

    [Header("Name")]
    //
    public string unitName = "sem nome";

    [SerializeField]
    private bool hasNameTag;

    [SerializeField]
    private GameObject nameTagPrefab;
    
    [SerializeField]
    private Transform nameTagUI;

    [SerializeField]
    private Vector3 nameTagOffset;

    [HideInInspector]
    public GameObject nameTag;

    [Header("Life")]
    [Range(1, 500)]
    public int lifeMax = 1;

    [Range(1, 500)]
    public int lifeCur = 1;

    [Range(0, 10)]
    public int armor = 0;

    [Header("Mana")]
    [Range(0, 10)]
    public int manaMax = 0;

    [Range(0, 10)]
    public int manaCur = 0;

    [Header("Movement")]
    [Range(1, 10)]
    public float speed = 1f;

    [Range(100f, 720f)]
    public float turnRate = 720f;

    [Header("Mapa")]
    public GameObject minimapIcon;

    [Header("Combat")]
    public bool isInCombat;




    void Awake()
    {
        animator = model.GetComponent<Animator>();

        if (hasNameTag == true)
        {
            if (nameTagPrefab != null && nameTagUI != null)
            {
                nameTag = Instantiate(nameTagPrefab, transform.position, Quaternion.identity) as GameObject;
                nameTag.transform.parent = transform;
                nameTag.GetComponent<Name_Tag>().SetAttributes(unitName, nameTagOffset);

            }
            else
                Debug.Log("Error to create Name Tag");
        }

        if (minimapIcon != null)
        {
            GameObject minimapTemp = Instantiate(minimapIcon, transform.position, Quaternion.identity) as GameObject;
            minimapTemp.transform.parent = model.transform;
        }
            
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
