using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Mover : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    // [SerializeField] float maxSpeed = 6f;
    [SerializeField] Transform waypoint;
    [SerializeField] Transform afterGameWaypoint;

    Ray lastRay;
    public bool allowMovement = true;
    private List<string> goodAnims = new List<string>();
    private List<string> badAnims = new List<string>();
    System.Random rand = new System.Random();

    const string SHAKE = "Shake";
    const string FOCUS = "Focus";

    const string CRAZY = "Crazy";
    const string BELLY = "Belly";
    const string CHEERING = "Cheering";
    const string EXCITED = "Excited";
    const string CLAPPING = "Clapping";
    const string CANCAN = "CanCan";
    const string GANGNAM = "Gangnam";
    const string SWIV = "Swiv";
    const string YMCA = "YMCA";


    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        goodAnims.AddRange(new[] { BELLY, CHEERING, EXCITED, CLAPPING, CANCAN, GANGNAM, SWIV, YMCA });
        badAnims.AddRange(new[] {SHAKE, FOCUS, CRAZY});
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            MoveTo();
        }
        
        // if (Input.GetKey(KeyCode.Space))
        // {
        //     DoHipHop();
        // }

        // if (Input.GetKey(KeyCode.L))
        // {
        //     allowMovement = true;
        // }

        // if (Input.GetKey(KeyCode.S))
        // {
        //     DoSalsa();
        // }

        // if (Input.GetKey(KeyCode.P))
        // {
        //     navMeshAgent.destination = waypoint.transform.position;
        // }
        
        UpdateAnimator();
    }

    public void MoveToAfterGameWaypoint()
    {
        navMeshAgent.destination = afterGameWaypoint.transform.position;
    }
    public void MoveTo()
    {
        if (allowMovement)
        {
            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
            if (hasHit)
            {
                navMeshAgent.destination = hit.point;
            }
        }  
    }

    private Ray GetMouseRay()
    {
        return Camera.main.ScreenPointToRay(Input.mousePosition);
    }
    

    private void UpdateAnimator()
    {
        Vector3 velocity = navMeshAgent.velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);
        float speed = localVelocity.z;
        if (speed < 0.4) speed = Mathf.Epsilon;
        GetComponent<Animator>().SetFloat("ForwardSpeed", speed);
    }

    private void DoCatwalk()
    {
        GetComponent<Animator>().applyRootMotion = true;
        GetComponent<Animator>().SetTrigger("catwalk");
        Invoke(nameof(StopCatwalk), 15f);
    }

    private void StopCatwalk()
    {
        GetComponent<Animator>().applyRootMotion = false;
        GetComponent<Animator>().ResetTrigger("catwalk");
    }

    private void DoHipHop()
    {
        // GetComponent<Animator>().applyRootMotion = true;
        GetComponent<Animator>().SetTrigger("HipHop");
        // Invoke(nameof(StopCatwalk), 15f);
    }

    public void DoSalsa()
    {
        // GetComponent<Animator>().applyRootMotion = false;
        GetComponent<Animator>().SetTrigger("Salsa");
        // Invoke(nameof(StopCatwalk), 15f);
    }

    public void CorrectAnswerAnimation()
    {
        int goodAnimsIndex = rand.Next(goodAnims.Count);
        GetComponent<Animator>().SetTrigger(goodAnims[goodAnimsIndex]);
    }

    public void IncorrectAnswerAnimation()
    {
        int badAnimsIndex = rand.Next(badAnims.Count);
        GetComponent<Animator>().SetTrigger(badAnims[badAnimsIndex]);
    }


    public void DoShake()
    {
        GetComponent<Animator>().SetTrigger("Shake");
    }

    public void DoFocus()
    {
        GetComponent<Animator>().SetTrigger("Focus");
    }

    public void DoCrazy()
    {
        GetComponent<Animator>().SetTrigger("Crazy");
    }

    public void GoToQuiz()
    {
        navMeshAgent.destination = waypoint.transform.position;
    }

}
