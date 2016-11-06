using UnityEngine;
using System.Collections;

public class SwitchSprite : MonoBehaviour
{
    private SpriteRenderer spriteRender;
    Sprite firstSprite;
    [SerializeField]
    Sprite secondSprite;

    // Use this for initialization
    void Start()
    {
        spriteRender = GetComponent<SpriteRenderer>();
        if (spriteRender)
        {
            firstSprite = spriteRender.sprite;
        }
    }

    public void Switch()
    {
        if (spriteRender)
        {
            if (spriteRender.sprite == firstSprite)
                spriteRender.sprite = secondSprite;
            else if (spriteRender.sprite == secondSprite)
                spriteRender.sprite = firstSprite;
        }
    }
}
