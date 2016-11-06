using UnityEngine;
using System.Collections;

public class FirePropagation : MonoBehaviour
{
    private Vector2 _pos;
    private Vector2 _toPos;
    private ObjectState _state;
    private Vector2 _distance;
    private ParticleSystem _particle;
    private int _maxParticle = 30;
    private float _maxLifeTime = 1;
    private Vector2 _maxShape;
    
	// Use this for initialization
	void Start ()
    {
        _state = GetComponentInParent<ObjectState>();
        _particle = GetComponent<ParticleSystem>();
        _maxShape = transform.parent.transform.localScale;
        _maxLifeTime = _maxShape.y;
        _particle.gameObject.layer = 1;
        _pos = transform.parent.position;
    }
	
	// Update is called once per frame
	void Update ()
    {
        _particle.transform.position = _pos;
        if (transform.parent)
        {
            UpdateToPosition();
            _distance = _toPos - _pos;
            if ((Vector3)_pos != transform.parent.position)
                _pos += _distance / _state.GetDestruction();
            if ((int)_state.GetDestruction() != 0)
            {
                _maxLifeTime = _maxShape.y;
                _particle.maxParticles = _maxParticle / (int)_state.GetDestruction();
                _particle.emissionRate = _maxParticle / (int)_state.GetDestruction();
                _particle.startLifetime = _maxLifeTime / (int)_state.GetDestruction();
                _particle.shape.box.Scale(_maxShape / _state.GetDestruction());
            }
        }
    }

    public void SetPosition(Vector2 pos)
    {
        _pos = (Vector3)pos - (transform.parent.position);
    }

    private void UpdateToPosition()
    {
        _toPos = transform.parent.position;
        _toPos.y -= transform.parent.localScale.y / 2;
    }

    public void Begin()
    {
        gameObject.SetActive(true);
    }
}