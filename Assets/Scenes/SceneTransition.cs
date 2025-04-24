using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : ScriptableObject
{
    public Scene LivingRoom;
    public Scene Gameplay;
    public Scene MainMenu;


    public void TransitionToLivingRoom()
    {

        SceneManager.LoadScene(LivingRoom.name);

    }

    public void TransitionToGameplay()
    {
        SceneManager.LoadScene(Gameplay.name);
    }

    public void TransitionToMainMenu()
    {
        SceneManager.LoadScene(MainMenu.name);
    }


}
