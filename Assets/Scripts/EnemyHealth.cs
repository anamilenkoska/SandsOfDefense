using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth=5;
    private int currentHealth;
    public static int enemiesAlive=0;

    [HideInInspector] public bool isDead=false;     //died and should stop everything
    [HideInInspector] public bool isHit=false;      //for hit animation

    private Animator animator;
    private Collider enemyCollider;
    private NavMeshAgent agent;
    private EnemyShooting shooter;

    private static bool gameWon=false;

    void Awake()
    {
        enemiesAlive++;     //count each enemy when scene loads
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth=maxHealth;        //enemy begins with full health
        animator=GetComponent<Animator>();
        enemyCollider=GetComponent<Collider>();
        agent=GetComponent<NavMeshAgent>();
        shooter=GetComponent<EnemyShooting>();
    }

    public void Damage(int damage)
    {
        if(isDead) return;      //prevent double death
        currentHealth-=damage;
        if (currentHealth > 0)      //if still alive hit
        {
            isHit=true;

            //stop movement when it is shot
            if (agent != null)
            {
                agent.isStopped=true;           //prevents movement while player's shoot 
                agent.velocity=Vector3.zero;    //cancels sliding
            }

            //stop shooting cleanly
            if (shooter != null)
            {
                shooter.StopShooting();     //prevent shooting while enemy is hit
            }

            animator.SetTrigger("Hit");
            StartCoroutine(ResumeAfterHit(0.5f));       //enemy is still for 0.5s
        }
        else
        {
            Die();
        }
    }

    IEnumerator ResumeAfterHit(float duration)
    {
        yield return new WaitForSeconds(duration);      //wait for 0.5s
        if (!isDead)
        {
            if (agent != null)
            {
                agent.isStopped=false;      //if enemy not dead after hit, resume movement
            }
        }
        isHit=false;
    }

    void Die()
    {
        isDead=true;

        enemiesAlive--;     //decrease enemy count

        animator.SetTrigger("Die");

        if (enemiesAlive <= 0 && !gameWon)
        {
            gameWon=true;
        }

        if (agent != null)
        {
            agent.isStopped=true;       //stop movement
            agent.velocity=Vector3.zero;
        }

        if (enemyCollider != null)
        {
            enemyCollider.enabled=false;
        }

        if (shooter != null)
        {
            shooter.enabled=false;
        }

        StartCoroutine(DestroyEnemy());     //wait before destroy-death animation to finish
    }

    IEnumerator DestroyEnemy()
    {
        yield return new WaitForSeconds(1f);      //wait 1s 
        Destroy(gameObject);                      //then destroy enemy
    }
}


