using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("Escape"))
        {
            LaunchGame();
        }

    }
    
     

    void LaunchGame()
    {
        SceneManager.LoadScene("Tetris Gameplay");
    }


    
}
