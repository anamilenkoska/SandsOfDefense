using UnityEngine;
using StarterAssets;
using TMPro;

public class HealthMonitor : MonoBehaviour
{
    [Header("Player Health")]
    public float healthLength=500;
    public float healthPos=320;     //x pos of the bar
    public GameObject healthBar;
    public float damageAmount=0;      //count how much we have going on
    public bool decreasingHealth=false;
    public float hitValue=50;      //amount of damage it is going to be
    public float playerHealth;
    public float maxPlayerHealth=500;       //match it with the bar length, so when it hits 0 it loses
    [SerializeField] AudioSource playerHit;
    private bool hasLost=false;
    private bool hasWon=false;

    [Header("EndGame")]
    public Animator hitScreen;
    public Animator lose;
    public Animator win;
    [SerializeField] AudioSource winSound;
    [SerializeField] AudioSource loseSound;

    [Header("Player Gun")]
    public LasgunFire playerGun;
    public Animator gunAnimator;
    public GameObject crossSides;
    
    [Header("Enemy Counter")]
    public TMP_Text enemyCounter;

    void Start()
    {
        playerHealth=maxPlayerHealth;

        if (enemyCounter != null)
        {
            EnemyHealth.enemyCounter=enemyCounter;
        }

        EnemyHealth.UpdateCounterUI();
    }

    void Update()
    {
        // Set position inside canvas correctly using anchoredPosition
        healthBar.GetComponent<RectTransform>().anchoredPosition = new Vector2(healthPos, 85); 
        healthBar.GetComponent<RectTransform>().sizeDelta = new Vector2(healthLength, 70);      

        if (decreasingHealth == true)       //if enemy is hitting the player
        {
            damageAmount+=2f;               //higher the value, quicker the health will decrease
            healthLength-=2f;               //take same value of the length of the bar
            healthPos-=1f;
            playerHealth-=2f;
            if (damageAmount >= hitValue)   //when hit the damageAmount will start increasing until it reaches hitValue
            {
                decreasingHealth=false;     
                damageAmount=0;             //reset
            }
            if(playerHealth<=0 && !hasLost)
            {
                hasLost=true;
                LoseAnimation();
                StopGame();              //stop everything after death
            }
        }

        if(!hasWon && EnemyHealth.enemiesAlive <= 0)
        {
            hasWon=true;
            WinAnimation();
            StopGame();     //stop everything from moving after end of game
        }
    }

    public void TakeDamage()
    {
        if (!hasLost)
        {
            decreasingHealth=true;
            damageAmount=0;         //reset counter for new hit
        }
    }

    public void HitFadeAnimation()
    {
        hitScreen.SetTrigger("PlayerHit");
        playerHit.Play();
    }

    public void LoseAnimation()
    {
        loseSound.Play();
        lose.SetTrigger("PlayerDead");
    }

    public void WinAnimation()
    {
        winSound.Play();
        win.SetTrigger("PlayerWin");
    }

    public void StopGame()
    {
        //disable player controller
        var playerContr=GetComponent<FirstPersonController>();
        playerContr.enabled=false;

        //disable crosshair
        crossSides.SetActive(false);

        //stop the background music
        GameObject backgroundMusic=GameObject.Find("BackgroundMusic");
        AudioSource music=backgroundMusic.GetComponent<AudioSource>();
        music.Stop();

        //disable shooting
        playerGun.enabled=false;
        playerGun.StopAllCoroutines();      //stop shooting coroutine
        
        gunAnimator.enabled=false;

        //destroy all bullets
        var bullets=FindObjectsByType<Bullet>(FindObjectsSortMode.None);        //FindObjectsOfType is deprecated and causes warnings in unity
        foreach(var b in bullets) Destroy(b.gameObject);

        //stop enemies from moving
        var enemies=FindObjectsByType<EnemyMovement>(FindObjectsSortMode.None);
        foreach (var e in enemies)
        {
            if(e!=this && e.enabled)
            {
                e.enabled=false;
            }
        }
    }
}


