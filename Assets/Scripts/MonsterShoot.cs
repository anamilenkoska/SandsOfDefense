using UnityEngine;

public class MonsterShoot : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform shootPoint;
    public float shootForce=10f;
    public float shootCooldown=1f;
    public float lastShootTime;

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastShootTime >= shootCooldown)
        {
            Shoot();
            lastShootTime=Time.time;
        }
    }

    void Shoot()
    {
        if(projectilePrefab==null || shootPoint==null) return;

        GameObject proj=Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);
        Rigidbody rb=proj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(shootPoint.forward*shootForce, ForceMode.Impulse);
        }
    }
}
