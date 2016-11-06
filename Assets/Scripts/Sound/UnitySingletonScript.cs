using UnityEngine;
using System.Collections;

// This script is to be attached to gameObject
// that aren't meant to be destroyed when loading a new scene
public class UnitySingletonScript : MonoBehaviour
{
    private static UnitySingletonScript instance = null;
    public static UnitySingletonScript Instance
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
    }
}
