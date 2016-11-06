using UnityEngine;
using System.Collections;

public class CheatCode : MonoBehaviour
{
    string _storeInput;

    void Start()
    {
        _storeInput = null;
    }

    void Update()
    {
        if (Input.GetButton("CheatCode"))
        {
            foreach (char c in Input.inputString)
            {
                _storeInput += c;
                if (c == "\b"[0])
                {
                    if (_storeInput.Length != 0)
                        _storeInput = _storeInput.Substring(0, _storeInput.Length - 1);
                }
            }
        }

        if (Input.GetButtonUp("CheatCode"))
        {
            if (_storeInput == "GOD")
            {
                if (gameObject.transform.parent.gameObject.GetComponent<PlayerHealth>().IsImmortal() == false)
                    gameObject.transform.parent.gameObject.GetComponent<PlayerHealth>().SetImmortal(true);
                else
                    gameObject.transform.parent.gameObject.GetComponent<PlayerHealth>().SetImmortal(false);
            }
            else if (_storeInput == "NEXT")
            {
                GameObject endLevel = GameObject.FindGameObjectWithTag("EndLevel");
                if (endLevel)
                {
                    endLevel.GetComponent<EndofLevel>().GoToNextScene();
                }
            }
            _storeInput = "";
        }
    }
}
