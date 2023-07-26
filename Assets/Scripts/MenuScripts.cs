using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScripts : MonoBehaviour
{
    [SerializeField] GameObject creditPanel;

    private void Start()
    {
        creditPanel.SetActive(false);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void Return()
    {
        creditPanel.SetActive(false);
    }

    public void Credit()
    {
        creditPanel.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("quit game");
    }

}
