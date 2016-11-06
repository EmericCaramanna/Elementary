using UnityEngine;
using System.Collections;

public class ObjectToFall : MonoBehaviour
{
    Rigidbody2D rigid;
    BoxCollider2D collid;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        collid = GetComponent<BoxCollider2D>();
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (rigid == null || collid == null)
            return;

        if (rigid.velocity.y < 0 && coll.gameObject.tag == "Player")
        {
            if (transform.position.y - (collid.size.y / 2) * transform.localScale.y > coll.transform.position.y)
            {
                GameObject saver = GameObject.FindGameObjectWithTag("CheckpointSaver");
                if (saver != null)
                    saver.GetComponent<SaveCheckpoint>().savecCheckpoint.GetComponent<Checkpoint>().Respawn(gameObject);
            }
        }
    }
}
