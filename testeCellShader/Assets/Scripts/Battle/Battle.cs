using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleAction
{
    Attack,
    Defend,
    Move,
    Spell
}

public class Battle : MonoBehaviour
{
    [SerializeField]
    private GameObject[] playerUnits, enemyUnits, playerSelected, playerTarget, enemySelected, enemyTarget, playerActionIcon, enemyActionIcon;

    public bool selecting = true;
    public bool targeting;

    [SerializeField]
    private GameObject commandCard, battleButton;

    public BattleAction[] playerAction, enemyAction;
    public int action;

    // Start is called before the first frame update
    void Start()
    {
        EnemyAI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Selecionado o jogador
    public void SelectUnit(GameObject unit)
    {
        if (playerSelected[action] != null)
        {
            playerSelected[action].transform.GetChild(0).GetComponent<Select_Unit>().isSelected = false;
            playerSelected[action].transform.GetChild(0).GetComponent<Select_Unit>().rend.material.color = playerSelected[action].transform.GetChild(0).GetComponent<Select_Unit>().col;
        }

        playerSelected[action] = unit;
        playerSelected[action].transform.GetChild(0).GetComponent<Select_Unit>().hover = null;

        commandCard.SetActive(true);
    }

    public void SelectTarget(GameObject unit)
    {
        playerTarget[action] = unit;
        playerTarget[action].transform.GetChild(0).GetComponent<Select_Unit>().hover = null;

        playerSelected[action].transform.GetChild(0).GetComponent<Select_Unit>().rend.material.color = playerSelected[action].transform.GetChild(0).GetComponent<Select_Unit>().col;
        playerSelected[action].transform.GetChild(0).GetComponent<Select_Unit>().isSelected = false;

        playerActionIcon[action].GetComponent<Button>().interactable = true;
        playerActionIcon[action].GetComponent<Action_Icons>().ChangeIcon(playerAction[action], playerSelected[action].GetComponent<Unit_Info>().unitColor, playerTarget[action].GetComponent<Unit_Info>().unitColor);

        if (action < 5)
        {
            selecting = true;
            targeting = false;
            action++;
        }
        else if (action == 5)
        {
            selecting = false;
            targeting = false;
            battleButton.GetComponent<Button>().interactable = true;
        }
    }

    //ao clicar no botao de ataque vai liberar para selecionar o inimigo
    public void AttackButton()
    {
        selecting = false;
        targeting = true;
        playerAction[action] = BattleAction.Attack;
        playerActionIcon[action].GetComponent<Action_Icons>().ChangeIcon(playerAction[action], playerSelected[action].GetComponent<Unit_Info>().unitColor, Color.white);
        commandCard.SetActive(false);
    }

    public void DefendButton()
    {
        playerSelected[action].transform.GetChild(0).GetComponent<Select_Unit>().hover = null;
        playerSelected[action].transform.GetChild(0).GetComponent<Select_Unit>().rend.material.color = playerSelected[action].transform.GetChild(0).GetComponent<Select_Unit>().col;
        playerSelected[action].transform.GetChild(0).GetComponent<Select_Unit>().isSelected = false;

        playerAction[action] = BattleAction.Defend;
        playerActionIcon[action].GetComponent<Action_Icons>().ChangeIcon(playerAction[action], playerSelected[action].GetComponent<Unit_Info>().unitColor, Color.white);
        commandCard.SetActive(false);

        playerActionIcon[action].GetComponent<Button>().interactable = true;

        if (action < 5)
        {
            selecting = true;
            targeting = false;
            action++;
        }
        else if (action == 5)
        {
            selecting = false;
            targeting = false;
            battleButton.GetComponent<Button>().interactable = true;
        }
    }

    public void StartBattle()
    {
        StartCoroutine(Actions());
    }

    IEnumerator Actions()
    {
        for (int a = 0; a <= 5; a++)
        {
            if (playerSelected[a].GetComponent<Unit_Info>().isDead == false)
            {
                PlayerAction(a);
                if (enemyAction[a] != BattleAction.Defend)
                    yield return new WaitForSeconds(2.5f);
            }
            playerActionIcon[a].GetComponent<Action_Icons>().ResetIcon();

            if (enemySelected[a].GetComponent<Unit_Info>().isDead == false)
            {
                EnemyAction(a);
                if (playerAction[a] != BattleAction.Defend)
                    yield return new WaitForSeconds(2.5f);
            }
            enemyActionIcon[a].GetComponent<Action_Icons>().ResetIcon();
        }

        action = 0;
        selecting = true;
        targeting = false;

        EnemyAI();
    }

    void PlayerAction(int act)
    {
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
                    break;
                }
        }
    }

    void EnemyAction(int act)
    {
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
                    break;
                }
        }
        enemyActionIcon[act].GetComponent<Action_Icons>().ResetIcon();
    }

    void EnemyAI()
    {
        for (int a = 0; a < 6; a++)
        {
            int rndAct = Random.Range(0, 100);
            if (rndAct <= 75)
            {
                enemyAction[a] = BattleAction.Attack;

                int selected = Random.Range(0, 3);
                int target = Random.Range(0, 3);

                enemySelected[a] = enemyUnits[selected];
                enemyTarget[a] = playerUnits[target];

                if (enemySelected[a].GetComponent<Unit_Info>().isDead == true)
                {
                    for (int s = 0; s < 3; s++)
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
                    for (int t = 0; t < 3; t++)
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

                int selected = Random.Range(0, 3);
                enemySelected[a] = enemyUnits[selected];

                if (enemySelected[a].GetComponent<Unit_Info>().isDead == true)
                {
                    for (int s = 0; s < 3; s++)
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