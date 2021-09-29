using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Minimap_Icon : MonoBehaviour
{
    [SerializeField]
    private GameObject minimapIcon;

    // Start is called before the first frame update
    private void Awake()
    {
        if (minimapIcon != null)
        {
            GameObject minimapTemp = Instantiate(minimapIcon, transform.position, Quaternion.identity) as GameObject;
            minimapTemp.transform.parent = transform;
        }
    }
}
