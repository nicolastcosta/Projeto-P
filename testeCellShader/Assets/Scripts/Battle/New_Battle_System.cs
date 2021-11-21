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
    private float currentTimer;
    public float timer;
    
    [Header("-> Turn Actions <-")]
    public int maximumActions;
    public int currentAction;

    [Header("-> Units <-")]
    public GameObject[] playerUnitsPosition;
    public GameObject[] enemyUnitsPosition;

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
        // First Enemy actions
        EnemyAI();
    }

    // Update
    private void Update()
    {
        NoActionTimer();
    }

    // Select player unit to order a action
    public void SelectUnit(GameObject unitPosition)
    {
        // Caso já tiver selecionado uma unidade, vai descelecionar ela
        if (selecteds[currentAction] != null)
        {
            selecteds[currentAction].GetComponent<Select_Unit>().isSelected = false;
            selecteds[currentAction].GetComponent<Select_Unit>().unitInPos.GetComponent<Unit_Info>().selectIcon.SetActive(false);
        }

        // Select the unit
        selecteds[currentAction] = unitPosition;
        selecteds[currentAction].GetComponent<Select_Unit>().hover = null;

        // Activates the player commandcard
        commandCard.SetActive(true);
    }

    // Select the enemy and jumps to the next action if is possible
    public void SelectEnemyUnit(GameObject unitPosition)
    {
        // Select the target and clear the indicator
        targets[currentAction] = unitPosition;
        targets[currentAction].GetComponent<Select_Unit>().hover = null;
        selecteds[currentAction].GetComponent<Select_Unit>().isSelected = false;

        selecteds[currentAction].GetComponent<Select_Unit>().unitInPos.GetComponent<Unit_Info>().selectIcon.SetActive(false);
        targets[currentAction].GetComponent<Select_Unit>().unitInPos.GetComponent<Unit_Info>().selectIcon.SetActive(false);

        // Update the action icon to show the target
        ChangeActionIcon(currentAction);

        // Changes the current action or enable the battle button
        EnableBattle();
    }

    // Select one of your units and jumps to the next action if is possible
    public void SelectPlayerUnit(GameObject unitPosition)
    {
        //Seleciona a unidade e limpa o indicador de seleção
        targets[currentAction] = unitPosition;

        targets[currentAction].GetComponent<Select_Unit>().hover = null;

        selecteds[currentAction].GetComponent<Select_Unit>().isSelected = false;

        selecteds[currentAction].GetComponent<Select_Unit>().unitInPos.GetComponent<Unit_Info>().selectIcon.SetActive(false);
        targets[currentAction].GetComponent<Select_Unit>().unitInPos.GetComponent<Unit_Info>().selectIcon.SetActive(false);

        // Update the action icon to show the target
        ChangeActionIcon(currentAction);

        // Changes the current action or enable the battle button
        EnableBattle();
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

        ChangeActionIcon(currentAction);

        commandCard.SetActive(false);
    }

    // Changes the curret action to Defend and jumps to the next action if is possible
    public void DefendButton()
    {
        // Select the unit and clear the indicator
        selecteds[currentAction].GetComponent<Select_Unit>().hover = null;
        selecteds[currentAction].GetComponent<Select_Unit>().isSelected = false;

        selecteds[currentAction].GetComponent<Select_Unit>().unitInPos.GetComponent<Unit_Info>().selectIcon.SetActive(false);

        // Select the action and change the icon
        actions[currentAction] = BattleAction.Defend;

        ChangeActionIcon(currentAction);

        commandCard.SetActive(false);

        // Changes the current action or enable the battle button
        EnableBattle();
    }

    // Change the action to Move and unlocks the your own units selection
    public void MoveButton()
    {
        // Enable the selection
        playerIsSelecting = false;
        playerIsTargetingEnemy = false;
        playerIsTargetingSelf = true;

        selecteds[currentAction].GetComponent<Select_Unit>().hover = null;
        selecteds[currentAction].GetComponent<Select_Unit>().isSelected = true;

        selecteds[currentAction].GetComponent<Select_Unit>().unitInPos.GetComponent<Unit_Info>().selectIcon.SetActive(true);

        // Select the action and change the icon
        actions[currentAction] = BattleAction.Move;

        ChangeActionIcon(currentAction);

        commandCard.SetActive(false);
    }

    // Enable the battle button if is possible
    void EnableBattle()
    {
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
            playerIsSelecting = false;
            playerIsTargetingEnemy = false;
            playerIsTargetingSelf = false;

            battleButton.GetComponent<Button>().interactable = true;
        }
    }

    // Starts the Battle
    public void StartBattle()
    {
        isInBattle = true;
        currentAction = 0;
        Actions(currentAction);
    }

    public void ChangeActionIcon(int action)
    {
        if (targets[action] != null)
        {
            actionIcon[action].GetComponent<Action_Icons>().ChangeIcon(actions[action], selecteds[action].GetComponent<Select_Unit>().posColor, targets[action].GetComponent<Select_Unit>().posColor);
        }
        else
            actionIcon[action].GetComponent<Action_Icons>().ChangeIcon(actions[action], selecteds[action].GetComponent<Select_Unit>().posColor, Color.white);
    }

    public void Actions(int action)
    {
        currentTimer = 0;

        if (action < maximumActions)
        {
            currentAction++;
            actionIcon[action].GetComponent<Action_Icons>().HideIcon();
        }

        if (action == maximumActions)
        {
            Debug.Log("End Battle");
            isInBattle = false;

            // Reset player actions
            for (int a = 0; a <= maximumActions -2; a += 2)
            {
                actions[a] = BattleAction.None;
                selecteds[a] = null;
                targets[a] = null;
            }

            for (int a = 0; a < maximumActions; a++)
            {
                actionIcon[a].GetComponent<Action_Icons>().ResetIcon();
            }

            playerIsSelecting = true;
            playerIsTargetingEnemy = false;
            playerIsTargetingSelf = false;

            currentAction = 0;

            // Enemy actions
            EnemyAI();
        }

        if (isInBattle == true)
        {
            // Compara se é impar ou par para ver de quem é o turno
            if (action % 2 == 0)
            {
                // Increases the crit chance of all player units
                for (int u = 0; u < playerUnitsPosition.Length; u++)
                {
                    if (playerUnitsPosition[u].GetComponent<Select_Unit>().unitInPos.GetComponent<Unit_Info>().isDead == false)
                    {
                        playerUnitsPosition[u].GetComponent<Select_Unit>().unitInPos.GetComponent<Unit_Info>().critChance += playerUnitsPosition[u].GetComponent<Select_Unit>().unitInPos.GetComponent<Unit_Info>().critScaling;

                        if (playerUnitsPosition[u].GetComponent<Select_Unit>().unitInPos.GetComponent<Unit_Info>().critChance > 100)
                            playerUnitsPosition[u].GetComponent<Select_Unit>().unitInPos.GetComponent<Unit_Info>().critChance = 100;
                    }
                }
            }
            
            if (selecteds[action].GetComponent<Select_Unit>().unitInPos.GetComponent<Unit_Info>().isDead == false)
            {
                switch (actions[action])
                {
                    case BattleAction.Attack:
                        {
                            if (targets[action].GetComponent<Select_Unit>().unitInPos.GetComponent<Unit_Info>().isDead == false)
                            {
                                selecteds[action].GetComponent<Select_Unit>().unitInPos.GetComponent<Unit_Info>().animator.SetTrigger("attack");

                                bool defend;
                                
                                int a = 1;
                                if (action % 2 != 0)
                                    a = -1;

                                if (actions[action + a] == BattleAction.Defend)
                                    defend = true;
                                else
                                    defend = false;

                                int damageTemp = selecteds[action].GetComponent<Select_Unit>().unitInPos.GetComponent<Unit_Info>().damage;
                                float critChanceTemp = selecteds[action].GetComponent<Select_Unit>().unitInPos.GetComponent<Unit_Info>().critChance;
                                float critMultTemp = selecteds[action].GetComponent<Select_Unit>().unitInPos.GetComponent<Unit_Info>().critDamageMult;

                                targets[action].GetComponent<Select_Unit>().unitInPos.GetComponent<Unit_Info>().TakeDamage(damageTemp, critChanceTemp, critMultTemp, defend);
                            }
                            break;
                        }

                    case BattleAction.Defend:
                        {
                            NoAction();
                            break;
                        }

                    case BattleAction.Move:
                        {
                            Select_Unit selectedUnit = selecteds[action].GetComponent<Select_Unit>();
                            Select_Unit targetUnit = targets[action].GetComponent<Select_Unit>();

                            Unit_Info selectedUnitInfo = selectedUnit.unitInPos.GetComponent<Unit_Info>();
                            Unit_Info targetUnitInfo = targetUnit.unitInPos.GetComponent<Unit_Info>();

                            if (!targetUnitInfo.isDead)
                            {
                                // Play animation
                                selectedUnitInfo.animator.SetTrigger("move");
                                targetUnitInfo.animator.SetTrigger("move");

                                // Change the units positions
                                Vector3 transTemp = selectedUnit.unitInPos.transform.position;

                                selectedUnit.unitInPos.transform.position = targetUnit.unitInPos.transform.position;
                                targetUnit.unitInPos.transform.position = transTemp;

                                // Change the targets
                                GameObject posTemp = selectedUnit.unitInPos;

                                selectedUnit.unitInPos = targetUnit.unitInPos;
                                targetUnit.unitInPos = posTemp;

                                // Change the position reference
                                GameObject unitTemp = selecteds[action];

                                selecteds[action] = targets[action];
                                targets[action] = unitTemp;

                                Debug.Log("Unidade 1: " + selectedUnit.unitInPos.name);
                                Debug.Log("Unidade 2: " + targetUnit.unitInPos.name);

                                //Reduces the crit change
                                selectedUnitInfo.critChance -= 25;

                                if (selectedUnitInfo.critChance < 0)
                                    selectedUnitInfo.critChance = 0;

                                targetUnitInfo.critChance -= 25;

                                if (targetUnitInfo.critChance < 0)
                                    targetUnitInfo.critChance = 0;
                            }
                            else
                            {
                                NoAction();
                            }
                            break;
                        }
                }
            }
            else
            {
                NoAction();
            }
        }
    }

    void NoAction()
    {
        Actions(currentAction);
    }

    // Time controller that keep the game going if has no actions left and still is in combat
    void NoActionTimer()
    {
        if (currentTimer >= timer && isInBattle == true)
        {
            currentTimer = 0;
            Actions(currentAction);
        }
        else if (currentTimer < timer && isInBattle == true)
        {
            currentTimer += 1 * Time.deltaTime;
        }
    }

    void EnemyAI()
    {
        for (int a = 1; a < maximumActions; a += 2)
        {
            int rndAct = Random.Range(0, 100);
            if (rndAct <= 75)
            {
                actions[a] = BattleAction.Attack;

                int selected = Random.Range(0, enemyUnitsPosition.Length);
                int target = Random.Range(0, playerUnitsPosition.Length);

                selecteds[a] = enemyUnitsPosition[selected];
                targets[a] = playerUnitsPosition[target];

                if (selecteds[a].GetComponent<Select_Unit>().unitInPos.GetComponent<Unit_Info>().isDead == true)
                {
                    for (int s = 0; s < enemyUnitsPosition.Length; s++)
                    {
                        if (enemyUnitsPosition[s].GetComponent<Select_Unit>().unitInPos.GetComponent<Unit_Info>().isDead == false)
                        {
                            selecteds[a] = enemyUnitsPosition[s];
                            break;
                        }
                    }
                }

                if (targets[a].GetComponent<Select_Unit>().unitInPos.GetComponent<Unit_Info>().isDead == true)
                {
                    for (int t = 0; t < playerUnitsPosition.Length; t++)
                    {
                        if (playerUnitsPosition[t].GetComponent<Select_Unit>().unitInPos.GetComponent<Unit_Info>().isDead == false)
                        {
                            targets[a] = playerUnitsPosition[t];
                            break;
                        }
                    }
                }

                ChangeActionIcon(a);
            }
            else
            {
                actions[a] = BattleAction.Defend;

                int selected = Random.Range(0, enemyUnitsPosition.Length);
                selecteds[a] = enemyUnitsPosition[selected];
                targets[a] = null;

                if (selecteds[a].GetComponent<Select_Unit>().unitInPos.GetComponent<Unit_Info>().isDead == true)
                {
                    for (int s = 0; s < enemyUnitsPosition.Length; s++)
                    {
                        if (enemyUnitsPosition[s].GetComponent<Select_Unit>().unitInPos.GetComponent<Unit_Info>().isDead == false)
                        {
                            selecteds[a] = enemyUnitsPosition[s];
                            break;
                        }
                    }
                }

                ChangeActionIcon(a);
            }
        }
    }
}
