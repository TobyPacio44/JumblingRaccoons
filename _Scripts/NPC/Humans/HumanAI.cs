using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.PostProcessing;

public class HumanAI : MonoBehaviour
{
    public State state;
    [HideInInspector] public NavMeshAgent agent;
    [HideInInspector] public FieldOfView FieldOfView;
    [HideInInspector] public Transform target;
    public Animator anim;

    //Patroling
    public GameObject[] Routine;
    public GameObject bed;
    [HideInInspector] public Vector3 walkPoint;
    [HideInInspector] public int point = 0;
    [HideInInspector] public bool walkPointSet, busy, nextPatrol, delay;
    [HideInInspector] public float randomWalkPointRadius = 250f;

    [Header("Area")]
    public bool limitedArea;
    public float agentArea;
    public Vector3 agentRadiusCenter;

    [Header("Speed")]
    public float normalSpeed;
    public float chaseSpeed;

    //Attacking
    [HideInInspector] public float timeBetweenAttacks;
    [HideInInspector] bool alreadyAttacked;

    //Timer
    [HideInInspector] public float busyFor, aggroFor, chaseFor;

    //Range
    [HideInInspector] public bool playerInSightRange, playerInAttackRange, sawPlayer;
    
    public enum State
    {   
        unbothered, 
        suspicious, 
        aggressive,
        sleeping,
        sitting
    }
    private Vector3 PatrolCenter;


    private void Awake()
    {
        FieldOfView = GetComponent<FieldOfView>();
        agent = GetComponent<NavMeshAgent>();
        PatrolCenter=transform.position;
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        StartCoroutine(ActionLoop());
    }

    private IEnumerator ActionLoop()
    {
        WaitForSeconds wait = new WaitForSeconds(0.1f);

        while (true)
        {
            yield return wait;
            MakeAction();
        }
    }
    private void MakeAction()
    {
        switch (state)
        {
            case State.unbothered:
                agent.speed = normalSpeed;
                Patrolling();
                anim.Play("Base Layer.Idle");
                break;

            case State.suspicious:
                agent.speed = chaseSpeed;
                Panic();
                anim.Play("Base Layer.LookingFor");
                break;

            case State.aggressive:
                agent.speed = chaseSpeed;
                ChasePlayer();
                anim.Play("Base Layer.Chase");
                break;

            case State.sleeping:
                agent.speed = normalSpeed;
                goToSleep();
                break;

            case State.sitting:
                break;
        }
    }
    private void Update()
    {
        playerInSightRange = FieldOfView.canSeePlayer;
        playerInAttackRange = FieldOfView.canAttackPlayer;
        target = FieldOfView.target;

        //Checks if agent is in his area
        if (Vector3.Distance(agent.transform.position, agentRadiusCenter) > agentArea && limitedArea)
        {
            state = State.unbothered;
            StopCoroutine(actionDelayWait());
            ResetDestination();            
            Debug.Log("Agent has left patrolling area");
        }

        if ((!playerInSightRange && !playerInAttackRange) && sawPlayer && target == null)
        {
            state = State.suspicious;
            StartCoroutine(aggroWait());
        }
        else if (playerInSightRange && !playerInAttackRange)
        {
            state = State.aggressive;
            sawPlayer = true;
        }
        // if ( playerInSightRange &&  playerInAttackRange) { AttackPlayer(); }
    }


    //unbothered
    private void Patrolling()
    {
        if (sawPlayer) { return; }

        if (nextPatrol)
        {
            walkPoint = new Vector3(Routine[point].transform.position.x, Routine[point].transform.position.y, Routine[point].transform.position.z);
            PatrolCenter = Routine[point].transform.position;
            nextPatrol = false;

            Debug.Log("Moving");
            agent.SetDestination(walkPoint);
            StartCoroutine(WaitForArrival());

            point++;
            if(point >= Routine.Length) { point = 0; }
        }
    }
  
    //suspicious
    private void Panic()
    {
        if (!playerInSightRange && !playerInAttackRange && !walkPointSet)
        {          
            searchRandomPoint();                        
        }
    }
    private void searchRandomPoint()
     {
        float randomZ = Random.Range(-randomWalkPointRadius, randomWalkPointRadius);
        float randomX = Random.Range(-randomWalkPointRadius, randomWalkPointRadius);
        Debug.Log("WalkPoint set");
        walkPoint = new Vector3(PatrolCenter.x + randomX, transform.position.y, PatrolCenter.z + randomZ);
        walkPointSet = true;

        NavMeshPath path = new NavMeshPath();
        if (NavMesh.CalculatePath(transform.position, walkPoint, NavMesh.AllAreas, path))
        {
            StartCoroutine(WaitForWalkPoint());
            agent.SetDestination(walkPoint);
        }

    }

