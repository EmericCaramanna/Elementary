using UnityEngine;
using System.Collections;

public class ElementToPickup : MonoBehaviour {
    public ShootScript.Element element;

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            coll.gameObject.GetComponentInChildren<ShootScript>().AddElement(element);
            Destroy(gameObject);
        }
    }
}
