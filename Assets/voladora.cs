using UnityEngine;
using System.Collections;

public class voladora : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float attackRange = 10f;
    public float fireInterval = 1f;
    private float timeSinceLastFire = 0f;
    public float bulletSpeed = 10f;
    public float fireRate;
    
    public GameObject projectilePrefab;
    private Quaternion forwardRotation;
    public Transform target;


    private void Start()
    {
        //forwardRotation = Quaternion.Inverse(target.transform.rotation) * this.transform.rotation * Quaternion.Euler(0,150,0);
    }

    void Update()
    {
        if (target != null)
        {
            /*Vector3 toTarget = target.transform.position - this.transform.position;
            Quaternion lookQuat = Quaternion.LookRotation(toTarget) * forwardRotation;
            this.transform.rotation = lookQuat;*/
            transform.LookAt(new Vector3(target.position.x, target.position.y, target.position.z + 2f));
            timeSinceLastFire += Time.deltaTime;

            if (Vector3.Distance(transform.position, target.position) < attackRange)
            {
                if (Random.value < 0.5f && timeSinceLastFire >= fireInterval)
                {
                    timeSinceLastFire = 0f;
                    FireProjectile();
                }
            }
            if (Vector3.Distance(transform.position, target.position) > attackRange && Vector3.Distance(transform.position, target.position) < 30)
            {
                transform.position = Vector3.MoveTowards(new Vector3(transform.position.x, 2, transform.position.z), new Vector3(target.position.x, 1, target.position.z), moveSpeed * Time.deltaTime);
            }

            if (Vector3.Distance(transform.position, target.position) < 8)
            {
                transform.position = Vector3.MoveTowards(new Vector3(transform.position.x, 2, transform.position.z), new Vector3(target.position.x, 1, target.position.z), -moveSpeed * Time.deltaTime);
            }
        }
    }

    void FireProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position - new Vector3(-0.4f,0.5f,0), transform.rotation);
        projectile.GetComponent<Rigidbody>().velocity = transform.forward * 40f;
        Destroy(projectile, 1f);
    }
}