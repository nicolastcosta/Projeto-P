using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player_Companion : MonoBehaviour
{

    [SerializeField]
    private GameObject[] companion;

    [SerializeField]
    private Transform[] target;

    // Update is called once per frame
    void Update()
    {
        if (companion[0] != null)
        {
            for (int index = 0; index < companion.Length; index++)
            {
                UpdateVariables(index);
                Move(index);
            }
        }
    }

    //Aqui atualiza o NavMesh para as opções base da base da Unidade
    void UpdateVariables(int index)
    {
        companion[index].GetComponent<NavMeshAgent>().speed = companion[index].GetComponent<Unit_Info>().speed;
        companion[index].GetComponent<NavMeshAgent>().angularSpeed = companion[index].GetComponent<Unit_Info>().turnRate;
        companion[index].GetComponent<NavMeshAgent>().radius = companion[index].GetComponent<CapsuleCollider>().radius;
        companion[index].GetComponent<NavMeshAgent>().height = companion[index].GetComponent<CapsuleCollider>().height;
    }

    void Move(int index)
    {
        companion[index].GetComponent<NavMeshAgent>().SetDestination(target[index].position);

        if (companion[index].GetComponent<NavMeshAgent>().remainingDistance > companion[index].GetComponent<NavMeshAgent>().stoppingDistance)
        {
            companion[index].GetComponent<Unit_Info>().animator.SetBool("running", true);
        }
        else
        {
            companion[index].GetComponent<Unit_Info>().animator.SetBool("running", false);
        }
    }
}
