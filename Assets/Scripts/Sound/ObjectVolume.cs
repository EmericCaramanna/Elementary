using UnityEngine;
using System.Collections;

public class ObjectVolume : MonoBehaviour
{
    AudioSource source;
    VolumeSettings setting;

    // Use this for initialization
    void Start()
    {
        GameObject volume;

        source = gameObject.GetComponent<AudioSource>();
        volume = GameObject.FindGameObjectWithTag("VolumeSettings");
        if (volume)
        {
            setting = volume.GetComponent<VolumeSettings>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (source && setting)
        {
            if (source.gameObject.tag == "Music" || source.gameObject.tag == "MainCamera")
            {
                if (source.volume != setting.musicValue)
                    source.volume = setting.musicValue;
            }
            else
            {
                if (source.volume != setting.soundsValue)
                    source.volume = setting.soundsValue;
            }
        }
    }
}
