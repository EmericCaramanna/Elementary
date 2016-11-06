using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SoundSliderFeedback : MonoBehaviour
{
    float previousValue;
    Slider sound;
    AudioSource source;

    // Use this for initialization
    void Start()
    {
        sound = gameObject.GetComponent<Slider>();
        source = gameObject.GetComponent<AudioSource>();
        if (sound)
        {
            previousValue = sound.value;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (source && sound && previousValue != sound.value)
        {
            previousValue = sound.value;
            source.volume = previousValue;
            source.Play();
        }
    }
}
