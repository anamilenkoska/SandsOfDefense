using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] AudioSource enemyHit;
    public int maxHealth = 5;
    private int currentHealth;
    public static int enemiesAlive = 0;

    [HideInInspector] public bool isDead = false;
    [HideInInspector] public bool isHit = false;

    private Animator animator;
    private Collider enemyCollider;
    private NavMeshAgent agent;
    private EnemyShooting shooter;

    private static bool gameWon = false;

    public static int totalEnemies = 0;
    public static TMP_Text enemyCounter;

    void Awake()
    {
        enemiesAlive++;
        totalEnemies++;
    }

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        enemyCollider = GetComponent<Collider>();
        agent = GetComponent<NavMeshAgent>();
        shooter = GetComponent<EnemyShooting>();

        UpdateCounterUI();
    }

    public void Damage(int damage)
    {
        if (isDead) return;
        currentHealth -= damage;
        if (currentHealth > 0)
        {
            isHit = true;

            if (agent != null)
            {
                agent.isStopped = true;
                agent.velocity = Vector3.zero;
            }

            if (shooter != null)
            {
                shooter.StopShooting();
            }

            animator.SetTrigger("Hit");
            enemyHit.Play();
            StartCoroutine(ResumeAfterHit(0.5f));
        }
        else
        {
            enemyHit.Play();
            Die();
        }
    }

    IEnumerator ResumeAfterHit(float duration)
    {
        yield return new WaitForSeconds(duration);
        if (!isDead)
        {
            agent.isStopped = false;      
        }
        isHit = false;
    }

    void Die()
    {
        isDead = true;

        enemiesAlive--;     

        UpdateCounterUI();

        animator.SetTrigger("Die");

        if (enemiesAlive <= 0 && !gameWon)
        {
            gameWon = true;
        }

        agent.isStopped = true;       
        agent.velocity = Vector3.zero;
        enemyCollider.enabled = false;
        shooter.enabled = false;

        StartCoroutine(DestroyEnemy());     
    }

    IEnumerator DestroyEnemy()
    {
        yield return new WaitForSeconds(1f);      
        Destroy(gameObject);                      
    }

    public static void UpdateCounterUI()
    {
        if (enemyCounter != null)
        {
            enemyCounter.text = $"{enemiesAlive}/{totalEnemies}";
        }
    }
}

