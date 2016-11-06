using UnityEngine;
using System.Collections;

public class FirePlatform : MonoBehaviour {

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
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

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Projectile")
        {
            ShotScript projectile = coll.gameObject.GetComponent<ShotScript>();
            if (projectile.GetElement() == ShootScript.Element.WATER)
            {
                Destroy(gameObject);
            }
        }
    }
}
