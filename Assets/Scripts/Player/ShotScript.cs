using UnityEngine;
using System.Collections;

// This class manage the shot;
// With the its speed and its direction;
// But also the collider events.
public class ShotScript : MonoBehaviour
{
    private Vector2 Direction;
    [SerializeField]
    private float speed;
    private GameObject _owner;
    private int DestroyDistance = 50;
    ShootScript.Element _elem;
    Vector2 dir;
    [SerializeField]
    private GameObject _fireSplash;
    [SerializeField]
    private GameObject _waterSplash;
    private GameObject _windObject;



    // On start the projectile calculate its directional vector
    // and the color of the projectile is set according to the element of the gun
    void Start()
    {
        Vector2 playerPosition;
        Vector2 cursorPosition;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        playerPosition = gameObject.transform.position;
        cursorPosition = ray.origin;
        Direction.x = cursorPosition.x - playerPosition.x;
        Direction.y = cursorPosition.y - playerPosition.y;
        Direction.Normalize();

        //GetComponent<ParticleSystem>().startColor = _owner.GetComponent<ShootScript>().GetColor();
        _elem = _owner.GetComponent<ShootScript>().GetElement();
        switch (_elem)
        {
            case ShootScript.Element.FIRE:
                //gameObject.GetComponent<ParticleSystem>().startColor = new Color(1, 0, 0, .5f);
                if (transform.FindChild("FireParticle"))
                    transform.FindChild("FireParticle").gameObject.SetActive(true);

                speed = 0.2f;
                break;
            case ShootScript.Element.WATER:
                //gameObject.GetComponent<ParticleSystem>().startColor = new Color(0, 0, 1, .5f);
                if (transform.FindChild("WaterParticle"))
                    transform.FindChild("WaterParticle").gameObject.SetActive(true);
                GetComponent<Rigidbody2D>().gravityScale = 0.5f;
                speed = 0.2f;
                break;
            case ShootScript.Element.ELECTRICITY:
                if (transform.FindChild("ElecParticle"))
                    transform.FindChild("ElecParticle").gameObject.SetActive(true);
                speed = 0.6f;
                break;
        }
    }

    void FixedUpdate()
    {
        if (Time.timeScale != 0)
        {
            gameObject.transform.Translate(Direction * speed);
            if (gameObject && _owner && Vector2.Distance(transform.position, _owner.transform.position) > DestroyDistance)
                DestroyObject(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<ObjectState>())
        {
            ObjectState otherState = collision.gameObject.GetComponent<ObjectState>();
            switch (_elem)
            {
                case ShootScript.Element.FIRE:
                    if (_fireSplash)
                        Instantiate(_fireSplash, transform.position, transform.rotation);
                    otherState.SetState(otherState.SetStateWFire());
                    otherState.SetPosition(transform.position);
                    break;
                case ShootScript.Element.WATER:
                    if (_waterSplash)
                        Instantiate(_waterSplash, transform.position, transform.rotation);
                    collision.GetComponentInParent<ObjectGeneration>().ExtinguishFire();
                    Debug.Log("Water");
                    break;
                case ShootScript.Element.ELECTRICITY:
                    otherState.SetState(otherState.SetStateWElec());
                    //Debug.Log(otherState.GetState());
                    Debug.Log("Electricity");
                    break;
            }
        }
        if (collision.gameObject.tag != "Player" && collision.gameObject.tag != "Checkpoint" && collision.gameObject.tag != "DontTrigger" && collision.gameObject.tag != "Projectile")
            DestroyObject(gameObject);
    }

    public void SetOwner(GameObject owner)
    {
        _owner = owner;
    }

    public ShootScript.Element GetElement()
    {
        return (_elem);
    }

    public void SetWindObject(GameObject windObject)
    {
        _windObject = windObject;
    }
}
