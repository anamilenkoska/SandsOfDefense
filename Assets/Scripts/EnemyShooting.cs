using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public Transform enemyTarget;
    private Animator animator;
    private bool isShooting=false;

    void Start()
    {
        animator=GetComponent<Animator>();
    }

    void Update()
    {
        if(!isShooting) return;

        Vector3 lookDir=enemyTarget.position-transform.position;     
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