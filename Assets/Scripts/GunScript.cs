using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GunScript : MonoBehaviour
{
    [SerializeField]
    static public float damage = 10f;
    [SerializeField]
    private TrailRenderer BulletTrail;
    [SerializeField]
    private Transform trailPoint;
    public float range = 100f;
    public Camera cam;
    public ParticleSystem shotLight;
    public GameObject impactEffect;
    public float impactForce = 60f;
    static public float fireRate = 10f;
    private float nextTimeToFire = 0f;
    public int maxAmmo = 30;
    private int currentAmmo = -1;
    public float reloadTime = 2;
    private bool isReloading = false;
    public TextMeshProUGUI ammoDisplay;
    public Animator myAnim;
    public Light _light;

    
    void Start()
    {
        myAnim = GetComponent<Animator>();
        currentAmmo = maxAmmo;
    }

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire && isReloading == false)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
            
        }
        
        ammoDisplay.text = currentAmmo.ToString();

        


 


        if (currentAmmo <= 0 && !isReloading)
        {
            
            StartCoroutine(Reload());

            return;
        }
    }

    public void dsfkmdsf()
    {
        Debug.Log("sadsad");
    }

    public IEnumerator Reload()
    {
        myAnim.SetBool("Reload", true);
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        isReloading = false;
        myAnim.SetBool("Reload", false);
    }

    public void Shoot()
    {
        if(currentAmmo > 0)
        {

            myAnim.SetBool("Shoot", true);
            currentAmmo--;
            shotLight.Play();
            isReloading = false;
            RaycastHit hit;
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
            {
                TrailRenderer trail = Instantiate(BulletTrail, trailPoint.position, Quaternion.identity);

                StartCoroutine(SpawnTrail(trail, hit));

                TargetScript target = hit.transform.GetComponent<TargetScript>();

                if (target != null)
                {
                    target.TakeDamage(damage);
                }

                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * impactForce);
                }



                GameObject impact = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                impact.GetComponent<ParticleSystem>().Play();
                Destroy(impact, 2f);
            }
        }
       
    }

    private IEnumerator SpawnTrail(TrailRenderer Trail, RaycastHit Hit)
    {
        float time = 0;
        Vector3 startPosition = Trail.transform.position;

        while (time < 1)
        {
            Trail.transform.position = Vector3.Lerp(startPosition, Hit.point, time);
            time += Time.deltaTime / Trail.time;
            yield return null;
            Destroy(Trail.gameObject, Trail.time);
        }
        myAnim.SetBool("Shoot", false);
    }
}
