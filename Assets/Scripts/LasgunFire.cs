using System.Collections;
using UnityEngine;

public class LasgunFire : MonoBehaviour
{
    [SerializeField] AudioSource gunFire;       //for the sound to be played
    [SerializeField] GameObject lasgun;         //for the gun object
    [SerializeField] bool canFire=true;              //to check if the gun can fire
    [SerializeField] GameObject sidesCross;         //to make the sides of the cross appear when shooting with the gun

    //for raycast
    [SerializeField] Camera playerCamera;
    [SerializeField] float range=100f;      //how far the gun can shoot
    [SerializeField] int damage=1;          //damage per shot

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && canFire==true)        //0 is for the left mouse button
        {
            canFire=false;
            StartCoroutine(FiringGun());        
        }        
    }

    IEnumerator FiringGun()     //declaring coroutine named firingGun
    {
        gunFire.Play();     //to play the sound of firing the gun
        sidesCross.SetActive(true);         //make the sides appear
        lasgun.GetComponent<Animator>().Play("LasgunFire");     //to play the animation
        
        Shoot();
        
        yield return new WaitForSeconds(0.5f);      //wait half second before enabling to fire the gun again
        lasgun.GetComponent<Animator>().Play("New State");      //in order to be able to play the animation every time the gun is fired
        sidesCross.SetActive(false);            //after firing the gun, sides disappear
        yield return new WaitForSeconds(0.1f);
        canFire=true;           //after half second enable the gun to be able to fire
    }

    void Shoot()
    {
        RaycastHit hit;

        if(Physics.Raycast(playerCamera.transform.position,playerCamera.transform.forward,out hit, range))
        {
            Debug.Log("Hit: "+hit.transform.name);

            EnemyHealth enemy=hit.transform.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.Damage(damage);
            }
        }
    }
}
