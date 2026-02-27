using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public Transform player;
    private Animator animator;
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

        Vector3 lookDir=player.position-transform.position;     //calculate direction from enemy to player
        lookDir.y=0;
        if (lookDir.sqrMagnitude > 0.01f)
        {
            transform.rotation=Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookDir), Time.deltaTime * 5f);
        }
    }

    public void StartShooting()
    {
        isShooting=true;

        animator.SetTrigger("Shoot");
    }

    public void StopShooting()
    {
        isShooting=false;
    }

    public bool IsShooting()
    {
        return isShooting;
    }
}