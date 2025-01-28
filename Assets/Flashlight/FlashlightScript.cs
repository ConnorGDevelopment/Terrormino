using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FlashlightScript : MonoBehaviour
{
    [SerializeField] private GameObject FlashlightLight; // Assign your flashlight GameObject in the Inspector
    private bool FlashlightActive = false;

    // Start is called before the first frame update
    void Start()
    {
        // Ensure the flashlight starts off
        FlashlightLight.SetActive(false);
    }

    public void FlashLightButton(InputAction.CallbackContext context)
    {
        // Check if the button is pressed 
        if (context.performed)
        {
            FlashlightActive = !FlashlightActive; // Toggle the flashlight state
            FlashlightLight.SetActive(FlashlightActive); // Enable or disable the light
            Debug.Log($"Flashlight toggled: {FlashlightActive}");
        }
    }
}
