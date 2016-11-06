using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ElecState : MonoBehaviour
{
    public GameObject BoltPrefab;
    
    List<GameObject> activeBoltsObj;
    List<GameObject> inactiveBoltsObj;
    public bool isParent;
    public int maxBolts = 17;

    Vector2 pos1, pos2;

    float _timer;
    
    void Start ()
    {
        activeBoltsObj = new List<GameObject>();
        inactiveBoltsObj = new List<GameObject>();

        _timer = 0.05f;

        pos1 = transform.position;
        //For however many bolts we've specified
        for (int i = 0; i < maxBolts; i++)
        {
            GameObject bolt = (GameObject)Instantiate(BoltPrefab, transform.position, Quaternion.identity);
            if (isParent)
                bolt.transform.parent = transform.parent.parent.transform;
            else
                bolt.transform.parent = transform;

            //Initialize our lightning with a preset number of max sexments
            bolt.GetComponent<LightningBolt>().Initialize(8);
            bolt.SetActive(false);
            inactiveBoltsObj.Add(bolt);
        }
    }
	
    void    OnDisable()
    {
        GameObject boltObj;
        LightningBolt boltComponent;

        if (activeBoltsObj != null)
        {
            int activeLineCount = activeBoltsObj.Count;


            for (int i = activeLineCount - 1; i >= 0; i--)
            {
                //pull GameObject
                boltObj = activeBoltsObj[i];

                //get the LightningBolt component
                boltComponent = boltObj.GetComponent<LightningBolt>();

                //if the bolt has faded out

                //deactive the segments it contains
                boltComponent.DeactivateSegments();

                //set it inactive
                boltObj.SetActive(false);

                //move it to the inactive list
                activeBoltsObj.RemoveAt(i);
                inactiveBoltsObj.Add(boltObj);
            }
        }
    }

	void Update () {
        GameObject boltObj;
        LightningBolt boltComponent;

        int activeLineCount = activeBoltsObj.Count;

        //loop through active lines (backwards because we'll be removing from the list)
        for (int i = activeLineCount - 1; i >= 0; i--)
        {
            //pull GameObject
            boltObj = activeBoltsObj[i];

            //get the LightningBolt component
            boltComponent = boltObj.GetComponent<LightningBolt>();

            //if the bolt has faded out
            if (boltComponent.IsComplete)
            {
                //deactive the segments it contains
                boltComponent.DeactivateSegments();

                //set it inactive
                boltObj.SetActive(false);

                //move it to the inactive list
                activeBoltsObj.RemoveAt(i);
                inactiveBoltsObj.Add(boltObj);
            }
        }
        if (isParent)
            pos2 = transform.parent.position;
        else
            pos2 = transform.position;
        _timer -= Time.deltaTime;
        if (_timer <= 0)
        {
            pos1 = pos2;
            _timer = 0.1f;
        }
        CreatePooledBolt(pos1, pos2, Color.yellow, 0.05f);

        //update and draw active bolts
        for (int i = 0; i < activeBoltsObj.Count; i++)
        {
            activeBoltsObj[i].GetComponent<LightningBolt>().UpdateBolt();
            activeBoltsObj[i].GetComponent<LightningBolt>().Draw();
        }
    }

    //calculate distance squared (no square root = performance boost)
    public float DistanceSquared(Vector2 a, Vector2 b)
    {
        return ((a.x - b.x) * (a.x - b.x) + (a.y - b.y) * (a.y - b.y));
    }

    void CreatePooledBolt(Vector2 source, Vector2 dest, Color color, float thickness)
    {
        //if there is an inactive bolt to pull from the pool
        if (inactiveBoltsObj.Count > 0)
        {
            //pull the GameObject
            GameObject boltObj = inactiveBoltsObj[inactiveBoltsObj.Count - 1];

            //set it active
            boltObj.SetActive(true);

            //move it to the active list
            activeBoltsObj.Add(boltObj);
            inactiveBoltsObj.RemoveAt(inactiveBoltsObj.Count - 1);

            //get the bolt component
            LightningBolt boltComponent = boltObj.GetComponent<LightningBolt>();

            //activate the bolt using the given position data
            boltComponent.ActivateBolt(source, dest, color, thickness);
        }
    }
}
