using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Action_Icons : MonoBehaviour
{
    public Image icon;
    public GameObject targetIcon;

    [SerializeField]
    private Sprite[] actionIcons;

    private bool hasAction;

    public void ChangeIcon(BattleAction action, Color unitColor, Color targetColor)
    {
        hasAction = true;
        icon.color = unitColor;
        switch (action)
        {
            case BattleAction.Attack:
                {
                    icon.sprite = actionIcons[0];

                    targetIcon.SetActive(true);
                    targetIcon.GetComponent<Image>().color = targetColor;
                    break;
                }

            case BattleAction.Defend:
                {
                    icon.sprite = actionIcons[1];
                    targetIcon.SetActive(false);
                    break;
                }

            case BattleAction.Move:
                {
                    icon.sprite = actionIcons[2];
                    targetIcon.SetActive(true);
                    targetIcon.GetComponent<Image>().color = targetColor;
                    break;
                }
        }
    }

    public void ResetIcon()
    {
        icon.sprite = actionIcons[3];
        icon.color = Color.white;
        targetIcon.SetActive(false);
    }
}
