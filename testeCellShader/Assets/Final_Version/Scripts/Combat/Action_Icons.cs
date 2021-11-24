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
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void ChangeIcon(BattleAction action, Color unitColor, Color targetColor)
    {
        hasAction = true;
        icon.color = unitColor;
        switch (action)
        {
            case BattleAction.Attack:
                {
                    targetIcon.SetActive(true);
                    targetIcon.GetComponent<Image>().color = targetColor;

                    if (icon.sprite != actionIcons[0])
                        animator.Play("New");

                    icon.sprite = actionIcons[0];
                    break;
                }

            case BattleAction.Defend:
                {
                    icon.sprite = actionIcons[1];
                    targetIcon.SetActive(false);

                    animator.Play("New");
                    break;
                }

            case BattleAction.Move:
                {
                    targetIcon.SetActive(true);
                    targetIcon.GetComponent<Image>().color = targetColor;

                    if (icon.sprite != actionIcons[2])
                        animator.Play("New");

                    icon.sprite = actionIcons[2];
                    break;
                }
        }
    }

    public void ResetIcon()
    {
        icon.sprite = actionIcons[3];
        icon.color = Color.white;
        targetIcon.SetActive(false);

        animator.Play("New");
    }

    public void HideIcon()
    {
        animator.Play("Use");
    }
}
