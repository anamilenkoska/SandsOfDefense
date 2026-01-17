using UnityEngine;

public class Glowing1 : MonoBehaviour
{
    public Color glowColor=Color.orange;
    public float intensity=8f;
    public float speed=2f;
    private Material _material;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _material=GetComponent<Renderer>().material;
        _material.EnableKeyword("_EMISSION");
        _material.SetColor("_EmissionColor",glowColor.linear*intensity);
    }

    // Update is called once per frame
    void Update()
    {
        // float emission=Mathf.PingPong(Time.time * speed, intensity);
        // _material.SetColor("_EmissionColor",glowColor.linear*emission);
    }
}
