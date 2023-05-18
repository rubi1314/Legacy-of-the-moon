using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public Transform player;
    public float damage = 10f;
    public float range = 100f;
    private PlayerMotor playerMotor;

    private void Update()
    {
        transform.LookAt(player);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerMotor>(out PlayerMotor playerMotor))
        {
            Shoot();
            new WaitForSeconds(3); 
        }
    }

    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, range))
        {
            PlayerMotor playerMotor = hit.transform.GetComponent<PlayerMotor>();
            if (playerMotor != null)
            {
                playerMotor.TakeDamage(15);
            }
        }
    }

}
