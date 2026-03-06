using UnityEngine;
using UnityEngine.UI;

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

    public HealthMonitor healthMonitor;

    private RawImage portalBar;

    void Start()
    {
        portalHealth=maxPortalHealth;
        portalBar=healthBarPortal.GetComponent<RawImage>();
    }

    void Update()
    {
        healthBarPortal.GetComponent<RectTransform>().anchoredPosition=new Vector2(healthPos,-55);
        healthBarPortal.GetComponent<RectTransform>().sizeDelta=new Vector2(healthLength,30);

        if (decreadingHealth == true)
        {
            portalBar.color=Color.darkRed;

            damageAmount+=2f;
            healthLength-=2f;
            healthPos-=1f;
            portalHealth-=2f;
            if (damageAmount >= hitValue)
            {
                decreadingHealth=false;
                damageAmount=0;
            }
            if (portalHealth <= 0)
            {
                healthMonitor.LoseAnimation();
                healthMonitor.StopGame();
            }
        }
    }
}
