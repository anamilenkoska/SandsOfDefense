using UnityEngine;

public class PortalHealth : MonoBehaviour
{
    [Header("Portal Health")]
    public float healthLength=350;
    public float healthPos=420;
    public GameObject healthBarPortal;
    public float damageAmount=0;
    public bool decreadingHealth=false;
    public float hitValue=20;
    public float portalHealth;
    public float maxPortalHealth=350;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        portalHealth=maxPortalHealth;
    }

    // Update is called once per frame
    void Update()
    {
        healthBarPortal.GetComponent<RectTransform>().anchoredPosition=new Vector2(healthPos,-55);
        healthBarPortal.GetComponent<RectTransform>().sizeDelta=new Vector2(healthLength,30);

        if (decreadingHealth == true)
        {
            damageAmount+=2f;
            healthLength-=2f;
            healthPos-=1f;
            portalHealth-=2f;
            if (damageAmount >= hitValue)
            {
                Debug.Log("Portal is hit");
                decreadingHealth=false;
                damageAmount=0;
            }
        }
    }
}
