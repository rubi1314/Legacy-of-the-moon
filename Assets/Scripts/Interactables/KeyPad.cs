using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPad : Interactable
{
    [SerializeField]
    private GameObject door;
    private bool doorOpen;


    // codigo del objeto interactuable
    protected override void Interact()
    {
        doorOpen = !doorOpen;
        door.GetComponent<Animator>().SetBool("IsOpen", doorOpen);

        
    }
}
