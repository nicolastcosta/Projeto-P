using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Life_Bar : MonoBehaviour
{
    public GameObject host;

    private Slider lifeBar;

    [SerializeField]
    private TextMeshProUGUI lifeText,critText;

    void Awake()
    {
        lifeBar = GetComponent<Slider>();

        // Error msg
        if (host == null)
            Debug.Log("No host");

        if (lifeText == null)
            Debug.Log("No life text");

        if (critText == null)
            Debug.Log("No crit text");

    }

    // Update is called once per frame
    void Update()
    {
        if (host != null)
        {
            int lifeMax = host.GetComponent<Unit_Info>().lifeMax;
            int lifeCur = host.GetComponent<Unit_Info>().lifeCur;

            lifeBar.maxValue = lifeMax;
            lifeBar.value = lifeCur;

            if(lifeText != null)
            {
                lifeText.text = (lifeCur.ToString() + " / " + lifeMax.ToString());
            }

            if (critText != null)
            {
                critText.text = ("Crit: " + host.GetComponent<Unit_Info>().critChance.ToString() + "%");
            }
        }
    }
}
