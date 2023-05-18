using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class torreta : MonoBehaviour
{
    
    public float attackRange = 10f;
    public float bulletSpeed = 30f;
    public float fireInterval = 1f;
    private float timeSinceLastFire = 0f;
    public GameObject projectilePrefab;
    private Quaternion forwardRotation;
    public Transform target;

    void Update()
    {
        if (target != null)
        {
            
            transform.LookAt (new Vector3(target.position.x, target.position.y, target.position.z));
            timeSinceLastFire += Time.deltaTime;


            if (Vector3.Distance(transform.position, target.position) < attackRange)
            {
                if (Random.value < 0.5f && timeSinceLastFire >= fireInterval)
                {
                    timeSinceLastFire = 0f;
                    FireProjectile();
                }
            }       
        }
    }
    void FireProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position + new Vector3(-0.65f, 1.3f, 0), transform.rotation);
        projectile.GetComponent<Rigidbody>().velocity = transform.forward * 40f;
        Destroy(projectile, 1f);
        
    }
}
