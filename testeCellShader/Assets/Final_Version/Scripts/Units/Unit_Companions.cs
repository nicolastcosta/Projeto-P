using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit_Companions : MonoBehaviour
{
    public GameObject[] companion;

    [SerializeField]
    private Transform[] target;

    // Update is called once per frame
    void Update()
    {
        if (companion.Length > 0)
        {
            for (int index = 0; index < companion.Length; index++)
            {
                if (companion[index] != null)
                {
                    UpdateVariables(index);
                    Move(index);
                }
            }
        }
    }

    //Aqui atualiza o NavMesh para as opções base da base da Unidade
    void UpdateVariables(int index)
    {
        if (companion[index].GetComponent<NavMeshAgent>() && companion[index].GetComponent<Unit_Info>())
        {
            NavMeshAgent navM = companion[index].GetComponent<NavMeshAgent>();
            Unit_Info unitInfo = companion[index].GetComponent<Unit_Info>();

            navM.speed = unitInfo.speed;
            navM.angularSpeed = unitInfo.turnRate;
        }
    }

    void Move(int index)
    {
        if (companion[index].GetComponent<NavMeshAgent>() && companion[index].GetComponent<Unit_Info>())
        {
            NavMeshAgent navM = companion[index].GetComponent<NavMeshAgent>();
            Unit_Info unitInfo = companion[index].GetComponent<Unit_Info>();

            if (target[index] != null)
                navM.SetDestination(target[index].position);
            else
                navM.SetDestination(transform.position);

            if (navM.remainingDistance > navM.stoppingDistance)
                unitInfo.animator.SetBool("walking", true);
            else
                unitInfo.animator.SetBool("walking", false);
        }
    }
}
