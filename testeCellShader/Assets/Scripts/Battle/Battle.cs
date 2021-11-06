using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleAction
{
    None,
    Attack,
    Defend,
    Move,
    Spell
}

public class Battle : MonoBehaviour
{
    [Header("-> Actions per Turn <-")]
    [SerializeField]
    private int maxActions;
    public int curAction;
    public int curMov;

    [Header("-> Units <-")]
    [SerializeField]
    private GameObject[] playerUnits;

    [SerializeField]
    private GameObject[] enemyUnits;

    [Header("-> Player Actions <-")]
    public BattleAction[] playerAction;

    [SerializeField]
    private GameObject[] playerSelected, playerTarget;

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
    public bool selecting = true;
    public bool targeting, isInBattle;

    [Header("-> UI Elements <-")]
    [SerializeField]
    private GameObject commandCard;

    [SerializeField]
    private GameObject battleButton;

    // Start is called before the first frame update
    void Start()
    {
        //Primeiras ações do inimigo
        EnemyAI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Jogador seleciona uma unidade dele
    public void SelectUnit(GameObject unit)
    {
        //Caso seja a primeira seleção não vai fazer nada, mas se já tiver selecionado antes, vai descelecionar a antiga para selecionar a nova
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

    //Ao clicar no botão de ataque vai liberar para selecionar o inimigo
    public void AttackButton()
    {
        //Libera a seleção de inimigos
        selecting = false;
        targeting = true;

        //Seleciona a ação e altera os icones
        playerAction[curAction] = BattleAction.Attack;
        playerActionIcon[curAction].GetComponent<Action_Icons>().ChangeIcon(playerAction[curAction], playerSelected[curAction].GetComponent<Unit_Info>().unitColor, Color.white);
        commandCard.SetActive(false);
    }

    //Jogador seleciona uma unidade inimiga caso a ação necessite
    public void SelectTarget(GameObject unit)
    {
        //Seleciona a unidade e limpa o indicador de seleção
        playerTarget[curAction] = unit;
        playerTarget[curAction].transform.GetChild(0).GetComponent<Select_Unit>().hover = null;
        playerSelected[curAction].transform.GetChild(0).GetComponent<Select_Unit>().isSelected = false;
        playerSelected[curAction].GetComponent<Unit_Info>().selectIcon.SetActive(false);
        playerTarget[curAction].GetComponent<Unit_Info>().selectIcon.SetActive(false);

        //Libera o botão para alterar a ação caso tenha errado
        //playerActionIcon[curAction].GetComponent<Button>().interactable = true;

        //Mostra o alvo que vai sofrer a ação no icone
        playerActionIcon[curAction].GetComponent<Action_Icons>().ChangeIcon(playerAction[curAction], playerSelected[curAction].GetComponent<Unit_Info>().unitColor, playerTarget[curAction].GetComponent<Unit_Info>().unitColor);

        //Pula para a proxima ação caso ainda falte
        if (curAction < maxActions-1)
        {
            selecting = true;
            targeting = false;
            curAction++;
        }
        else if (curAction == maxActions-1)
        {
            selecting = false;
            targeting = false;

            //Debloqueia o botão de iniciar o combate caso tenha preenchido todas ações do turno
            battleButton.GetComponent<Button>().interactable = true;
        }
    }

    //Ao clicar no botão de defesa vai alterar a ação atual e ir para a proxima
    public void DefendButton()
    {
        //Seleciona a unidade e limpa o indicador de seleção
        playerSelected[curAction].transform.GetChild(0).GetComponent<Select_Unit>().hover = null;
        playerSelected[curAction].GetComponent<Unit_Info>().selectIcon.SetActive(false);
        playerSelected[curAction].transform.GetChild(0).GetComponent<Select_Unit>().isSelected = false;

        //Seleciona a ação e altera os icones
        playerAction[curAction] = BattleAction.Defend;
        playerActionIcon[curAction].GetComponent<Action_Icons>().ChangeIcon(playerAction[curAction], playerSelected[curAction].GetComponent<Unit_Info>().unitColor, Color.white);
        commandCard.SetActive(false);

        //Libera o botão para alterar a ação caso tenha errado
        //playerActionIcon[curAction].GetComponent<Button>().interactable = true;

        //Pula para a proxima ação caso ainda falte
        if (curAction < maxActions-1)
        {
            selecting = true;
            targeting = false;
            curAction++;
        }
        else if (curAction == maxActions-1)
        {
            selecting = false;
            targeting = false;

            //Debloqueia o botão de iniciar o combate caso tenha preenchido todas ações do turno
            battleButton.GetComponent<Button>().interactable = true;
        }
    }

    //Ao clicar no botão de batalha inicia o combate
    public void StartBattle()
    {
        isInBattle = true;
        curAction = 0;
        Actions();
    }

    //Faz as ações com timers para as ações.
    public void Actions()
    {
        
        if (curAction == maxActions && curMov == maxActions * 2)
        {
            isInBattle = false;

            //Ações do inimigo
            EnemyAI();

            //Final do turno
            curMov = 0;
            curAction = 0;
            selecting = true;
            targeting = false;
        }

        if (isInBattle == true)
        {
            curMov++;
            //Compara se é impar ou par para ver de quem é o turno
            if (curMov % 2 != 0)
            {
                if (playerSelected[curAction].GetComponent<Unit_Info>().isDead == false)
                {
                    PlayerAction(curAction);
                }
                else
                {
                    StartCoroutine(DeadActions());
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
                    StartCoroutine(DeadActions());
                }
            }
        }
        
    }

    IEnumerator DeadActions()
    {
        yield return new WaitForSeconds(0.5f);
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