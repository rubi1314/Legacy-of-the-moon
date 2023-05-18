using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    public float speed = 5f;
    static public float maxHealth = 100f;
    static public float currentHealth;
    public float sprintSpeed = 10f;
    public float crouchSpeed = 1f;
    private bool isGrounded, isSprinting, isCrouching;
    private float gravity = -9.8f;
    public float jumpHeight = 3f;
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public Animator myAnim;
    public Animator PlAnim;

    /****/

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
    private bool isShooting = false;
    public TextMeshProUGUI ammoDisplay;
    public Light _light;



    /****/

    // Start is called before the first frame update
    void Start()
    {
        damage = 10f;
        fireRate = 10f;
        currentAmmo = maxAmmo;
        currentHealth = maxHealth;
        SetMaxHealth();
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = controller.isGrounded;
       

        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire && isReloading == false )
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }

        ammoDisplay.text = currentAmmo.ToString();

        if (currentAmmo <= 0 && isReloading == false && currentAmmo <30)
        {

            StartCoroutine(Reload());

            return;
        }
    }

// recibe los inputs y los aplica al character controller
    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;


        if (moveDirection.x == 0 && moveDirection.z == 0 && isReloading == false)
        {
            myAnim.SetFloat("Velocity", 0f);
            PlAnim.SetFloat("Velocity", 0f);
        }
        else
        {
            if (isSprinting && isReloading == false)
            {
                controller.Move(transform.TransformDirection(moveDirection) * sprintSpeed * Time.deltaTime);
                myAnim.SetFloat("Velocity", 1f);
                PlAnim.SetFloat("Velocity", 1f);
            }
            else
            {
                controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
                myAnim.SetFloat("Velocity", 0.5f);
                PlAnim.SetFloat("Velocity", 0.5f);
            }

        }
          

        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
            //myAnim.SetFloat("Velocity", 0);
        }

        playerVelocity.y += gravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    public void Jump()
    {
        if (isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
    }

    public void SprintPressed()
    {
        isSprinting = true;
        //myAnim.SetBool("Run", true);
        myAnim.SetFloat("Velocity", 1);
    }

    public void SprintReleased()
    {
        isSprinting = false;
        //myAnim.SetBool("Run", false);
    }
    public void CrouchPressed()
    {
        isCrouching = true;
    }

    public void CrouchReleased()
    {
        isCrouching = false;
    }

    public void SetHealth()
    {
        slider.value = currentHealth;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void SetMaxHealth()
    {
        slider.maxValue = maxHealth;
        slider.value = currentHealth;
        fill.color = gradient.Evaluate(1f);
    }

    public void TakeDamage(float damage)
    {
        
        currentHealth -= damage;
        SetHealth();
    }





    ///************************/
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
        if (currentAmmo > 0)
        {

            
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
        myAnim.SetBool("Shoot", true);
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

    /******/



}
