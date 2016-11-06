using UnityEngine;
using System.Collections;
using System;

public class PlayerKiller : MonoBehaviour
{
    public static void BecomePK(GameObject obj)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Collider2D colliderPlayer;
        if (player != null && (colliderPlayer = player.GetComponent<BoxCollider2D>()) != null && obj.GetComponent<BoxCollider2D>() != null)
        {
            Physics2D.IgnoreCollision(obj.GetComponent<Collider2D>(), colliderPlayer);
        }
    }

    public static void LosePK(GameObject obj)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Collider2D colliderPlayer;
        if (player != null && (colliderPlayer = player.GetComponent<BoxCollider2D>()) != null && obj.GetComponent<BoxCollider2D>() != null)
        {
            Physics2D.IgnoreCollision(obj.GetComponent<Collider2D>(), colliderPlayer, false);
        }
    }

    void OnCollisionEnter2D (Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            GameObject saver = GameObject.FindGameObjectWithTag("CheckpointSaver");
            if (saver != null)
                saver.GetComponent<SaveCheckpoint>().savecCheckpoint.GetComponent<Checkpoint>().Respawn(gameObject);
        }
    }
}
