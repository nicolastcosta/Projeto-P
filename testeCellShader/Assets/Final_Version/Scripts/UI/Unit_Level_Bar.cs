using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Unit_Level_Bar : MonoBehaviour
{
    [SerializeField]
    private GameObject unit;

    [SerializeField]
    private Image levelBar;

    [SerializeField]
    private TextMeshProUGUI levelText;

    private Unit_Info unitInfo;

    private void Awake()
    {
        if (unit != null)
        {
            if (unit.GetComponent<Unit_Info>())
                unitInfo = unit.GetComponent<Unit_Info>();
            else
                Debug.Log("The unit of Unit Level Bar needs Unit Info script");

            if (levelBar == null)
                Debug.Log("Unit Level Bar needs a level bar");

            if (levelText == null)
                Debug.Log("Unit Level Bar needs a level text");
        }
        else
            Debug.Log("Unit level bar has no unit reference");
    }

    // Update is called once per frame
    void Update()
    {
        if (unit != null)
        {
            if (unit.GetComponent<Unit_Info>() && levelBar != null && levelText != null)
            {
                levelBar.fillAmount = ((float)unitInfo.curExp / (float)unitInfo.maxExp[unitInfo.unitLevel - 1]);
                levelText.text = unitInfo.unitLevel.ToString();
            }
        }
    }
}
