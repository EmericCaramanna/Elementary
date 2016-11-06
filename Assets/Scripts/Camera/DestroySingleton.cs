using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

// This Script is to prevent the singleton music object
// to play when there is already another music in the scene.
// See the UnitySingletonScript for more information.
public class DestroySingleton : MonoBehaviour
{
    void Start()
    {
        GameObject singleton = null;
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            if (singleton = GameObject.Find("SingletonMusic"))
            {
                Destroy(singleton);
            }
        }
    }
}
