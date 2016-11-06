using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

// This script is used to manage the ui of the level selection screen
// It is to be attached to the level selection canvas 
public class LevelSelectionScript : MonoBehaviour {

    // Represent the main canvas of the main menu
    // It is used to switch between the level selection and the main menu
    public GameObject MainCanvas;
    public GameObject Option;
    public GameObject Levels;
    private GameObject Spawner;
    private GameObject Player;

    void Start()
    {
        Spawner = GameObject.FindGameObjectWithTag("Checkpoint");
    }

    // Activates the main canvas and desactivate the level canvas
    public void BackButtonClick()
    {
        if (Spawner != null && (Player = GameObject.FindGameObjectWithTag("Player")) != null)
        {
            MainCanvas.SetActive(true);
            Option.SetActive(true);
            Levels.SetActive(false);
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
                Levels.SetActive(true);
            }
        }
    }

    // The following function are here to manage the level selection buttons.
    public void MoveOneClick()
    {
        Destroy(GameObject.Find("CheckpointSaver"));
        SceneManager.LoadScene("M_01");
    }
    public void MoveTwoClick()
    {
        Destroy(GameObject.Find("CheckpointSaver"));
        SceneManager.LoadScene("M_02");
    }
    public void MoveThreeClick()
    {
        Destroy(GameObject.Find("CheckpointSaver"));
        SceneManager.LoadScene("M_03");
    }
    public void MoveFourClick()
    {
        Destroy(GameObject.Find("CheckpointSaver"));
        SceneManager.LoadScene("M_L_01");
    }
    public void FireOneClick()
    {
        Destroy(GameObject.Find("CheckpointSaver"));
        SceneManager.LoadScene("F_01");
    }
    public void FireTwoClick()
    {
        Destroy(GameObject.Find("CheckpointSaver"));
        SceneManager.LoadScene("F_02");
    }
    public void FireThreeClick()
    {
        Destroy(GameObject.Find("CheckpointSaver"));
        SceneManager.LoadScene("F_03");
    }
    public void FireFourClick()
    {
        Destroy(GameObject.Find("CheckpointSaver"));
        SceneManager.LoadScene("F_L_01");
    }
    public void WaterOneClick()
    {
        Destroy(GameObject.Find("CheckpointSaver"));
        SceneManager.LoadScene("W_01");
    }
    public void WaterTwoClick()
    {
        Destroy(GameObject.Find("CheckpointSaver"));
        SceneManager.LoadScene("W_02");
    }
    public void WaterThreeClick()
    {
        Destroy(GameObject.Find("CheckpointSaver"));
        SceneManager.LoadScene("W_03");
    }
    public void WaterFourClick()
    {
        Destroy(GameObject.Find("CheckpointSaver"));
        SceneManager.LoadScene("W_L_01");
    }
    public void FireWaterOneClick()
    {
        Destroy(GameObject.Find("CheckpointSaver"));
        SceneManager.LoadScene("FW_L_01");
    }
    public void FireWaterTwoClick()
    {
        Destroy(GameObject.Find("CheckpointSaver"));
        SceneManager.LoadScene("FW_L_02");
    }
    public void FireWaterThreeClick()
    {
        Destroy(GameObject.Find("CheckpointSaver"));
        SceneManager.LoadScene("FW_L_03");
    }
    public void ElectricityOneClick()
    {
        Destroy(GameObject.Find("CheckpointSaver"));
        SceneManager.LoadScene("E_01");
    }
    public void ElectricityTwoClick()
    {
        Destroy(GameObject.Find("CheckpointSaver"));
        SceneManager.LoadScene("E_02");
    }
}
