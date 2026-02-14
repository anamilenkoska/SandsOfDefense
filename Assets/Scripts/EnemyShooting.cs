using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public Transform player;

    [Header("Shoot Settings")]
    public int shotsPerStop=3;
    public float timeBetweenShots=0.8f;

    private Animator animator;

    private int shotsFired=0;
    private float shootTimer=0f;
    private bool isShooting=false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator=GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isShooting) return;

        Vector3 lookDir=player.position-transform.position;
        lookDir.y=0;
        if (lookDir.sqrMagnitude > 0.01f)
        {
            transform.rotation=Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookDir), Time.deltaTime * 5f);
        }
        shootTimer+=Time.deltaTime;

        if (shootTimer >= timeBetweenShots)
        {
            if (shotsFired < shotsPerStop)
            {
                shootTimer=0f;
                shotsFired++;
                animator.SetTrigger("Shoot");
            }
            else
            {
                isShooting=false;
            }
        }
    }

    public void StartShooting()
    {
        isShooting=true;
        shotsFired=0;
        shootTimer=timeBetweenShots;
    }

    public bool IsShooting()
    {
        return isShooting;
    }
}