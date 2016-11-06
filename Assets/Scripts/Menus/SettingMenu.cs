using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SettingMenu : MonoBehaviour
{

    public GameObject MainCanvas;
    public GameObject Option;
    public GameObject Setting;
    private GameObject Spawner;
    private GameObject Player;

    void Start()
    {
        Spawner = GameObject.FindGameObjectWithTag("Checkpoint");
    }

    void OnEnable()
    {
        if (GameObject.FindGameObjectWithTag("FullscreenToggle"))
        {
            if (GameObject.FindGameObjectWithTag("FullscreenToggle").GetComponent<Toggle>())
            {
                if (Screen.fullScreen)
                    GameObject.FindGameObjectWithTag("FullscreenToggle").GetComponent<Toggle>().isOn = true;
                else
                    GameObject.FindGameObjectWithTag("FullscreenToggle").GetComponent<Toggle>().isOn = false;
            }
        }
    }

    public void BackButtonSetting()
    {
        if (GameObject.FindGameObjectWithTag("FullscreenToggle"))
        {
            if (GameObject.FindGameObjectWithTag("FullscreenToggle").GetComponent<Toggle>())
            {
                if (GameObject.FindGameObjectWithTag("FullscreenToggle").GetComponent<Toggle>().isOn)
                    Screen.fullScreen = true;
                else
                    Screen.fullScreen = false;
            }
        }
        BackButtonClick();
    }
    public void BackButtonClick()
    {
        if (Spawner != null && (Player = GameObject.FindGameObjectWithTag("Player")) != null)
        {

            MainCanvas.SetActive(true);
            Option.SetActive(true);
            Setting.SetActive(false);
            Player.GetComponent<PlayerHealth>().Die(Spawner, null);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (MainCanvas != null && Option != null)
            {
                MainCanvas.SetActive(false);
                Option.SetActive(false);
                Setting.SetActive(true);
            }
        }
    }
}