using UnityEngine;
using System.Collections;

public class ManageTutorial : MonoBehaviour
{
    [SerializeField]
    GameObject Space;
    [SerializeField]
    GameObject A;
    [SerializeField]
    GameObject D;
    [SerializeField]
    GameObject _1;
    [SerializeField]
    GameObject _2;
    [SerializeField]
    GameObject _3;
    [SerializeField]
    GameObject _4;
    [SerializeField]
    GameObject LeftClick;

    bool dPress = false, aPress = false;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CheckDown();
        CheckUp();
    }

    void CheckDown()
    {
        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log("Jump");
            if (Space && Space.GetComponent<SwitchSprite>())
            {
                Space.GetComponent<SwitchSprite>().Switch();
            }
        }
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            if (D && D.GetComponent<SwitchSprite>())
            {
                if (dPress == false)
                {
                    dPress = true;
                    D.GetComponent<SwitchSprite>().Switch();
                }
            }
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            if (A && A.GetComponent<SwitchSprite>())
            {
                if (aPress == false)
                {
                    aPress = true;
                    A.GetComponent<SwitchSprite>().Switch();
                }
            }
        }
        if (Input.GetButtonDown("FireState"))
        {
            if (_1 && _1.GetComponent<SwitchSprite>())
            {
                _1.GetComponent<SwitchSprite>().Switch();
            }
        }
        if (Input.GetButtonDown("WaterState"))
        {
            if (_2 && _2.GetComponent<SwitchSprite>())
            {
                _2.GetComponent<SwitchSprite>().Switch();
            }
        }
        if (Input.GetButtonDown("ElectricityState"))
        {
            if (_3 && _3.GetComponent<SwitchSprite>())
            {
                _3.GetComponent<SwitchSprite>().Switch();
            }
        }
        if (Input.GetButtonDown("WindState"))
        {
            if (_4 && _4.GetComponent<SwitchSprite>())
            {
                _4.GetComponent<SwitchSprite>().Switch();
            }
        }
        if (Input.GetButtonDown("Fire1"))
        {
            if (LeftClick && LeftClick.GetComponent<SwitchSprite>())
            {
                LeftClick.GetComponent<SwitchSprite>().Switch();
            }
        }
    }

    void CheckUp()
    {
        if (Input.GetButtonUp("Jump"))
        {
            if (Space && Space.GetComponent<SwitchSprite>())
            {
                Space.GetComponent<SwitchSprite>().Switch();
            }
        }
        if (Input.GetAxisRaw("Horizontal") == 0)
        {
            if (A && D && A.GetComponent<SwitchSprite>() && D.GetComponent<SwitchSprite>())
            {
                if (dPress == true)
                {
                    D.GetComponent<SwitchSprite>().Switch();
                    dPress = false;
                }
                if (aPress == true)
                {
                    A.GetComponent<SwitchSprite>().Switch();
                    aPress = false;
                }
            }
        }
        if (Input.GetButtonUp("FireState"))
        {
            if (_1 && _1.GetComponent<SwitchSprite>())
            {
                _1.GetComponent<SwitchSprite>().Switch();
            }
        }
        if (Input.GetButtonUp("WaterState"))
        {
            if (_2 && _2.GetComponent<SwitchSprite>())
            {
                _2.GetComponent<SwitchSprite>().Switch();
            }
        }
        if (Input.GetButtonUp("ElectricityState"))
        {
            if (_3 && _3.GetComponent<SwitchSprite>())
            {
                _3.GetComponent<SwitchSprite>().Switch();
            }
        }
        if (Input.GetButtonUp("WindState"))
        {
            if (_4 && _4.GetComponent<SwitchSprite>())
            {
                _4.GetComponent<SwitchSprite>().Switch();
            }
        }
        if (Input.GetButtonUp("Fire1"))
        {
            if (LeftClick && LeftClick.GetComponent<SwitchSprite>())
            {
                LeftClick.GetComponent<SwitchSprite>().Switch();
            }
        }
    }
}
