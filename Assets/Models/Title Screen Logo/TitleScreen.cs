using Microsoft.Win32.SafeHandles;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;


public class TitleScreen : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SelectingTitle(SelectEnterEventArgs _)
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("Escape"))
        {
            BeginGame();
        }

    }
    
     

    void BeginGame()
    {
        SceneManager.LoadScene("Tetris Gameplay");
    }


    
}
