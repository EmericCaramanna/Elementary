using UnityEngine;
using System.Collections;

public class ManageParticle : MonoBehaviour {

    private Vector3 _dir;
    private Vector3 _initPos;
    private Color _startColor;

    // Use this for initialization
    void Start () {
        if (GetComponent<ParticleSystem>())
            _startColor = GetComponent<ParticleSystem>().startColor;
        _initPos = transform.parent.position;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void    Hide(Vector3 pos, float opacity)
    {
        //_dir = pos - transform.position;
        //if (transform.parent.localPosition.y >= 0f)
        //    transform.parent.Translate(_dir.x * (Time.deltaTime), 0, -_dir.y * (Time.deltaTime * 0.5f), Space.Self);
        //else
            gameObject.SetActive(false);
        //if (GetComponent<ParticleSystem>())
        //    GetComponent<ParticleSystem>().startColor = new Color(_startColor.r, _startColor.g, _startColor.b, Mathf.Lerp(_startColor.a, 0.0f, Time.time * 2f));
    }

    public void    Show(Vector3 pos, float opacity)
    {
        gameObject.SetActive(true);
        //_dir = _initPos - transform.parent.position;
        //if (transform.parent.localPosition.y <= _initPos.y + 3)
        //    transform.parent.Translate(_dir.x * (Time.deltaTime), 0, _dir.y * (Time.deltaTime * 0.5f), Space.Self);
        //if (GetComponent<ParticleSystem>())
        //    GetComponent<ParticleSystem>().startColor = new Color(_startColor.r, _startColor.g, _startColor.b, Mathf.Lerp(0, _startColor.a, Time.time * 2f));
    }

}
