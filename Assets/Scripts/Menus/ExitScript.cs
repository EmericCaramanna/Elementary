using UnityEngine;
using System.Collections;

public class ExitScript : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Exit");
            Application.Quit();
        }
    }
}
