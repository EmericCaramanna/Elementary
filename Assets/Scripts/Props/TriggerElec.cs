using UnityEngine;
using System.Collections;
using System;

public class TriggerElec : MonoBehaviour, IActivateObject
{
    [SerializeField]
    GameObject[] objectsToActivate;

    ObjectState state;
    bool isActivated;

    bool enable = true;

    void Start()
    {
        state = GetComponent<ObjectState>();
    }

    void Update()
    {
        if (!enable)
            return;

        if (state != null &&
            ((!isActivated && state._onConduction) ||
            (isActivated && !state._onConduction)))
        {
            isActivated = !isActivated;
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
