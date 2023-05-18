using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI promptText;
    public Slider Xslider;
    public Slider Yslider;
    public TextMeshProUGUI X;
    public TextMeshProUGUI Y;
    // Start is called before the first frame update
    void Start()
    {
        PlayerMotor.currentHealth = 100;
        Xslider.value = PlayerLook.xSensitivity;
        Yslider.value = PlayerLook.ySensitivity;        
    }

    private void Update()
    {
        if (PlayerMotor.currentHealth <= 0)
        {
            SceneManager.LoadScene("Muerte");
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        X.text = PlayerLook.xSensitivity.ToString();
        Y.text = PlayerLook.ySensitivity.ToString(); 
        
        
    }

    public void UpdateText(string promptMessage)
    {
        promptText.text = promptMessage;
    }

    public void Play()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void Options()
    {
        Debug.Log("Options");
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Menu()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene("Menu");
    }

    

    public void xSensitivity()
    {
        PlayerLook.xSensitivity = Xslider.value;
    }

    public void ySensitivity()
    {
        PlayerLook.ySensitivity = Yslider.value;
    }
}
