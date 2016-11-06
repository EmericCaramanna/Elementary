using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Checkpoint : MonoBehaviour {

    public GameObject player;
    public string playerTag;
    public bool activeCheckpoint;
    public bool fire, water, wind, electricity;
    public Sprite Lock, Unlock;

    public GameObject localPlayer;

    public void SpawnPlayer()
    {
        if (activeCheckpoint && !GameObject.FindGameObjectWithTag(playerTag))
        {
            localPlayer = (GameObject)Instantiate(player, this.transform.position, this.transform.rotation);
            localPlayer.GetComponentInChildren<ShootScript>().SetElements(fire, water, wind, electricity);
            localPlayer.GetComponentInChildren<ManageElementTab>().SetElements(fire, water, electricity, wind);
        }
    }

    public void Respawn(GameObject killer)
    {
        if (gameObject && localPlayer)
            localPlayer.GetComponent<PlayerHealth>().Die(gameObject, killer);
    }

    void Update()
    {
        if (localPlayer == null)
        {
            SpawnPlayer();
        }
    }

    void changeActiveCheckpoint()
    {

    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == playerTag)
        {
            GameObject[] checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint");
            for (int i = 0; i < checkpoints.Length; ++i)
            {
                if (checkpoints[i] != this.gameObject)
                {
                    checkpoints[i].GetComponent<Checkpoint>().activeCheckpoint = false;
                    checkpoints[i].GetComponent<Checkpoint>().localPlayer = null;
                }
            }
            activeCheckpoint = true;
            if (GetComponent<SpriteRenderer>())
            {
                GetComponent<SpriteRenderer>().sprite = Lock;
            }
            localPlayer = coll.gameObject;
            GetComponentInParent<SaveCheckpoint>().savecCheckpoint = gameObject;
        }
    }
}
