

using UnityEngine;
using System.Collections;

public class FallingPlatform : MonoBehaviour
{
    [SerializeField]
    int layerToChange;

    [SerializeField]
    float timeToFall = 2f;
    float timer = 0f;
    bool willFall = false;

    Rigidbody2D rigid;

	void Start ()
    {
        rigid = GetComponent<Rigidbody2D>();
	}

    void Update()
    {
        if (willFall)
        {
            timer += Time.deltaTime;
            if (timer >= timeToFall)
            {
                rigid.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
                rigid.gravityScale = 1f;
                gameObject.layer = layerToChange;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            willFall = true;
        }
    }
}