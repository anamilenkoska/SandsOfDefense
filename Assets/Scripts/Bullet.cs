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
            Destroy(gameObject);
            return;
        }

        if (other.CompareTag("Player"))
        {
            HealthMonitor health=other.GetComponent<HealthMonitor>();

            if (health != null)     
            {
                health.TakeDamage();        
                health.HitFadeAnimation();    
            }
            Destroy(gameObject);
        }

        PortalHealth portal=other.GetComponentInParent<PortalHealth>();
        if (portal != null)
        {
            portal.decreadingHealth=true;
            Destroy(gameObject);
        }
    }
}
