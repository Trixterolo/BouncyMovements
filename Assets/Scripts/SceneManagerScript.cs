using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ExitGame()
    {
        Debug.Log("Quitting the game");
        Application.Quit();

        //if(Input.Ge)
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}
