using UnityEngine;
using UnityEngine.AI;

public class WalkTest : MonoBehaviour
{
    public Transform player;
    public float idleDuration=2f;   //seconds to stay idle
    private NavMeshAgent agent;
    private Animator animator;
    private float idleTimer=0f;
    private bool hasStartedWalking=false;

    void Start()
    {
        agent=GetComponent<NavMeshAgent>();
        animator=GetComponent<Animator>();

        agent.updateRotation=false;         //rotate manually
        agent.isStopped=true;       //start idle
        animator.SetBool("isWalking",false);
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
                agent.isStopped=false;      //start walking
            }else{return;}      //skip walking logic->still in idle
        }

        float distance=Vector3.Distance(transform.position,player.position);

        if (distance <= agent.stoppingDistance)     //check if monster has reached the player
        {
            agent.isStopped=true;
            animator.SetBool("isWalking",false);    //if yes stop walking
        }
        else
        {
            agent.isStopped=false;
            animator.SetBool("isWalking",true);
            agent.SetDestination(player.position);

            //rotate monter toward player
            if (agent.velocity.sqrMagnitude > 0.01f)
            {
                transform.rotation=Quaternion.LookRotation(agent.velocity.normalized);
            }
        }
    }

}
