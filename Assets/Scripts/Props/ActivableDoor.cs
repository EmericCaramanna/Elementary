using UnityEngine;
using System.Collections;
using System;

public class ActivableDoor : MonoBehaviour, IActivateObject
{
    [SerializeField]
    SpriteRenderer Background;

    SpriteRenderer spriteRenderer;
    [SerializeField] Sprite activatedSprite;
    [SerializeField] Sprite desactivatedSprite;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.red;
    }

    public void Activate()
    {
        GameObject o = GameObject.FindGameObjectWithTag("Player");
        if (o != null)
        {
            GetComponent<Collider2D>().enabled = false;
        }
        GetComponent<SpriteRenderer>().sprite = desactivatedSprite;

        //spriteRenderer.color = Color.white;
    }

    public void Desactivate()
    {
        GameObject o = GameObject.FindGameObjectWithTag("Player");
        if (o != null)
        {
            GetComponent<Collider2D>().enabled = true;
        }
        GetComponent<SpriteRenderer>().sprite = activatedSprite;
        //spriteRenderer.color = Color.red;
    }
}
