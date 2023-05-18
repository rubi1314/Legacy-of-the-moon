using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Camera cam;
    private float xRotation = 0f;
    

    static public float xSensitivity = 30f;
    static public float ySensitivity = 30f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void ProcessLook(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;
        // rotacion de la camara
        xRotation -= (mouseY * Time.deltaTime) * ySensitivity;
        xRotation = Mathf.Clamp(xRotation, -80, 80);
        // aplicar a la camara
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        // rotar al jugador
        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSensitivity);

        
    }
}
