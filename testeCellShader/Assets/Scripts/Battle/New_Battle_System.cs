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

public class New_Battle_System : MonoBehaviour
{
    public int currentTurn;
    
    [Header("-> Turn Actions <-")]
    public int maximumActions;
    public int currentAction;

    [Header("-> Units <-")]
    public GameObject[] playerUnits;
    public GameObject[] enemyUnits;

    [Header("-> Selections and Targets <-")]
    public BattleAction[] actions;

    public GameObject[] selecteds, targets;

    [Header("-> Action Icons <-")]
    [SerializeField]
    private GameObject[] actionIcon;

    [Header("-> Battle States <-")]
    public bool playerIsSelecting = true;
    public bool playerIsTargetingEnemy, playerIsTargetingSelf, isInBattle;

    [Header("-> UI Elements <-")]
    [SerializeField]
    private GameObject commandCard;

    [SerializeField]
    private GameObject battleButton;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Select player unit to order a action
    public void SelectUnit(GameObject unit)
    {
        // Caso já tiver selecionado uma unidade, vai descelecionar ela
        if (selecteds[currentAction] != null)
        {
            selecteds[currentAction].transform.GetChild(0).GetComponent<Select_Unit>().isSelected = false;
            selecteds[currentAction].GetComponent<Unit_Info>().selectIcon.SetActive(false);
        }

        // Select the unit
        selecteds[currentAction] = unit;
        selecteds[currentAction].transform.GetChild(0).GetComponent<Select_Unit>().hover = null;

        // Ativa o painel de comandos da unidade selecionada
        commandCard.SetActive(true);
    }

    // Enable the battle button
    void EnableBattle()
    {
        playerIsSelecting = false;
        playerIsTargetingEnemy = false;
        playerIsTargetingSelf = false;

        battleButton.GetComponent<Button>().interactable = true;
    }

    // Change the action to Attack and unlocks the enemy selection
    public void AttackButton()
    {
        // Unlocks the enemy selection
        playerIsSelecting = false;
        playerIsTargetingEnemy = true;
        playerIsTargetingSelf = false;

        // Select the action and change the icon
        actions[currentAction] = BattleAction.Attack;
        actionIcon[currentAction].GetComponent<Action_Icons>().ChangeIcon(actions[currentAction], selecteds[currentAction].GetComponent<Unit_Info>().unitColor, Color.white);
        commandCard.SetActive(false);
    }

    // Select the enemy and jumps to the next action if is possible
    public void SelectTargetEnemy(GameObject unit)
    {
        // Select the target and clear the indicator
        targets[currentAction] = unit;
        targets[currentAction].transform.GetChild(0).GetComponent<Select_Unit>().hover = null;
        selecteds[currentAction].transform.GetChild(0).GetComponent<Select_Unit>().isSelected = false;

        selecteds[currentAction].GetComponent<Unit_Info>().selectIcon.SetActive(false);
        targets[currentAction].GetComponent<Unit_Info>().selectIcon.SetActive(false);

        // Update the action icon to show the target
        actionIcon[currentAction].GetComponent<Action_Icons>().ChangeIcon(actions[currentAction], selecteds[currentAction].GetComponent<Unit_Info>().unitColor, targets[currentAction].GetComponent<Unit_Info>().unitColor);

        // Changes the current action or enable the battle button
        if (currentAction < maximumActions - 2)
        {
            playerIsSelecting = true;
            playerIsTargetingEnemy = false;
            playerIsTargetingSelf = false;
            currentAction += 2;
        }
        else if (currentAction == maximumActions - 2)
        {
            EnableBattle();
        }
    }

    // Changes the curret action to Defend and jumps to the next action if is possible
    public void DefendButton()
    {
        // Select the unit and clear the indicator
        selecteds[currentAction].transform.GetChild(0).GetComponent<Select_Unit>().hover = null;
        selecteds[currentAction].GetComponent<Unit_Info>().selectIcon.SetActive(false);
        selecteds[currentAction].transform.GetChild(0).GetComponent<Select_Unit>().isSelected = false;

        // Select the action and change the icon
        actions[currentAction] = BattleAction.Defend;
        actionIcon[currentAction].GetComponent<Action_Icons>().ChangeIcon(actions[currentAction], selecteds[currentAction].GetComponent<Unit_Info>().unitColor, Color.white);
        commandCard.SetActive(false);


        // Changes the current action or enable the battle button
        if (currentAction < maximumActions - 2)
        {
            playerIsSelecting = true;
            playerIsTargetingEnemy = false;
            playerIsTargetingSelf = false;
            currentAction += 2;
        }
        else if (currentAction == maximumActions - 2)
        {
            EnableBattle();
        }
    }

    // Starts the Battle
    public void StartBattle()
    {
        isInBattle = true;
        currentAction = 0;
        Actions(currentAction);
    }

    public void Actions(int action)
    {
        currentAction++;

        if (currentAction == maximumActions)
        {
            Debug.Log("End Battle");
            isInBattle = false;

            //Ações do inimigo
            //EnemyAI();

            //Final do turno
            currentAction = 0;

            playerIsSelecting = true;
            playerIsTargetingEnemy = false;
            playerIsTargetingSelf = false;
        }

        if (isInBattle == true && currentAction < maximumActions)
        {
            // Compara se é impar ou par para ver de quem é o turno
            if (action % 2 == 0)
            {
                // Increases the crit chance of all player units
                for (int u = 0; u < playerUnits.Length; u++)
                {
                    if (playerUnits[u].GetComponent<Unit_Info>().isDead == false)
                    {
                        playerUnits[u].GetComponent<Unit_Info>().critChance += playerUnits[u].GetComponent<Unit_Info>().critScaling;

                        if (playerUnits[u].GetComponent<Unit_Info>().critChance > 100)
                            playerUnits[u].GetComponent<Unit_Info>().critChance = 100;
                    }
                }
            }
            
            if (selecteds[action].GetComponent<Unit_Info>().isDead == false)
            {
                Debug.Log("PLayer doing Actions");
                //actions(currentAction);
            }
            else
            {
                Debug.Log("Player dead");
                //DeadActions();
            }

            /*else
            {
                Debug.Log("Enemy turn");
                if (enemySelected[curAction].GetComponent<Unit_Info>().isDead == false)
                {
                    EnemyAction(curAction);
                }
                else
                {
                    //DeadActions();
                }
            }

            currentAction++;*/
        }

    }
}
