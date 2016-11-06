using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

// This script manage the UI of the main menu
public class MainMenuScript : MonoBehaviour
{

    public GameObject HowToPlay;
    public GameObject MainMenu;
    public GameObject Options;
    private GameObject Spawner;
    private GameObject Player;

    void Start()
    {
        Spawner = GameObject.FindGameObjectWithTag("Checkpoint");
    }

    public void HowToPlayClick()
    {
        if (HowToPlay != null && Options != null)
        {
            HowToPlay.SetActive(true);
            Options.SetActive(false);
            gameObject.SetActive(false);
        }
    }
    
    public void BackToMenuClick()
    {
        if (MainMenu != null && Options != null && HowToPlay != false)
        {
            if ((Player = GameObject.FindGameObjectWithTag("Player")) != null && Spawner != null)
            {
                HowToPlay.SetActive(false);
                Options.SetActive(true);
                MainMenu.SetActive(true);
                Player.GetComponent<PlayerHealth>().Die(Spawner, null);
            }
        }
    }
}
