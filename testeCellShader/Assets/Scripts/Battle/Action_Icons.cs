using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Action_Icons : MonoBehaviour
{
    public Image icon;

    [SerializeField]
    private Sprite[] actionIcons;

    private bool hasAction;

    public void ChangeIcon(BattleAction action)
    {
        hasAction = true;

        switch (action)
        {
            case BattleAction.Attack:
                {
                    icon.sprite = actionIcons[0];
                    break;
                }

            case BattleAction.Defend:
                {
                    icon.sprite = actionIcons[1];
                    break;
                }
        }
    }

    public void ResetIcon()
    {
        icon.sprite = actionIcons[2];
    }
}
