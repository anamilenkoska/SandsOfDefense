using UnityEngine;

public class EnemyBulletShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform enemyTarget;
    public Transform shootPoint;
    public float bulletSpeed=20f;
    public float targetHeight=1.7f;
    
    public void SpawnBullet()
    {
        if(bulletPrefab==null || shootPoint==null) return;

        GameObject bullet=Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
        Rigidbody rb=bullet.GetComponent<Rigidbody>();

        if (rb != null)
        {
            //adjust target height
            Vector3 aimPosition=new Vector3(enemyTarget.position.x,enemyTarget.position.y+targetHeight,enemyTarget.position.z);     //position where it shoot the player

            //compute straight direction
            Vector3 direction=(aimPosition-shootPoint.position).normalized;

            //set velocity in a straight line, and set speed
            rb.linearVelocity=direction*bulletSpeed;

            //rotate bullet to face target
            bullet.transform.forward=direction;     //to not fly sideways
        }

        //destroy 
        Destroy(bullet,5f);
    }
}