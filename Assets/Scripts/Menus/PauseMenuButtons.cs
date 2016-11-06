using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PauseMenuButtons : MonoBehaviour
{
    [SerializeField]
    GameObject Options;
    [SerializeField]
    GameObject HowToPlay;
    [SerializeField]
    GameObject Credits;
    [SerializeField]
    GameObject Destructive;

    public bool OnMenu = false;

    public void ResumeGamePress()
    {
        if (GameObject.FindGameObjectWithTag("MainCamera") != null)
        {
            if (GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PauseMenu>())
            {
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PauseMenu>().isPaused = false;
                Time.timeScale = 1.0f;
                OnMenu = false;
                gameObject.SetActive(false);
            }
        }
    }

    void OnEnable()
    {
        if (GameObject.FindGameObjectWithTag("MainCamera") != null)
        {
            if (GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PauseMenu>())
            {
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PauseMenu>().restart = false;
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PauseMenu>().main = false;
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PauseMenu>().quit = false;
            }
        }
    }

    public void MainMenuPress()
    {
        if (GameObject.FindGameObjectWithTag("MainCamera") != null)
        {
            if (GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PauseMenu>())
            {
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PauseMenu>().restart = false;
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PauseMenu>().main = true;
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PauseMenu>().quit = false;
            }
        }
        if (Destructive)
        {
            gameObject.SetActive(false);
            Destructive.SetActive(true);
        }
    }

    public void QuitGamePress()
    {
        if (GameObject.FindGameObjectWithTag("MainCamera") != null)
        {
            if (GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PauseMenu>())
            {
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PauseMenu>().restart = false;
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PauseMenu>().main = false;
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PauseMenu>().quit = true;
            }
        }
        if (Destructive)
        {
            gameObject.SetActive(false);
            Destructive.SetActive(true);
        }
    }

    public void RestartPress()
    {
        if (GameObject.FindGameObjectWithTag("MainCamera") != null)
        {
            if (GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PauseMenu>())
            {
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PauseMenu>().restart = true;
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PauseMenu>().main = false;
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PauseMenu>().quit = false;
            }
        }
        if (Destructive)
        {
            gameObject.SetActive(false);
            Destructive.SetActive(true);
        }
    }

    public void SettingPress()
    {
        if (Options)
        {
            gameObject.SetActive(false);
            Options.SetActive(true);
        }
    }

    public void HowToPlayPress()
    {
        if (HowToPlay)
        {
            gameObject.SetActive(false);
            HowToPlay.SetActive(true);
        }
    }

    public void CreditsPress()
    {
        if (Credits)
        {
            gameObject.SetActive(false);
            Credits.SetActive(true);
        }
    }
}
