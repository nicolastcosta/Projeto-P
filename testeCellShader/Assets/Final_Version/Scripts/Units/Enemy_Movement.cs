using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Movement : MonoBehaviour
{
    private Vector3 startPosition;

    private NavMeshAgent navMesh;
    private Unit_Info unitInfo;

    [SerializeField]
    private float wanderArea;

    [SerializeField]
    private float wanderDelay;
    private float timer;
    private bool wandering = false;


    // Start is called before the first frame update
    void Awake()
    {
        startPosition = transform.position;

        if (GetComponent<NavMeshAgent>())
            navMesh = GetComponent<NavMeshAgent>();
        else
            Debug.Log("Enemy movment needs a Nav Mesh Agent");

        if (GetComponent<Unit_Info>())
            unitInfo = GetComponent<Unit_Info>();
        else
            Debug.Log("Enemy movment needs a Unit info script");
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<NavMeshAgent>())
        {
            navMesh.speed = unitInfo.speed;
            navMesh.angularSpeed = unitInfo.turnRate;


            if (wandering == false)
            {
                if (timer < wanderDelay)
                {
                    timer += 1 * Time.deltaTime;
                }
                else
                {
                    timer = 0;

                    float randomX = Random.Range(wanderArea * -1, wanderArea);
                    float randomZ = Random.Range(wanderArea * -1, wanderArea);

                    Vector3 newPosition = new Vector3(startPosition.x + randomX, startPosition.y, startPosition.z + randomZ);

                    navMesh.SetDestination(newPosition);
                }
            }

            if (navMesh.remainingDistance > navMesh.stoppingDistance)
            {
                unitInfo.animator.SetBool("walking", true);
                wandering = true;
            }
            else
            {
                unitInfo.animator.SetBool("walking", false);
                wandering = false;
            }
        }
    }
}
