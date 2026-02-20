using System.Collections;
using UnityEngine;

public class HealthMonitor : MonoBehaviour
{
    public float healthLength=500;
    public float healthPos=320;     //x pos of the bar
    public GameObject healthBar;
    public float damageAmount;      //count how much we have going on
    public bool decreasingHealth=false;
    public float hitValue=50;      //amount of damage it is going to be

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
            if (damageAmount >= hitValue)   //when hit the damageAmount will start increasing until it reaches hitValue
            {
                decreasingHealth=false;     
                damageAmount=0;             //reset
            }
            else
            {
                damageAmount+=2f;     //higher the value, quicker the health will decrease
                healthLength-=2f;           //take same value of the length of the bar
                healthPos-=1f;
            }
        }
    }

    public void TakeDamage()
    {
        decreasingHealth=true;
    }
}



