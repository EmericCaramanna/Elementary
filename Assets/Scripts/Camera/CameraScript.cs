using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour
{
    [SerializeField]
    private float dampTime = 0.15f;
    [SerializeField]
    private GameObject min;
    [SerializeField]
    private GameObject max;

    private Vector3 velocity = Vector3.zero;
    private Transform target;
    private Vector3 pt = new Vector3(0.5f, 0.5f, 0);
    private bool firstFrame = true;

    void Start()
    {
        if (GameObject.FindGameObjectWithTag("Player"))
            target = GameObject.FindGameObjectWithTag("Player").transform;
        GameObject cp = GameObject.FindGameObjectWithTag("CheckpointSaver");
        if (cp)
        {

            Vector3 position = cp.GetComponent<SaveCheckpoint>().savecCheckpoint.transform.position;
            position.x = (position.x < min.transform.position.x) ? min.transform.position.x : position.x;
            position.x = (position.x > max.transform.position.x) ? max.transform.position.x : position.x;

            position.y = (position.y < min.transform.position.y) ? min.transform.position.y : position.y;
            position.y = (position.y > max.transform.position.y) ? max.transform.position.y : position.y;

            position.z = -10;
            transform.position = position;
        }
    }
    void Update()
    {
        if (target)
        {
            Vector3 point = Camera.main.WorldToViewportPoint(target.position);
            pt.z = point.z;
            Vector3 delta = target.position - Camera.main.ViewportToWorldPoint(pt);
            Vector3 destination = transform.position + delta;

            if (min != null && max != null)
            {
                destination.x = Mathf.Clamp(destination.x, min.transform.position.x, max.transform.position.x);
                destination.y = Mathf.Clamp(destination.y, min.transform.position.y, max.transform.position.y);
            }
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
        }
        else if (GameObject.FindGameObjectWithTag("Player"))
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }

    }
}
/*		// The target x and y coordinates should not be larger than the maximum or smaller than the minimum.
		targetX = Mathf.Clamp(targetX, minXAndY.x, maxXAndY.x);
		targetY = Mathf.Clamp(targetY, minXAndY.y, maxXAndY.y);
 */