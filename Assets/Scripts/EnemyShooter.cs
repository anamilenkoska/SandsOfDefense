using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform player;
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
            Vector3 target=new Vector3(player.position.x,player.position.y+targetHeight,player.position.z);

            //compute straight direction
            Vector3 direction=(target-shootPoint.position).normalized;

            //set velocity in a straight line
            rb.linearVelocity=direction*bulletSpeed;

            //rotate bullet to face target
            bullet.transform.forward=direction;
        }

        //destroy 
        Destroy(bullet,5f);
    }
}