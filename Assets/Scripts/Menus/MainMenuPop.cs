using UnityEngine;
using System.Collections;

// This script is supposed to be attached to a gameobject
// that hide the main menu.
public class MainMenuPop : MonoBehaviour
{
    public GameObject MainCanvas;

    // When the gameObject is destroyed
    // The main menu is activated
    void OnDestroy()
    {
        if (MainCanvas != null)
            MainCanvas.SetActive(true);
    }
}
