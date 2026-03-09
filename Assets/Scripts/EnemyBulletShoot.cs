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
            Vector3 aimPosition=new Vector3(enemyTarget.position.x,enemyTarget.position.y+targetHeight,enemyTarget.position.z);     
            Vector3 direction=(aimPosition-shootPoint.position).normalized;

            rb.linearVelocity=direction*bulletSpeed;
            bullet.transform.forward=direction;     
        }

        //destroy 
        Destroy(bullet,5f);
    }
}