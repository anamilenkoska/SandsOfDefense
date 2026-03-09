using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public Transform enemyTarget;
    public float idleDuration=2f;   
    public float walkDistance=6f;
    public float firstShootDistance=30f;        
    private float idleTimer=0f;
    private NavMeshAgent agent;
    private Animator animator;
    private EnemyShooting shooter;
    private EnemyHealth health;

    private bool hasStartedWalking=false;
    private bool isWalking=false;
    private bool waitingForShootToFinish=false;
    private bool firstShootDone=false;

    void Start()
    {
        agent=GetComponent<NavMeshAgent>();
        animator=GetComponent<Animator>();
        shooter=GetComponent<EnemyShooting>();
        health=GetComponent<EnemyHealth>();

        agent.updateRotation=false;         //rotate manually
        agent.isStopped=true;       //start idle
    }

    void Update()
    {
        if(enemyTarget==null) return;
        if(health!=null && (health.isDead || health.isHit)) return;      

        if (!hasStartedWalking)     
        {
            idleTimer+=Time.deltaTime;      

            if(idleTimer>=idleDuration)     
            {
                hasStartedWalking=true;
                StartWalking();
            }
            return;      
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
        Vector3 direction=(enemyTarget.position-transform.position).normalized;
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
            float distanceToTarget=Vector3.Distance(transform.position,enemyTarget.position);

            if (distanceToTarget > firstShootDistance)
            {
                StartWalking();
                return;
            }
            firstShootDone=true;        
        }

        isWalking=false;
        agent.isStopped=true;
        agent.velocity=Vector3.zero;
        animator.SetBool("isWalking",false);

        waitingForShootToFinish=true;
        shooter.StartShooting();
    }
}


