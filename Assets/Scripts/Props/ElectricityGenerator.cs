using UnityEngine;
using System.Collections;

public class ElectricityGenerator : MonoBehaviour {
    [SerializeField]
    float timeToGenerate;
    float timer = 0.5f;

    ObjectState objState;
    
	void Start ()
    {
        objState = GetComponent<ObjectState>();
	}
	
	void Update ()
    {
        timer += Time.deltaTime;
        if (timer >= timeToGenerate)
        {
            objState.SetState(objState.SetStateWElec());
            timer -= timeToGenerate;
        }
	}
}
