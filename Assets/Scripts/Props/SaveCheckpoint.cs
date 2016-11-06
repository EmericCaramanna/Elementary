using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SaveCheckpoint : MonoBehaviour {
    public GameObject savecCheckpoint;
    GameObject localPlayer;

    bool isFirstFrame = true;

    private static SaveCheckpoint instance = null;
    public static SaveCheckpoint Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
        savecCheckpoint.GetComponent<Checkpoint>().SpawnPlayer();
    }

    void Update()
    {
        if (isFirstFrame)
        {
            savecCheckpoint.GetComponent<Checkpoint>().SpawnPlayer();
            isFirstFrame = false;
        }
        if (Input.GetButtonDown("Respawn"))
        {
            Respawn();
        }
    }

    public void Respawn()
    {
        isFirstFrame = true;
        Destroy(savecCheckpoint.GetComponent<Checkpoint>().localPlayer);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
