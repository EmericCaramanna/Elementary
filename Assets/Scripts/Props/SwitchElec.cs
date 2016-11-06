using UnityEngine;
using System.Collections;
using System;

public class SwitchElec : MonoBehaviour, IActivateObject
{
    [SerializeField]
    GameObject[] objectsToActivate;
    [SerializeField]
    float timeToSwitchOneMoreTime = 0.2f;
    [SerializeField] Sprite activatedSprite;
    [SerializeField] Sprite desactivatedSprite;

    ObjectState state;
    float currentTime;
    bool isActivated;

    bool enable = true;

    void Start()
    {
        state = GetComponent<ObjectState>();
        currentTime = timeToSwitchOneMoreTime;
    }

    void Update()
    {
        if (!enable)
            return;

        currentTime += Time.deltaTime;

        if (state && state._onConduction && currentTime >= timeToSwitchOneMoreTime)
        {
            isActivated = !isActivated;
            if (isActivated)
            {
                GetComponent<SpriteRenderer>().sprite = activatedSprite;
            }
            else
                GetComponent<SpriteRenderer>().sprite = desactivatedSprite;
            foreach (GameObject obj in objectsToActivate)
            {
                if (obj == null)
                    continue;

                IActivateObject[] activObjs = obj.GetComponents<IActivateObject>();
                foreach (IActivateObject activObj in activObjs)
                {
                    if (isActivated)
                        activObj.Activate();
                    else
                        activObj.Desactivate();
                }
            }
            currentTime = 0f;
        }

    }

    public void Activate()
    {
        enable = true;
    }

    public void Desactivate()
    {
        enable = false;
    }
}
