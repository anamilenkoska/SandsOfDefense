using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public Transform player;
    public float idleDuration=2f;   //seconds to stay idle
    public float walkDistance=6f;
    private NavMeshAgent agent;
    private Animator animator;
    private EnemyShooting shooter;
    public float firstShootDistance=2000f;        //must be within this distance to start shooting
    private float idleTimer=0f;
    private bool hasStartedWalking=false;
    private bool isWalking=false;
    private bool waitingForShootToFinish=false;
    private bool firstShootDone=false;

    void Start()
    {
        agent=GetComponent<NavMeshAgent>();
        animator=GetComponent<Animator>();
        shooter=GetComponent<EnemyShooting>();

        agent.updateRotation=false;         //rotate manually
        agent.isStopped=true;       //start idle
        //animator.SetBool("isWalking",false);
    }

    void Update()
    {
        if(player==null) return;

        if (!hasStartedWalking)     //check if the monster has started walking
        {
            idleTimer+=Time.deltaTime;      //if not then count the time in idle

            if(idleTimer>=idleDuration)     //check if the monster has been in idle long enough
            {
                hasStartedWalking=true;
                //agent.isStopped=false;      //start walking
                StartWalking();
            }
            return;      //skip walking logic->still in idle
        }

        if (waitingForShootToFinish)
        {
            if (!shooter.IsShooting())
            {
                waitingForShootToFinish=false;
                StartWalking();
            }
            return;
        }

        if (isWalking)
        {
            if (agent.velocity.sqrMagnitude > 0.1f)
            {
                transform.rotation=Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(agent.velocity.normalized),Time.deltaTime*10f);
            }

            if(!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {
                StopAndShoot();
            }
        }
    }

    void StartWalking()
    {
        Vector3 direction=(player.position-transform.position).normalized;
        Vector3 target=transform.position+direction*walkDistance;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(target, out hit, walkDistance, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }

        agent.isStopped=false;
        agent.velocity=Vector3.zero;
        animator.SetBool("isWalking",true);
        isWalking=true;
    } 

    void StopAndShoot()
    {
        if (!firstShootDone)
        {
            float distanceToTarget=Vector3.Distance(transform.position,player.position);

            if (distanceToTarget > firstShootDistance)
            {
                //player is too far-continue walking
                StartWalking();
                return;
            }
            firstShootDone=true;        //allow shooting from now on
        }

        isWalking=false;
        agent.isStopped=true;
        agent.velocity=Vector3.zero;
        animator.SetBool("isWalking",false);

        waitingForShootToFinish=true;
        shooter.StartShooting();
    }

}


