using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Color glowColor;
    public float intensity=8f;
    private Material _material;   

    void Start()
    {
        _material=GetComponent<Renderer>().material;
        _material.EnableKeyword("_EMISSION");
        _material.SetColor("_EmissionColor",glowColor.linear*intensity);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Terrain"))
        {
            Debug.Log("Terrain hit");
            Destroy(gameObject);
            return;
        }

        if (other.CompareTag("Player"))
        {
            Debug.Log("Player hit");

            //get health monitor from player
            HealthMonitor health=other.GetComponent<HealthMonitor>();

            if (health != null)     //check if the script for health exists on player
            {
                health.TakeDamage();        //it exists-call the function that takes health from player
                health.HitFadeAnimation();    //activate red fade after every hit
            }
            Destroy(gameObject);
        }

        PortalHealth portal=other.GetComponentInParent<PortalHealth>();
        if (portal != null)
        {
            Debug.Log("Portal hit");
            portal.decreadingHealth=true;
            Destroy(gameObject);
        }
    }
}
