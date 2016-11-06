using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SettingPauseMenu : MonoBehaviour {

    [SerializeField]
    GameObject PauseCanvas;

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
        GameObject musicSlider = GameObject.FindGameObjectWithTag("MusicSlider");
        GameObject soundSlider = GameObject.FindGameObjectWithTag("SoundSlider");
        GameObject volumeSettings = GameObject.FindGameObjectWithTag("VolumeSettings");
        if (volumeSettings)
        {
            if (musicSlider)
                musicSlider.GetComponent<Slider>().value = volumeSettings.GetComponent<VolumeSettings>().musicValue;
            if (soundSlider)
                soundSlider.GetComponent<Slider>().value = volumeSettings.GetComponent<VolumeSettings>().soundsValue;
        }

    }

    public void BackButtonClick()
    {
        if (PauseCanvas)
        {
            PauseCanvas.SetActive(true);
            gameObject.SetActive(false);
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
        if (PauseCanvas)
        {
            PauseCanvas.SetActive(true);
            gameObject.SetActive(false);
        }
    }
    public void YesPress()
    {
        if (GameObject.FindGameObjectWithTag("MainCamera") != null)
        {
            if (GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PauseMenu>())
            {
                if (GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PauseMenu>().main)
                {
                    GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PauseMenu>().isPaused = false;
                    Time.timeScale = 1.0f;
                    Destroy(GameObject.Find("CheckpointSaver"));
                    SceneManager.LoadScene("MainMenu");

                }
                else if (GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PauseMenu>().restart)
                {

                    GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PauseMenu>().isPaused = false;
                    Time.timeScale = 1.0f;
                    Destroy(GameObject.Find("CheckpointSaver"));
                    SceneManager.LoadScene("M_01");
                }
                else if (GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PauseMenu>().quit)
                {
                    Application.Quit();
                }
            }
        }
    }
}