    //aggressive
    private void ChasePlayer()
    {
        if(target != null)
        {
            PatrolCenter = target.position;
            agent.SetDestination(target.position);
        }

        if (!playerInSightRange)
        {
            if (FieldOfView.canChasePlayer)
            {
                agent.SetDestination(PatrolCenter);
                return;
            }
            StartCoroutine(chaseSight());           
        }
    }
    private void AttackPlayer()
    {
        //agent.SetDestination();
        //transform.LookAt();
    }

    //Sleeping

    private void goToSleep()
    {
        agent.SetDestination(bed.transform.position);
    }

    public void wakeUp()
    {
        agent.enabled = true;
        state = State.unbothered;
        FieldOfView.ignore = false;
    }

    void ResetDestination()
    {
        Debug.Log("Reset");
        walkPointSet = false;
        sawPlayer = false;
        target = null;
        FieldOfView.target = null;
        agent.SetDestination(walkPoint);
    }

    //Waits until agent reached next patrol.
    private IEnumerator WaitForArrival()
    {
        busy = true;
        yield return new WaitUntil(() => !agent.pathPending && agent.remainingDistance < 0.5f);
        nextPatrol = true;
        busy = false;
        yield break;
    }

    //Waits until agent reached randomPoint.
    private IEnumerator WaitForWalkPoint()
    {
        walkPointSet = true;
        yield return new WaitUntil(() => !agent.pathPending && agent.remainingDistance < 0.5f);
        walkPointSet = false;
        yield break;
    }

    //Waits before taking another action.
    private IEnumerator actionDelayWait()
    {
        if (state == State.suspicious)
        {
            WaitForSeconds achievePointTime = new WaitForSeconds(busyFor);
            while (true)
            {
                yield return achievePointTime;
                walkPointSet = false;
            }
        }
        yield break;
    }

    //Waits while player out of radius, chases as long as active, then is panicking
    private IEnumerator chaseSight()
    {      
            WaitForSeconds chaseSight = new WaitForSeconds(chaseFor);
            while (true)
            {
            if (!playerInSightRange)
            {
                yield return chaseSight;
                Debug.Log("ChaseSight");
                state = State.suspicious;
                StartCoroutine(aggroWait());
                yield break;
            }
        }

    }

    //Waits after seeing the player. After it's done it will go back to patrolling it's points.
    private IEnumerator aggroWait()
    {
        sawPlayer = false;
        WaitForSeconds aggroWait = new WaitForSeconds(aggroFor);
        while (true)
        {
            yield return aggroWait;
            state = State.unbothered;
            StopCoroutine(actionDelayWait());
            yield break;
        }
    }

    //Interaction
    private void OnTriggerEnter(Collider other)
    {
        if ((other.tag == "Open") && !other.GetComponent<OpenDoor>().isOpened)
        {
            other.GetComponent<OpenDoor>().Interact();
            anim.Play("Base Layer.Action");
        }

        if (other.tag == "Bed" && state == State.sleeping)
        {
            agent.enabled = false;
            GameObject lay = other.GetComponent<bedSnap>().lay;
            transform.position = lay.transform.position;

            transform.eulerAngles = new Vector3(
            transform.eulerAngles.x,
            lay.transform.localEulerAngles.y,
            transform.eulerAngles.z
            );

            FieldOfView.ignore = true;

            anim.Play("Base Layer.Sleep");
            Debug.Log("Bed");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Open")
        {
            StartCoroutine(InteractDelay(other.gameObject));
        }
    }
    private IEnumerator InteractDelay(GameObject Interaction)
    {
        WaitForSeconds InteractDelay = new WaitForSeconds(2.5f);
        while (true)
        {
            yield return InteractDelay;
            Interact(Interaction);
            yield break;
        }

    }
    private void Interact(GameObject interaction)
    {
        interaction.GetComponent<OpenDoor>().Interact();
    }

    //Gizmos
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, walkPoint);

        Handles.color = Color.yellow;
        Handles.DrawWireArc(agentRadiusCenter, Vector3.up, Vector3.forward, 360, agentArea);

        Handles.color = Color.blue;
        Handles.DrawWireArc(PatrolCenter, Vector3.up, Vector3.forward, 360, randomWalkPointRadius);
    }
}
