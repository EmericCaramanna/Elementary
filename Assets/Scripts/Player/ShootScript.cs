using UnityEngine;
using System.Collections;

// This script is atached to the gun gameObject
// It manages the elements and the shooting of the projectiles.
public class ShootScript : MonoBehaviour
{
    public GameObject ProjectilePrefab;
    private Element _elem;
    private bool _fire = true, _water = true, _wind = true, _elec = true;
    private float t = 0;
    private ParticleSystem.EmissionModule _fireEmission;
    private ParticleSystem.EmissionModule _waterEmission;
    private GameObject _fireModule;
    private GameObject _waterModule;
    private GameObject _elecModule;
    private GameObject _windModule;
    private Vector2 _dir;
    private GameObject _windObject;

    public AudioClip fireClip;
    public AudioClip waterClip;
    public AudioClip elecClip;
    public AudioClip windClip;

    public enum Element
    {
        FIRE = 0,
        WATER = 1,
        WIND = 2,
        ELECTRICITY = 3,
        NONE = 4
    }

    // The gun starts the level with no element activated
    void Start()
    {
        _elem = Element.NONE;
        _fireModule = transform.FindChild("FireParticle").gameObject;
        _waterModule = transform.FindChild("WaterParticle").gameObject;
        _elecModule = transform.FindChild("ElecParticle").gameObject;
        _fireEmission = _fireModule.GetComponent<ParticleSystem>().emission;
        _fireEmission.enabled = false;
    }

    // Function used to defined which elements can be used for the current level
    public void SetElements(bool fire, bool water, bool wind, bool elec)
    {
        _fire = fire;
        _water = water;
        _elec = elec;
        _wind = wind;
    }

    public void AddElement(Element elem)
    {
        switch (elem)
        {
            case Element.FIRE:
                _fire = true;
                break;
            case Element.WATER:
                _water = true;
                break;
            case Element.WIND:
                _wind = true;
                break;
            case Element.ELECTRICITY:
                _elec = true;
                break;
        }
    }

    void Update()
    {
        t += Time.deltaTime;
        if (Input.GetButtonDown("Fire1") && _elem != Element.NONE)
        {
            if (t > 0.5f)
            {
                GameObject projectile = (GameObject)Instantiate(ProjectilePrefab, transform.position, Quaternion.identity);
                if (projectile != null)
                {
                    projectile.GetComponent<ShotScript>().SetOwner(gameObject);
                    //projectile.transform.parent = gameObject.transform;
                    if (_elem == Element.FIRE)
                        gameObject.GetComponent<AudioSource>().clip = fireClip;
                    else if (_elem == Element.WATER)
                        gameObject.GetComponent<AudioSource>().clip = waterClip;
                    else if (_elem == Element.ELECTRICITY)
                        gameObject.GetComponent<AudioSource>().clip = elecClip;
                    gameObject.GetComponent<AudioSource>().Play();

                }
                t = 0;
            }
        }
        else if ((Input.GetButtonDown("Fire2") || (Input.GetButtonDown("Fire1"))) && _elem == Element.WIND)
        {
                _windObject = Physics2D.Raycast(transform.position, _dir).collider.gameObject;
        }
        else if ((Input.GetButtonDown("Fire2") || (Input.GetButtonDown("Fire1"))) && _elem == Element.WIND)
        {
            if (_windObject)
                _windObject = null;
        }
        else if (Input.GetButtonDown("FireState") && _fire)
        {
            SetElement(Element.FIRE);
        }
        else if (Input.GetButtonDown("WaterState") && _water)
        {
            SetElement(Element.WATER);
        }
        else if (Input.GetButtonDown("ElectricityState") && _elec)
        {
            SetElement(Element.ELECTRICITY);
        }
        else if (Input.GetButtonDown("WindState") && _wind)
        {
            SetElement(Element.WIND);
        }
        Vector2 ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _dir.x = ray.x - transform.parent.position.x;
        _dir.y = ray.y - transform.parent.position.y;
        transform.position = (Vector2)transform.parent.position + _dir.normalized * 0.8f;
        if (Input.GetButton("Fire2") && _elem == Element.WIND)
        {
            if (_windObject)
            {
                if (_windObject.GetComponent<ObjectState>())
                {
                    if (_windObject.GetComponent<ObjectState>().GetWindTranslate() == ObjectState.WindTranslation.HORIZONTAL)
                        _windObject.transform.position = Vector3.Lerp(_windObject.transform.position, new Vector3(transform.position.x, _windObject.transform.position.y, _windObject.transform.position.z), Time.deltaTime * 5);
                    else if (_windObject.GetComponent<ObjectState>().GetWindTranslate() == ObjectState.WindTranslation.VERTICAL)
                        _windObject.transform.position = Vector3.Lerp(_windObject.transform.position, new Vector3(_windObject.transform.position.x, transform.position.y, _windObject.transform.position.z), Time.deltaTime * 5);
                }
            }
        }
        if (Input.GetButton("Fire1") && _elem == Element.WIND)
        {
            if (_windObject)
            {
                if (_windObject.GetComponent<ObjectState>())
                {
                    if (_windObject.GetComponent<ObjectState>().GetWindTranslate() == ObjectState.WindTranslation.HORIZONTAL)
                        _windObject.transform.position = Vector3.Lerp(_windObject.transform.position, new Vector3(_windObject.transform.position.x + _dir.normalized.x, _windObject.transform.position.y, _windObject.transform.position.z), Time.deltaTime * 5);
                    else if (_windObject.GetComponent<ObjectState>().GetWindTranslate() == ObjectState.WindTranslation.VERTICAL)
                        _windObject.transform.position = Vector3.Lerp(_windObject.transform.position, new Vector3(_windObject.transform.position.x, _windObject.transform.position.y + 1, _windObject.transform.position.z), Time.deltaTime * 5);
                }
            }
        }
    }


    public void SetElement(Element elem)
    {
        if (elem == Element.FIRE && _fire)
        {
            _fireModule.SetActive(true);
            _waterModule.SetActive(false);
            _elecModule.SetActive(false);
        }
        else if (elem == Element.WATER && _water)
        {
            _fireModule.SetActive(false);
            _waterModule.SetActive(true);
            _elecModule.SetActive(false);
        }
        else if (elem == Element.ELECTRICITY && _elec)
        {
            _elecModule.SetActive(true);
            _fireModule.SetActive(false);
            _waterModule.SetActive(false);
        }
        else if (elem == Element.WIND && _wind)
        {

        }
        else if (elem == Element.NONE)
        {
            _fireModule.SetActive(false);
            _waterModule.SetActive(false);
            _elecModule.SetActive(false);
        }
        _elem = elem;
    }

    public Element GetElement()
    {
        return (_elem);
    }

    public GameObject GetWindObject()
    {
        return _windObject;
    }
}
