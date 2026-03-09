using System.Collections;
using UnityEngine;

public class LasgunFire : MonoBehaviour
{
    [SerializeField] AudioSource gunFire;       
    [SerializeField] GameObject lasgun;         
    [SerializeField] bool canFire=true;              
    [SerializeField] GameObject sidesCross;   

    //for raycast
    [SerializeField] Camera playerCamera;
    [SerializeField] float range=100f;      
    [SerializeField] int damage=1;          

    void Update()
    {
        if (Input.GetMouseButton(0) && canFire==true)        
        {
            canFire=false;
            StartCoroutine(FiringGun());        
        }        
    }

    IEnumerator FiringGun()     
    {
        gunFire.Play();     
        sidesCross.SetActive(true);         
        lasgun.GetComponent<Animator>().Play("LasgunFire");     
        
        Shoot();
        
        yield return new WaitForSeconds(0.5f);      
        lasgun.GetComponent<Animator>().Play("New State");      
        sidesCross.SetActive(false);            
        yield return new WaitForSeconds(0.1f);
        canFire=true;           
    }

    void Shoot()
    {
        RaycastHit hit;

        if(Physics.Raycast(playerCamera.transform.position,playerCamera.transform.forward,out hit, range))
        {
            EnemyHealth enemy=hit.transform.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.Damage(damage);
            }
        }
    }
}
