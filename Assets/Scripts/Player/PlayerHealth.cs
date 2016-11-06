using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

// This script manages the life and death of the character
public class PlayerHealth : MonoBehaviour
{
    private bool respawning = false;
    private GameObject _checkpoint = null;
    private Vector3 _dir;
    bool _isImmortal = false;
    GameObject mainCamera = null;
    GameObject checkpointSaver = null;
    GameObject _playerParticle;

    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        checkpointSaver = GameObject.FindGameObjectWithTag("CheckpointSaver");
        _playerParticle = transform.FindChild("PlayerParticle").gameObject;
    }


    // When the character dies all forces and collisions
    // are disabled, the camera shakes, and a sound is played
    public void Die(GameObject checkpoint, GameObject killer)
    {
        if (!_isImmortal)
        {
            _checkpoint = checkpoint;
            respawning = true;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            gameObject.GetComponent<PlayerControl>().enabled = false;
            gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
            gameObject.GetComponentInChildren<ShootScript>().SetElement(ShootScript.Element.NONE);
            gameObject.GetComponentInChildren<ShootScript>().enabled = false;
            gameObject.GetComponent<AudioSource>().Play();
            _playerParticle.GetComponent<ParticleSystem>().startSize = 0.1f;
            _playerParticle.transform.FindChild("SpiritTale1").GetComponent<ParticleSystem>().startSize = 0.1f;
            _playerParticle.transform.FindChild("SpiritTale2").GetComponent<ParticleSystem>().startSize = 0.1f;
            if (mainCamera.GetComponent<CameraShake>() != null)
                mainCamera.GetComponent<CameraShake>().shakeDuration = .20f;
        }
    }

    public bool IsImmortal()
    {
        return _isImmortal;
    }

    public void SetImmortal(bool isImmortal)
    {
        _isImmortal = isImmortal;
    }

    // When respawning the character moves to the last registered checkpoint
    void FixedUpdate()
    {
        if (respawning == true && _checkpoint != null)
        {
            if (!CheckPositionInRange(gameObject.transform.position, _checkpoint.transform.position, 0.5f))
            {
                _dir = _checkpoint.transform.position - gameObject.transform.position;
                _dir.Normalize();
                gameObject.transform.Translate(_dir * 0.2f);
            }
            else
            {
                if (checkpointSaver)
                {
                    checkpointSaver.GetComponent<SaveCheckpoint>().Respawn();
                }
                else
                {
                    checkpointSaver = GameObject.FindGameObjectWithTag("CheckpointSaver");
                }
                //respawning = false;
                //gameObject.GetComponent<CircleCollider2D>().enabled = true;
                //gameObject.GetComponent<PlayerControl>().enabled = true;
                //gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
                //gameObject.GetComponentInChildren<ShootScript>().enabled = true;
                //gameObject.GetComponent<ParticleSystem>().startSize = 1f;
            }
        }
    }

    private bool CheckPositionInRange(Vector3 v1, Vector3 v2, float range)
    {
        if (v1.x < v2.x + range && v1.x > v2.x - range &&
            v1.y < v2.y + range && v1.y > v2.y - range)
            return true;
        return false;
    }
}
