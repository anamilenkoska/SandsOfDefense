using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] AudioSource enemyHit;
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

    public static int totalEnemies=0;
    public static TMP_Text enemyCounter;

    void Awake()
    {
        enemiesAlive++;     //count each enemy when scene loads
        totalEnemies++;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth=maxHealth;        //enemy begins with full health
        animator=GetComponent<Animator>();
        enemyCollider=GetComponent<Collider>();
        agent=GetComponent<NavMeshAgent>();
        shooter=GetComponent<EnemyShooting>();

        UpdateCounterUI();      //update when enemy dies
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
            enemyHit.Play();
            StartCoroutine(ResumeAfterHit(0.5f));       //enemy is still for 0.5s
        }
        else
        {
            enemyHit.Play();        //the last hit to have sound
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

        UpdateCounterUI();

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

    public static void UpdateCounterUI()
    {
        if (enemyCounter != null)
        {
            enemyCounter.text=$"{enemiesAlive}/{totalEnemies}";
        }
    }
}

