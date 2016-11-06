using UnityEngine;
using System.Collections;

public class HowToPlayEnable : MonoBehaviour {

    public void OnEnable()
    {
        if (GameObject.FindGameObjectWithTag("MainCamera"))
        {
            Vector3 pos = GameObject.FindGameObjectWithTag("MainCamera").transform.position;
            pos.z = 0;
            gameObject.transform.position = pos;
        }
    }
}
