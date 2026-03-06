using UnityEngine;

public class Glowing : MonoBehaviour
{
    public Color glowColor=Color.cyan;
    public float intensity=8f;
    public float speed=2f;
    private Material _material;

    void Start()
    {
        _material=GetComponent<Renderer>().material;
        _material.EnableKeyword("_EMISSION");
        _material.SetColor("_EmissionColor",glowColor.linear*intensity);
    }
}
