using UnityEngine;

public class HealthMonitor : MonoBehaviour
{
    public float healthLength=500;
    public float healthPos=320;     //x pos of the bar
    public GameObject healthBar;
    public float damageAmount=0;      //count how much we have going on
    public bool decreasingHealth=false;
    public float hitValue=50;      //amount of damage it is going to be
    public float playerHealth;
    public float maxPlayerHealth=500;       //match it with the bar length, so when it hits 0 it loses
    private bool hasLost=false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerHealth=maxPlayerHealth;
        //StartCoroutine(HealthChange());
    }

    // Update is called once per frame
    void Update()
    {
        // Set position inside canvas correctly using anchoredPosition
        healthBar.GetComponent<RectTransform>().anchoredPosition = new Vector2(healthPos, -70); 
        healthBar.GetComponent<RectTransform>().sizeDelta = new Vector2(healthLength, 70);      

        if (decreasingHealth == true)       //if enemy is hitting the player
        {
            damageAmount+=2f;               //higher the value, quicker the health will decrease
            healthLength-=2f;               //take same value of the length of the bar
            //maxPlayerHealth-=2f;
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
                Debug.Log("You lost");
            }
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
}