using UnityEngine;

public class LasgunFire : MonoBehaviour
{
    [SerializeField] AudioSource gunFire;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))        //0 is for the left mouse button
        {
            gunFire.Play();
        }        
    }
}
