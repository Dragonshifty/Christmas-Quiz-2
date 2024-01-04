using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCMover : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    [SerializeField] List<Transform> wayPoints;
    System.Random rand = new System.Random();
    int wayPointListCount;
    [SerializeField] int waitTime = 10;
    bool doSpecial = false;
    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        wayPointListCount = wayPoints.Count;
    }

    private void Start() 
    {
        StartCoroutine(MoveNPC());  
    }
    void Update()
    {
        UpdateAnimator();
    }

    

    private IEnumerator MoveNPC()
    {
        while (true)
        {
            int waiting = rand.Next(waitTime);
            int destinationSelectionIndex = rand.Next(wayPointListCount);
            int speed = rand.Next(1,7);
            navMeshAgent.speed = speed;

            if (doSpecial)
            {
                RunSpecial();
            }
            
            doSpecial = true;

            yield return new WaitForSeconds(waiting);
            navMeshAgent.destination = wayPoints[destinationSelectionIndex].position;

            yield return new WaitUntil(() => navMeshAgent.remainingDistance < 0.1f);
        }
    }

    private void UpdateAnimator()
    {
        Vector3 velocity = navMeshAgent.velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);
        float speed = localVelocity.z;
        if (speed < 0.4) speed = Mathf.Epsilon;
        GetComponent<Animator>().SetFloat("ForwardSpeed", speed);
    }

    private void RunSpecial()
    {
        int chances = rand.Next(4);
        if (chances == 1) 
        {
            Debug.Log("Special");
            GetComponent<Animator>().SetTrigger("special");
        }
    }
}
