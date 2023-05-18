using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public GameObject _playerMotorScrip;


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //target = other.transform;
            _playerMotorScrip.GetComponent<PlayerMotor>().TakeDamage(20f);
            Destroy(this.gameObject);

        }

        if (other.gameObject.CompareTag("Finish"))
        {
            Destroy(this.gameObject);
        }
    }
}
