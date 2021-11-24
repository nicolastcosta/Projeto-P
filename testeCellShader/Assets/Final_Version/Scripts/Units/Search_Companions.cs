using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Search_Companions : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Unit_Info unitInfo;
            Unit_Companions unitComp;

            if (other.GetComponent<Unit_Info>() && other.GetComponent<Unit_Companions>())
            {
                unitInfo = other.GetComponent<Unit_Info>();
                unitComp = other.GetComponent<Unit_Companions>();

                if (unitComp.companion.Length > 0)
                {
                    for (int c = 0; c < unitComp.companion.Length; c++)
                    {
                        if (unitComp.companion[c] == null)
                        {
                            unitComp.companion[c] = gameObject;
                            GetComponent<Search_Companions>().enabled = false;
                            break;
                        }
                    }
                }
            }
        }
     }
}
