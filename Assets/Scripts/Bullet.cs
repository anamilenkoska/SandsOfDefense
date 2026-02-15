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
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player hit");
            Destroy(gameObject);
        }
    }
}
