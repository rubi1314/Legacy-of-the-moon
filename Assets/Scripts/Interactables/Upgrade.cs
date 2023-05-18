using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Upgrade : Interactable
{
    // codigo del objeto interactuable
    protected override void Interact()
    {
        Destroy(gameObject);

        if (tag == "DamageUpgrade")
        {
            PlayerMotor.damage = PlayerMotor.damage * 2;
        }

        if (tag == "FireRateUpgrade")
        {
            PlayerMotor.fireRate = PlayerMotor.fireRate + 3;
        }

        if (tag == "Win")
        {
            SceneManager.LoadScene("Win");
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    
}
