using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Battle : MonoBehaviour
{
    [Header("-> Actions per Turn <-")]
    [SerializeField]
    public int maxActions;
    public int curAction;
    public int curMov;

    [Header("-> Units <-")]
    [SerializeField]
    private GameObject[] playerUnits;

    [SerializeField]
    private GameObject[] enemyUnits;

    [Header("-> Player Actions <-")]
    public BattleAction[] playerAction;

    public GameObject[] playerSelected, playerTarget;

    [Header("-> Enemy Actions <-")]
    public BattleAction[] enemyAction;

    [SerializeField]
    private GameObject[] enemySelected, enemyTarget;

    [Header("-> Action Icons <-")]
    [SerializeField]
    private GameObject[] playerActionIcon;

    [SerializeField]
    private GameObject[] enemyActionIcon;

    [Header("-> Battle States <-")]
    public bool selectingPlayer = true;
    public bool targetingEnemy, targetingPlayer, isInBattle;

    [Header("-> UI Elements <-")]
    [SerializeField]
    private GameObject commandCard;

    [SerializeField]
    private GameObject battleButton;

    // Start is called before the first frame update
    void Start()
    {
        //Primeiras a��es do inimigo
        EnemyAI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Jogador seleciona uma unidade dele
    public void SelectUnit(GameObject unit)
    {
        //Caso seja a primeira sele��o n�o vai fazer nada, mas se j� tiver selecionado antes, vai descelecionar a antiga para selecionar a nova
        if (playerSelected[curAction] != null)
        {
            playerSelected[curAction].transform.GetChild(0).GetComponent<Select_Unit>().isSelected = false;
            playerSelected[curAction].GetComponent<Unit_Info>().selectIcon.SetActive(false);
        }

        playerSelected[curAction] = unit;
        playerSelected[curAction].transform.GetChild(0).GetComponent<Select_Unit>().hover = null;
        
        //Ativa o painel de comandos da unidade selecionada
        commandCard.SetActive(true);
    }

    //Ao clicar no bot�o de ataque vai liberar para selecionar o inimigo
    public void AttackButton()
    {
        //Libera a sele��o de inimigos
        selectingPlayer = false;
        targetingEnemy = true;
        targetingPlayer = false;

        //Seleciona a a��o e altera os icones
        playerAction[curAction] = BattleAction.Attack;
        playerActionIcon[curAction].GetComponent<Action_Icons>().ChangeIcon(playerAction[curAction], playerSelected[curAction].GetComponent<Unit_Info>().unitColor, Color.white);
        commandCard.SetActive(false);
    }

    //Jogador seleciona uma unidade inimiga caso a a��o necessite
    public void SelectTargetEnemy (GameObject unit)
    {
        //Seleciona a unidade e limpa o indicador de sele��o
        playerTarget[curAction] = unit;
        playerTarget[curAction].transform.GetChild(0).GetComponent<Select_Unit>().hover = null;
        playerSelected[curAction].transform.GetChild(0).GetComponent<Select_Unit>().isSelected = false;
        playerSelected[curAction].GetComponent<Unit_Info>().selectIcon.SetActive(false);
        playerTarget[curAction].GetComponent<Unit_Info>().selectIcon.SetActive(false);

        //Mostra o alvo que vai sofrer a a��o no icone
        playerActionIcon[curAction].GetComponent<Action_Icons>().ChangeIcon(playerAction[curAction], playerSelected[curAction].GetComponent<Unit_Info>().unitColor, playerTarget[curAction].GetComponent<Unit_Info>().unitColor);

        //Pula para a proxima a��o caso ainda falte
        if (curAction < maxActions-1)
        {
            selectingPlayer = true;
            targetingEnemy = false;
            targetingPlayer = false;
            curAction++;
        }
        else if (curAction == maxActions-1)
        {
            selectingPlayer = false;
            targetingEnemy = false;
            targetingPlayer = false;

            //Debloqueia o bot�o de iniciar o combate caso tenha preenchido todas a��es do turno
            battleButton.GetComponent<Button>().interactable = true;
        }
    }

    //Ao clicar no bot�o de defesa vai alterar a a��o atual e ir para a proxima
    public void DefendButton()
    {
        //Seleciona a unidade e limpa o indicador de sele��o
        playerSelected[curAction].transform.GetChild(0).GetComponent<Select_Unit>().hover = null;
        playerSelected[curAction].GetComponent<Unit_Info>().selectIcon.SetActive(false);
        playerSelected[curAction].transform.GetChild(0).GetComponent<Select_Unit>().isSelected = false;

        //Seleciona a a��o e altera os icones
        playerAction[curAction] = BattleAction.Defend;
        playerActionIcon[curAction].GetComponent<Action_Icons>().ChangeIcon(playerAction[curAction], playerSelected[curAction].GetComponent<Unit_Info>().unitColor, Color.white);
        commandCard.SetActive(false);


        //Pula para a proxima a��o caso ainda falte
        if (curAction < maxActions-1)
        {
            selectingPlayer = true;
            targetingEnemy = false;
            targetingPlayer = false;
            curAction++;
        }
        else if (curAction == maxActions-1)
        {
            selectingPlayer = false;
            targetingEnemy = false;
            targetingPlayer = false;

            //Debloqueia o bot�o de iniciar o combate caso tenha preenchido todas a��es do turno
            battleButton.GetComponent<Button>().interactable = true;
        }
    }

    public void MoveButton()
    {
        //Libera a sele��o de inimigos
        selectingPlayer = false;
        targetingPlayer = false;
        targetingPlayer = true;

        playerSelected[curAction].transform.GetChild(0).GetComponent<Select_Unit>().hover = null;
        playerSelected[curAction].GetComponent<Unit_Info>().selectIcon.SetActive(true);
        playerSelected[curAction].transform.GetChild(0).GetComponent<Select_Unit>().isSelected = true;

        //Seleciona a a��o e altera os icones
        playerAction[curAction] = BattleAction.Move;
        playerActionIcon[curAction].GetComponent<Action_Icons>().ChangeIcon(playerAction[curAction], playerSelected[curAction].GetComponent<Unit_Info>().unitColor, Color.white);
        commandCard.SetActive(false);
    }

    public void SelectTargetPlayer (GameObject unit)
    {
        //Seleciona a unidade e limpa o indicador de sele��o
        playerTarget[curAction] = unit;
        playerTarget[curAction].transform.GetChild(0).GetComponent<Select_Unit>().hover = null;
        playerSelected[curAction].transform.GetChild(0).GetComponent<Select_Unit>().isSelected = false;
        playerSelected[curAction].GetComponent<Unit_Info>().selectIcon.SetActive(false);
        playerTarget[curAction].GetComponent<Unit_Info>().selectIcon.SetActive(false);

        //Mostra o alvo que vai sofrer a a��o no icone
        playerActionIcon[curAction].GetComponent<Action_Icons>().ChangeIcon(playerAction[curAction], playerSelected[curAction].GetComponent<Unit_Info>().unitColor, playerTarget[curAction].GetComponent<Unit_Info>().unitColor);

        //Pula para a proxima a��o caso ainda falte
        if (curAction < maxActions - 1)
        {
            selectingPlayer = true;
            targetingEnemy = false;
            targetingPlayer = false;
            curAction++;
        }
        else if (curAction == maxActions - 1)
        {
            selectingPlayer = false;
            targetingEnemy = false;
            targetingPlayer = false;

            //Debloqueia o bot�o de iniciar o combate caso tenha preenchido todas a��es do turno
            battleButton.GetComponent<Button>().interactable = true;
        }
    }

    //Ao clicar no bot�o de batalha inicia o combate
    public void StartBattle()
    {
        isInBattle = true;
        curAction = 0;
        Actions();
    }

    //Caso seja impar vai fazer as a��es do inimigo, caso seja par vai fazer as do jogador e caso esteja no maximo bloqueia as a��es para fazer o proximo turno
    public void Actions()
    {
        
        if (curAction == maxActions && curMov == maxActions * 2)
        {
            Debug.Log("No actions nor moves");
            isInBattle = false;

            //A��es do inimigo
            EnemyAI();

            //Final do turno
            curMov = 0;
            curAction = 0;
            selectingPlayer = true;
            targetingEnemy = false;
            targetingPlayer = false;
        }

        if (isInBattle == true)
        {
            Debug.Log("Battling");
            curMov++;
            //Compara se � impar ou par para ver de quem � o turno
            if (curMov % 2 != 0)
            {
                Debug.Log("Player turn");
                for (int u = 0; u < playerUnits.Length; u++)
                {
                    if (playerUnits[u].GetComponent<Unit_Info>().isDead == false)
                    {
                        playerUnits[u].GetComponent<Unit_Info>().critChance += playerUnits[u].GetComponent<Unit_Info>().critScaling;

                        if (playerUnits[u].GetComponent<Unit_Info>().critChance > 100)
                            playerUnits[u].GetComponent<Unit_Info>().critChance = 100;
                    }
                }

                if (playerSelected[curAction].GetComponent<Unit_Info>().isDead == false)
                {
                    Debug.Log("PLayer doing Actions");
                    PlayerAction(curAction);
                }
                else
                {
                    Debug.Log("Player dead");
                    DeadActions();
                }
            }
            // Inimigo
            else
            {
                curAction++;

                if (enemySelected[curAction - 1].GetComponent<Unit_Info>().isDead == false)
                {
                    EnemyAction(curAction - 1);
                }
                else
                {
                    DeadActions();
                }
            }
        }
        
    }

    void DeadActions()
    {
        Actions();
    }

    void PlayerAction(int act)
    {
        playerActionIcon[act].GetComponent<Action_Icons>().ResetIcon();

        switch (playerAction[act])
        {
            case BattleAction.Attack:
                {
                    if (playerTarget[act].GetComponent<Unit_Info>().isDead == false)
                    {
                        playerSelected[act].GetComponent<Unit_Info>().animator.SetTrigger("attack");

                        bool defend;
                        if (enemyAction[act] == BattleAction.Defend && playerTarget[act] == enemySelected[act])
                            defend = true;
                        else
                            defend = false;


                        playerTarget[act].GetComponent<Unit_Info>().TakeDamage(playerSelected[act].GetComponent<Unit_Info>().attackDamage, defend);
                    }
                    break;
                }

            case BattleAction.Defend:
                {
                    Actions();
                    break;
                }

            case BattleAction.Move:
                {
                    playerSelected[act].GetComponent<Unit_Info>().navMesh.SetDestination(playerTarget[act].transform.position);
                    playerTarget[act].GetComponent<Unit_Info>().navMesh.SetDestination(playerSelected[act].transform.position);

                    playerSelected[act].GetComponent<Unit_Info>().critChance -= 20;
                    playerTarget[act].GetComponent<Unit_Info>().critChance -= 20;

                    playerSelected[act].GetComponent<Unit_Info>().animator.SetBool("move", true);
                    playerTarget[act].GetComponent<Unit_Info>().animator.SetBool("move", true);
                    break;
                }
        }
    }

    void EnemyAction(int act)
    {
        enemyActionIcon[act].GetComponent<Action_Icons>().ResetIcon();

        switch (enemyAction[act])
        {
            case BattleAction.Attack:
                {
                    if (enemyTarget[act].GetComponent<Unit_Info>().isDead == false)
                    {
                        enemySelected[act].GetComponent<Unit_Info>().animator.SetTrigger("attack");

                        bool defend;
                        if (playerAction[act] == BattleAction.Defend && playerSelected[act] == enemyTarget[act])
                            defend = true;
                        else
                            defend = false;

                        enemyTarget[act].GetComponent<Unit_Info>().TakeDamage(enemySelected[act].GetComponent<Unit_Info>().attackDamage, defend);
                    }
                    break;
                }

            case BattleAction.Defend:
                {
                    Actions();
                    break;
                }
        }
        enemyActionIcon[act].GetComponent<Action_Icons>().ResetIcon();
    }







    void EnemyAI()
    {
        for (int a = 0; a < maxActions; a++)
        {
            int rndAct = Random.Range(0, 100);
            if (rndAct <= 75)
            {
                enemyAction[a] = BattleAction.Attack;

                int selected = Random.Range(0, enemyUnits.Length);
                int target = Random.Range(0, playerUnits.Length);

                enemySelected[a] = enemyUnits[selected];
                enemyTarget[a] = playerUnits[target];

                if (enemySelected[a].GetComponent<Unit_Info>().isDead == true)
                {
                    for (int s = 0; s < enemyUnits.Length; s++)
                    {
                        if (enemyUnits[s].GetComponent<Unit_Info>().isDead == false)
                        {
                            enemySelected[a] = enemyUnits[s];
                            break;
                        }
                    }
                }

                if (enemyTarget[a].GetComponent<Unit_Info>().isDead == true)
                {
                    for (int t = 0; t < playerUnits.Length; t++)
                    {
                        if (playerUnits[t].GetComponent<Unit_Info>().isDead == false)
                        {
                            enemyTarget[a] = playerUnits[t];
                            break;
                        }
                    }
                }

                enemyActionIcon[a].GetComponent<Action_Icons>().ChangeIcon(enemyAction[a], enemySelected[a].GetComponent<Unit_Info>().unitColor, enemyTarget[a].GetComponent<Unit_Info>().unitColor);
            }
            else
            {
                enemyAction[a] = BattleAction.Defend;

                int selected = Random.Range(0, enemyUnits.Length);
                enemySelected[a] = enemyUnits[selected];

                if (enemySelected[a].GetComponent<Unit_Info>().isDead == true)
                {
                    for (int s = 0; s < enemyUnits.Length; s++)
                    {
                        if (enemyUnits[s].GetComponent<Unit_Info>().isDead == false)
                        {
                            enemySelected[a] = enemyUnits[s];
                            break;
                        }
                    }
                }

                enemyActionIcon[a].GetComponent<Action_Icons>().ChangeIcon(enemyAction[a], enemySelected[a].GetComponent<Unit_Info>().unitColor, Color.white);
            }
        }
    }
}