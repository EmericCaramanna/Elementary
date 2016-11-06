using UnityEngine;
using System.Collections;

public class ObjectGeneration : MonoBehaviour {

    private Vector2 _size;
    private int _horizontalObjNb;
    private int _verticalObjNb;
    private Vector2 _topLeft;
    [SerializeField] private GameObject _objectToGenerate;
    private GameObject objTmp;
    private Vector2 _hitPos;
    [SerializeField] private float _sizeCube;
    private ObjectState ObjStateScript;
    [SerializeField] private bool _withRigidBody;
    private ObjectState[] _listObjectState;
    [SerializeField] private bool _canBeWet;
    public bool _isInFire;
    [SerializeField] private ObjectState.WindTranslation _windTranslate;



    // Current object state
    public short _state;
    // Time of this object to propagate fire
    public float _timeToPropagate;
    // How long this object can stay in ectricity state
    public float _timeStayElec;
    // Time resostance to fire
    public float _timeToDestroy;
    // Is gameObject is on propagation
    public bool _onPropagation;
    // Is gameObject is on conduction
    public bool _onConduction;
    // Is gameObject is on self destruction
    public bool _onDestruction;

    // Use this for initialization
    void Start () {
	    if (GetComponent<BoxCollider2D>())
        { 
            _size = GetComponent<BoxCollider2D>().size;
            _horizontalObjNb = (int)(_size.x / _sizeCube);
            _verticalObjNb = (int)(_size.y / _sizeCube);
            _topLeft = (Vector2)transform.position - (_size / 2);
            for (float y = 0; y < _verticalObjNb; y++)
            {
                for (float x = 0; x < _horizontalObjNb; ++x)
                {
                    objTmp = (Instantiate(_objectToGenerate, new Vector2(_topLeft.x + _sizeCube / 2 + x * _sizeCube, _topLeft.y + _sizeCube / 2 + y *_sizeCube), transform.rotation) as GameObject);
                    if (objTmp)
                        objTmp.transform.SetParent(transform);
                    objTmp.transform.localScale = new Vector2(_size.x / (_horizontalObjNb * 2), _size.y / (_verticalObjNb * 2));
                    if (objTmp.GetComponent<ObjectState>())
                    {
                        ObjStateScript = objTmp.GetComponent<ObjectState>();
                        ObjStateScript._state = _state;
                        ObjStateScript._timeToPropagate = _timeToPropagate;
                        ObjStateScript._timeStayElec = _timeStayElec;
                        ObjStateScript._timeToDestroy = _timeToDestroy;
                        ObjStateScript._onPropagation = _onPropagation;
                        ObjStateScript._onDestruction = _onDestruction;
                        ObjStateScript._onConduction = _onConduction;
                        ObjStateScript._canBeWet = _canBeWet;
                        ObjStateScript.SetWindTranslate(_windTranslate);
                    }
                    if (objTmp.GetComponent<Rigidbody2D>() && GetComponent<Rigidbody2D>())
                    {
                        objTmp.GetComponent<Rigidbody2D>().gravityScale = GetComponent<Rigidbody2D>().gravityScale;
                        objTmp.GetComponent<Rigidbody2D>().mass = GetComponent<Rigidbody2D>().mass;
                        objTmp.GetComponent<Rigidbody2D>().constraints = GetComponent<Rigidbody2D>().constraints;
                        GetComponent<Rigidbody2D>().Sleep();
                        //if (!_withRigidBody)
                        //    objTmp.GetComponent<Rigidbody2D>().Sleep();
                    }
                    if (GetComponent<BoxCollider2D>().isTrigger)
                        objTmp.GetComponent<BoxCollider2D>().isTrigger = true;
                    objTmp.layer = gameObject.layer;

                }
            }
            GetComponent<BoxCollider2D>().enabled = false;
        }
        
        //DestroyObject(gameObject);
	}

    public void ExtinguishFire()
    {
        _listObjectState = GetComponentsInChildren<ObjectState>();
        foreach (ObjectState gs in _listObjectState)
        {
            if (gs)
            {
                if (_isInFire)
                {
                    if ((gs.GetState() & (short)ObjectState.State.BURNING) != 0)
                        gs.SetState(gs.SetStateWWater());
                }
                else
                {
                    gs.SetState(gs.SetStateWWater());
                }
            }
        }
        if (_isInFire)
            _isInFire = false;
    }

    public void StopElec()
    {
        _listObjectState = GetComponentsInChildren<ObjectState>();
        foreach (ObjectState gs in _listObjectState)
        {
            if (gs)
            {
                if ((gs.GetState() & (short)ObjectState.State.ELECTRIFIED) != 0)
                {
                    short tmp = gs.GetState();
                    tmp -= (short)ObjectState.State.ELECTRIFIED;
                    gs.SetState(tmp);
                }
            }
        }
    }
	
    void SetHiPosition(Vector2 pos)
    {

    }
}
