using UnityEngine;
using System.Collections;

public class SplashScript : MonoBehaviour {
    private float _lifetime;

	// Use this for initialization
	void Start () {
        if (GetComponent<ParticleSystem>())
        {
            _lifetime = GetComponent<ParticleSystem>().startLifetime;
        }
    }
	
	// Update is called once per frame
	void Update () {
        _lifetime -= Time.deltaTime;
        if (_lifetime <= 0)
            DestroyObject(gameObject);
	}
}
