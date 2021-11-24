using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using TMPro;

public enum UnitType
{
    SwordBoy,
    ShieldBoy,
    MageGirl,
    Demon,
    Tentacle,
    Kraken
}


public enum UnitSpells
{
    watherBall,
    slash,
    stun,
    heal
}

public class Unit_Info : MonoBehaviour
{
    //objeto que carrega o modelo
    [Header("Essencials")]
    public GameObject model;

    //o animator que o modelo carrega
    [HideInInspector]
    public Animator animator;

    [HideInInspector]
    public NavMeshAgent navM;

    public UnitType unitType; 


    [Header("-> Name <-")]
    public string unitName = "sem nome";

    [SerializeField]
    private GameObject nameTagPrefab;
    
    [SerializeField]
    private Transform nameTagUI;

    [SerializeField]
    private Vector3 nameTagOffset;

    [HideInInspector]
    public GameObject nameTag;

    [Header("-> Level <-")]
    [Range(1, 20)]
    public int unitLevel = 1;

    public int[] maxExp;
    public int curExp;

    [Header("-> Level Scaling <-")]
    public int lifeScaling;
    public int manaScaling;
    public int damageScaling;

    [Header("-> Life <-")]
    public int lifeBase = 1;
    public int lifeMax = 1;
    public int lifeCur = 1;
    public int lifeBonus;

    [Range(0, 10)]
    public int armor = 0;

    [Header("-> Mana <-")]
    public int manaBase;
    public int manaMax, manaCur, manaBonus;

    [Header("-> Movement <-")]
    [Range(1, 10)]
    public float speed = 1f;

    [Range(100f, 720f)]
    public float turnRate = 720f;

    [Header("-> Map <-")]
    public GameObject minimapIcon;

    [Header("-> Combat <-")]
    public GameObject battleSystem;
    public bool isInCombat, isDead;
    public GameObject unitPos;

    [Header("-> Basic Attack <-")]
    public int damageBase;
    public int damage;
    public int damageBonus;
    public float critChance;
    public float critDamageMult;
    public float critScaling;

    [Header("-> Spells <-")]
    public UnitSpells[] spells;
    public int[] spellDamage;
    public int[] spellCost;
    public Sprite[] spellIcon;



    [Header("-> UI <-")]
    public GameObject selectIcon;

    [SerializeField]
    private GameObject lifeBar;

    [SerializeField]
    private GameObject damageIndicator;


    void Start()
    {
        if (model != null)
            animator = model.GetComponent<Animator>();
        else
            Debug.Log(gameObject.name + ": no model");

        if (GetComponent<NavMeshAgent>())
            navM = GetComponent<NavMeshAgent>();

        if (nameTagPrefab != null && nameTagUI != null)
        {
            nameTag = Instantiate(nameTagPrefab, transform.position, Quaternion.identity) as GameObject;
            nameTag.transform.parent = transform;
            nameTag.GetComponent<Name_Tag>().SetAttributes(unitName, nameTagOffset);
        }

        if (minimapIcon != null && model != null && isDead == false)
        {
            GameObject minimapTemp = Instantiate(minimapIcon, transform.position, Quaternion.identity) as GameObject;
            minimapTemp.transform.rotation = model.transform.rotation;
            minimapTemp.transform.parent = model.transform;
        }

        if (tag == "Player")
        {
            if (isInCombat == false)
            {
                if (Scene_Variables.instance.playerCurrentPosition != Vector3.zero)
                    transform.position = Scene_Variables.instance.playerCurrentPosition;
            }

            unitLevel = PlayerPrefs.GetInt("playerLevel");
            curExp = PlayerPrefs.GetInt("playerExp");
        }
        else if (tag == "Companion_1")
        {
            if (Scene_Variables.instance.companion1CurrentPosition != Vector3.zero)
                transform.position = Scene_Variables.instance.companion1CurrentPosition;
        }
        else if (tag == "Companion_2")
        {
            if (Scene_Variables.instance.companion2CurrentPosition != Vector3.zero)
                transform.position = Scene_Variables.instance.companion2CurrentPosition;
        }
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("combat", isInCombat);
        animator.SetBool("dead", isDead);
        LevelUP();
        UpdateVitals();
        LifeBar();
    }

    void LifeBar()
    {
        if (lifeBar != null)
        {
            if (isDead == true)
                lifeBar.SetActive(false);
            else
                lifeBar.SetActive(true);
        }
    }

    void LevelUP()
    {
        if (curExp >= maxExp[unitLevel - 1])
        {
            curExp -= maxExp[unitLevel];
            unitLevel++;

            lifeCur = lifeMax;

            PlayerPrefs.SetInt("playerLevel", unitLevel);
        }
    }

    public void UpdateVitals()
    {
        lifeMax = lifeBase + lifeScaling * (unitLevel - 1) + lifeBonus;
        manaMax = manaBase + manaScaling * (unitLevel - 1) + manaBonus;

        damage = damageBase + damageScaling * (unitLevel - 1) + damageBonus;
    }

    public void TakeDamage(int damageTemp, float critChanceTemp, float critMultTemp, bool isDefending)
    {
        float critRandomizer = Random.Range(0, 100);

        int damage = damageTemp;

        if (critRandomizer <= critChanceTemp)
        {
            damage = ((int)(damageTemp * critMultTemp));
            damageIndicator.GetComponent<TextMeshProUGUI>().color = Color.red;
        }
        else
            damageIndicator.GetComponent<TextMeshProUGUI>().color = Color.yellow;

        if (isDefending == true)
            damage /= 2;

        if (lifeCur > damage)
        {
            lifeCur -= damage;
            damageIndicator.GetComponent<TextMeshProUGUI>().text = damage.ToString();

            if (isDefending == false)
                animator.SetTrigger("damaged");
            else
                animator.SetTrigger("defend");
        }
        else
        {
            damageIndicator.GetComponent<TextMeshProUGUI>().text = damageTemp.ToString();
            animator.SetBool("dead", true);
            lifeCur = 0;
            isDead = true;
        }

        damageIndicator.GetComponent<Animator>().SetTrigger("PopUp");
    }
}
