using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heal : Interactable
{
    public GameObject player;
    protected override void Interact()
    {
        PlayerMotor.currentHealth = PlayerMotor.currentHealth + 50;
        player.GetComponent<PlayerMotor>().SetHealth();
        Destroy(gameObject);
    }
}
