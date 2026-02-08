using UnityEngine;

public class NightCompletion : MonoBehaviour
{
    // Start is called before the first frame update



    // This script will look at the win condition, that being the score being reached.
    // Once reached the light will fade to black and the player will have beaten the night
    // This script will cause the player to then change the scene back to the living room
    // This script will keep repeating this process each night
    // This script could also handle which night the game will be starting on next

    private string _nightName;
    public ScenePicker scenePicker;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
