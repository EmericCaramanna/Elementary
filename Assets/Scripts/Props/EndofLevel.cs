using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class EndofLevel : MonoBehaviour
{
    public string _scName;
    private bool _hasTrigger;
    private GameObject _player;
    private Vector2 _dir;
    bool first = true;
    private float _timeToStayIn = 1f;

	// Use this for initialization
	void Start ()
    {
        _hasTrigger = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if (_hasTrigger)
        {
            _timeToStayIn -= Time.deltaTime;
            if (_timeToStayIn < 0f)
            {
                GoToNextScene();
            }

            _player.transform.Translate(_dir.normalized.x * Time.deltaTime, 0, 0);
            _player.GetComponent<Rigidbody2D>().gravityScale = -1f;
            //_player.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(_player.gameObject.GetComponent<Rigidbody2D>().velocity.x, 0);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (first)
            {
                GetComponent<AudioSource>().Play();
                first = false;
            }
            _player = other.gameObject;
            _dir = transform.position - _player.transform.position;
            _hasTrigger = true;
            if (_player.GetComponent<PlayerControl>())
                _player.GetComponent<PlayerControl>().enabled = false;
        }
    }
    
    void OnTriggerExit2D(Collider2D other)
    {
        if (_hasTrigger)
        {
            GoToNextScene();
        }
    }

    public void GoToNextScene()
    {
        Destroy(GameObject.Find("CheckpointSaver"));
        if (_scName.Length != 0)
            SceneManager.LoadScene(_scName);
        else
        {
            int sceneToLoad = (SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings;
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
