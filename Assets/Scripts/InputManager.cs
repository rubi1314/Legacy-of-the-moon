using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public GameObject _prueba;

    private PlayerInput playerInput;
    public PlayerInput.OnFootActions OnFoot;
    public PlayerInput.ShootActions Shoot;

    private PlayerMotor motor;
    private GunScript gun;
    private PlayerLook look;
    //public Animator myAnim;

    // Start is called before the first frame update
    void Awake()
    {
        playerInput = new PlayerInput();
        OnFoot = playerInput.OnFoot;
        motor = GetComponent<PlayerMotor>();
        Shoot = playerInput.Shoot;
        gun = GetComponent<GunScript>();
        OnFoot.Jump.performed += ctx => motor.Jump();
        OnFoot.SprintStart.performed += ctx => motor.SprintPressed();
        OnFoot.SprintFinish.performed += ctx => motor.SprintReleased();
        OnFoot.CrouchStart.performed += ctx => motor.CrouchPressed();
        OnFoot.CrouchFinish.performed += ctx => motor.CrouchReleased();
        OnFoot.Reload.started += Reload;        
        look = GetComponent<PlayerLook>();
    }

    private void Reload(InputAction.CallbackContext obj)
    {
      
        motor.GetComponent<PlayerMotor>().StartCoroutine("Reload");
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // le dice al PlayerMotor que se mueva 
        motor.ProcessMove(OnFoot.Movement.ReadValue<Vector2>());
      
    }

    void LateUpdate()
    {
        look.ProcessLook(OnFoot.Look.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        OnFoot.Enable();
    }
    private void OnDisable()
    {
        OnFoot.Disable();
    }

    /*private void ShootEnable()
    {
        Shoot.Enable();
    }
    private void ShootDisable()
    {
        Shoot.Disable();
    }*/
}
