using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{
    /******* + MOVEMENT + *******/
    [SerializeField]
    float maxSpeed = 5f;

    CircleCollider2D coll;
    float offsetMovement = 0.2f;
    Vector3 move;
    /******* - MOVEMENT - *******/

    /********* + JUMP + *********/
    [SerializeField]
    float jumpForce = 100f;

    [HideInInspector]
    public bool grounded = false;
    float jumpOneMoreTime = 0.2f;
    float jumpTime;

    Vector2 jumpVector;

    [SerializeField]
    LayerMask m_WhatIsGround;
    /********* - JUMP - *********/

    Vector3 vectorDown = new Vector3(0, -0.2f, 0);

    void Start()
    {
        jumpTime = jumpOneMoreTime;

        jumpVector = new Vector2(0f, jumpForce);

        coll = GetComponent<CircleCollider2D>();
        move = new Vector3(0, 0, 0);

        GameObject[] killers = GameObject.FindGameObjectsWithTag("PlayerKiller");
        foreach (GameObject killer in killers)
            PlayerKiller.BecomePK(killer);

        GameObject[] stateCubes = GameObject.FindGameObjectsWithTag("ObjectStateCube");
        foreach (GameObject cube in stateCubes)
        {
            ObjectState state = cube.GetComponent<ObjectState>();
            if (state != null)
            {
                if (state.isState((int)ObjectState.State.BURNING | (int)ObjectState.State.ELECTRIFIED))
                {
                    PlayerKiller.BecomePK(cube);
                }
            }
        }
    }

    void Update()
    {
        grounded = GetComponent<BoxCollider2D>().IsTouchingLayers(m_WhatIsGround);
        jumpTime += Time.deltaTime;
        if (Input.GetButton("Jump") && grounded && jumpTime > jumpOneMoreTime)
        {
            Vector2 velocity = GetComponent<Rigidbody2D>().velocity;
            velocity.y = 0f;
            GetComponent<Rigidbody2D>().velocity = velocity;
            GetComponent<Rigidbody2D>().AddForce(jumpVector);
            jumpTime = 0f;
        }
        float h = Input.GetAxisRaw("Horizontal");
        if (h != 0f)
        {
            if (!Physics2D.Raycast(transform.position - vectorDown, h * Vector2.right, coll.radius + offsetMovement, m_WhatIsGround))
            {
                move.x = h * maxSpeed * Time.deltaTime;
                gameObject.transform.Translate(move);
            }
        }
    }
}
