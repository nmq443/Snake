using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    public void Menu()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("quit game");
    }
}
