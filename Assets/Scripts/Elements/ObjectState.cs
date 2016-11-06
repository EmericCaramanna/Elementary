using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectState : MonoBehaviour
{
    public enum State
    {
        WET             = 1,
        FLAMMABLE       = 2,
        BURNING         = 4,
        CONDUCTOR       = 8,
        ELECTRIFIED     = 16,
        WINDABLE        = 32
    }

    public enum WindTranslation
    {
        HORIZONTAL      = 1,
        VERTICAL        = 2
    }

    // Current object state
    public short _state;
    // Time of this object to propagate fire
    public float _timeToPropagate;
    // How long this object can stay in ectricity state
    public float _timeStayElec;
    // Time resostance to fire
    public float _timeToDestroy;
    // Current time of fire propagation
    private float _propagation;
    // Time remaning before ectricity state stop
    private float _conduction;
    // Time remaning before self destruction by fire
    private float _destruction;
    private Vector2 _pos;
    private Color _oldColor;
    [SerializeField] private WindTranslation _windTranslate;

    // Is gameObject is on propagation
    public bool _onPropagation;
    // Is gameObject is on conduction
    public bool _onConduction;
    // Is gameObject is on self destruction
    public bool _onDestruction;
    private bool _isDestruct;
    public bool _canBeWet;

    private FirePropagation _fireProp;
    private GameObject _fireObject;
    private GameObject _waterParticle;
    private GameObject _steamParticle;
    private GameObject _elecParticle;

    private List<GameObject> currentCollisions = new List<GameObject>();

    // Use this for initialization
    void Start()
    {
        if (transform.FindChild("FirePropagation"))
            _fireObject = transform.FindChild("FirePropagation").gameObject;
        if (transform.FindChild("WaterStateParticle"))
            _waterParticle = transform.FindChild("WaterStateParticle").gameObject;
        if (transform.FindChild("SteamParticle"))
            _steamParticle = transform.FindChild("SteamParticle").gameObject;
        if (transform.FindChild("ElecStateParticle"))
            _elecParticle = transform.FindChild("ElecStateParticle").gameObject;
        _pos = transform.position;
        if (_onPropagation == true)
        {
            _propagation = _timeToPropagate;
            _fireObject.SetActive(true);
            if (GetComponentInChildren<FirePropagation>())
            {
                _fireProp = GetComponentInChildren<FirePropagation>();
                _fireProp.SetPosition(_pos);
                _fireProp.Begin();
            }
            PlayerKiller.BecomePK(gameObject);
        }
        if (_onDestruction == true)
            _destruction = _timeToDestroy;
        _isDestruct = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_onPropagation == true)
        {
            _propagation -= Time.deltaTime;
            if (_propagation < 1)
            {
                foreach (GameObject gObject in currentCollisions)
                {
                    if (gObject)
                    {
                        if (gObject.GetComponent<ObjectState>() != null)
                        {
                            if (gameObject.GetComponentInChildren<ObjectState>()._isDestruct == true)
                                currentCollisions.Remove(gObject);
                            else
                                gObject.GetComponentInChildren<ObjectState>().SetState(gObject.GetComponentInChildren<ObjectState>().GetNewState(_state));
                        }
                    }
                }
            }
        }
        if (_onConduction == true)
        {
            _conduction -= Time.deltaTime;
            foreach (GameObject gObject in currentCollisions)
            {
                if (gObject && gObject.GetComponentInChildren<ObjectState>() != null)
                {
                    gObject.GetComponentInChildren<ObjectState>().SetState(gObject.GetComponentInChildren<ObjectState>().GetNewState(_state));
                }
            }
            if (_conduction <= 0)
            {
                _elecParticle.SetActive(false);
                _conduction = _timeStayElec;
                _onConduction = false;
                _state -= (short)State.ELECTRIFIED;
                if (GetComponentInParent<ObjectGeneration>())
                    GetComponentInParent<ObjectGeneration>().StopElec();

                    PlayerKiller.LosePK(gameObject);
            }
        }
        if (_onDestruction == true)
        {
            _destruction -= Time.deltaTime;
            if (_destruction < 0 && _destruction > -1)
            {
                _isDestruct = true;
                Destroy(gameObject);
            }
        }
    }

    void    OnCollisionEnter2D(Collision2D collision)
    {
        if (!currentCollisions.Contains(collision.gameObject))
            currentCollisions.Add(collision.gameObject);

        if (_onDestruction && collision.gameObject.tag == "Player")
        {
            GameObject[] objs = GameObject.FindGameObjectsWithTag("Checkpoint");
            foreach (GameObject obj in objs)
            {
                Checkpoint chk = obj.GetComponent<Checkpoint>();
                if (chk != null && chk.activeCheckpoint)
                {
                    chk.Respawn(gameObject);
                }
            }
        }
    }

    //void OnCollisionStay2D(Collision2D collision)
    //{

    //}

    void    OnCollisionExit2D(Collision2D collision)
    {
        currentCollisions.Remove(collision.gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!currentCollisions.Contains(collision.gameObject))
            currentCollisions.Add(collision.gameObject);

        if (_onDestruction && collision.gameObject.tag == "Player")
        {
            GameObject[] objs = GameObject.FindGameObjectsWithTag("Checkpoint");
            foreach (GameObject obj in objs)
            {
                Checkpoint chk = obj.GetComponent<Checkpoint>();
                if (chk != null && chk.activeCheckpoint)
                {
                    chk.Respawn(gameObject);
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        currentCollisions.Remove(collision.gameObject);
    }

    /*
    *   Return a new state depend on the state in parameter
    */
    public short    GetNewState(short state)
    {
        short newState = state;
        if ((state & (short)State.BURNING) != 0)
        {
            newState = SetStateWFire();
        }
        else if ((state & (short)State.WET) != 0)
        {
            newState = SetStateWWater();
        }
        if ((state & (short)State.ELECTRIFIED) != 0)
        {
            newState = SetStateWElec();
        }
        return newState;
    }

    /*
    *   Return a new state depend on a fire state
    */
    public short SetStateWFire()
    {
        if ((_state & (short)State.WET) != 0)
        {
            _waterParticle.SetActive(false);
            _steamParticle.GetComponent<ParticleSystem>().Play();
            _propagation = _timeToPropagate;
            return (short)(_state - (short)State.WET);
        }
        else if ((_state & (short)State.FLAMMABLE) != 0 && (_state & (short)State.BURNING) == 0)
        {
            _propagation = _timeToPropagate;
            _destruction = _timeToDestroy;
            _onDestruction = true;
            _onPropagation = true;
            if (GetComponentInParent<ObjectGeneration>())
                GetComponentInParent<ObjectGeneration>()._isInFire = true;
            _waterParticle.SetActive(false);
            _fireObject.SetActive(true);
            if (GetComponentInChildren<FirePropagation>())
            {
                _fireProp = GetComponentInChildren<FirePropagation>();
                _fireProp.Begin();
                _fireProp.SetPosition(_pos);
            }
            if (gameObject.GetComponentInParent<ObjectSound>())
                gameObject.GetComponentInParent<ObjectSound>().playBurning();
            PlayerKiller.BecomePK(gameObject);
            return (short)(_state + (short)State.BURNING);
        }
        return _state;
    }



    /*
    *   Return a new state depend on a water state
    */
    public short SetStateWWater()
    {
        if ((_state & (short)State.WET) == 0 && (_state & (short)State.BURNING) == 0 && _canBeWet)
        {
            _waterParticle.SetActive(true);
            return (short)(_state + (short)State.WET);
        }
        if ((_state & (short)State.BURNING) != 0)
        {
            _onDestruction = false;
            _onPropagation = false;
            _steamParticle.GetComponent<ParticleSystem>().Play();
            _fireObject.SetActive(false);
            gameObject.GetComponentInParent<ObjectSound>().playExtinguish();
            PlayerKiller.LosePK(gameObject);
            return (short)(_state - (short)State.BURNING);
        }
        return _state;
    }

    /*
    *   Return a new state depend on a electricity state
    */
    public short SetStateWElec()
    {
        if ((_state & (short)State.ELECTRIFIED) == 0 && ((_state & (short)State.CONDUCTOR) != 0 || (_state & (short)State.WET) != 0))
        {

            _conduction = _timeStayElec;
            _onConduction = true;
            _elecParticle.SetActive(true);
            PlayerKiller.BecomePK(gameObject);
            return (short)(_state + (short)State.ELECTRIFIED);
        }
        return _state;
    }

    public bool isState(int state)
    {
        return (_state & (short)state) != 0;
    }

    public bool isOnlyState(int state)
    {
        return _state == state;
    }

    public void SetState(short newState)
    {
        _state = newState;
        //Debug.Log(_state);
    }

    public void SetPosition(Vector2 pos)
    {
        _pos = pos;
    }

    public void SetWindTranslate(WindTranslation windTranslate)
    {
        _windTranslate = windTranslate;
    }

    public short GetState() { return _state; }
    public float GetPropagation() { return _propagation; }
    public float GetDestruction() { return _destruction; }
    public WindTranslation GetWindTranslate() { return _windTranslate; }
}
