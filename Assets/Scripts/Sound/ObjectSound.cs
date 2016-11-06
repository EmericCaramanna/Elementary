using UnityEngine;
using System.Collections;

public class ObjectSound : MonoBehaviour
{
    [SerializeField]
    AudioClip Burning;
    [SerializeField]
    AudioClip Extinguish;

    AudioSource source;

    void Start()
    {
        source = gameObject.GetComponent<AudioSource>();
    }

    public void playBurning()
    {
        if (source)
        {
            if (Burning && source.clip == Burning && source.isPlaying)
                return;
            source.Stop();
            if (Burning)
            {
                source.clip = Burning;
                source.loop = true;
                source.Play();
            }
        }
    }

    public void playExtinguish()
    {
        if (source)
        {
            source.Stop();
            if (Extinguish)
            {
                source.clip = Extinguish;
                source.loop = false;
                source.Play();
            }
        }
    }

    void Update()
    {
        if (source && source.isPlaying && gameObject.transform.childCount == 0)
        {
            source.Stop();
        }
    }

}
