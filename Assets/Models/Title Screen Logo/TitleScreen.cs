using Microsoft.Win32.SafeHandles;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;
using Helpers;


public class TitleScreen : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        
    }

    

    // Update is called once per frame
    void Update()
    {
        

    }
    
     

    public void BeginGame()
    {
        SceneManager.LoadScene("Tetris Gameplay");
    }


    public UnityEvent<InputAction> OnTitleTransitionGrab = new();

    public void TitleToGameplayTransition(InputAction inputAction)
    {
        int triggerInput = Math.RoundNearestNonZeroInt(inputAction.ReadValue<float>());
        UnityEngine.Debug.Log(triggerInput);
        if (triggerInput == 1)
        {
            BeginGame();
        }
        
    }
}


    

