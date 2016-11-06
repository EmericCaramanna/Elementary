using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


// This script is to be added on
// the main Camera of each level
public class PauseMenu : MonoBehaviour
{
    public bool isPaused = false;
    [SerializeField]
    GameObject PauseMenuObject;

    public bool restart = false, quit = false, main = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = true;
            if (isPaused)
            {
                if (PauseMenuObject && !PauseMenuObject.GetComponent<PauseMenuButtons>().OnMenu)
                {
                    PauseMenuObject.SetActive(true);
                    PauseMenuObject.GetComponent<PauseMenuButtons>().OnMenu = true;
                    Time.timeScale = 0.0f;
                }
            }
        }
    }
}
