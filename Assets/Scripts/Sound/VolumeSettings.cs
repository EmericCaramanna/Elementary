using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class VolumeSettings : MonoBehaviour
{
    private static VolumeSettings instance = null;
    public static VolumeSettings Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public float musicValue;
    public float soundsValue;
    [SerializeField]
    Slider musicSlider = null;
    [SerializeField]
    Slider soundsSlider = null;

    void Start()
    {
        if (GameObject.FindGameObjectWithTag("MusicSlider"))
            musicSlider = GameObject.FindGameObjectWithTag("MusicSlider").GetComponent<Slider>();
        if (GameObject.FindGameObjectWithTag("SoundSlider"))
            soundsSlider = GameObject.FindGameObjectWithTag("SoundSlider").GetComponent<Slider>();
    }

    void Update()
    {
        if (!musicSlider)
            if (GameObject.FindGameObjectWithTag("MusicSlider"))
                musicSlider = GameObject.FindGameObjectWithTag("MusicSlider").GetComponent<Slider>();
        if (!soundsSlider)
            if (GameObject.FindGameObjectWithTag("SoundSlider"))
                soundsSlider = GameObject.FindGameObjectWithTag("SoundSlider").GetComponent<Slider>();

        if (musicSlider && musicSlider.value != musicValue)
            musicValue = musicSlider.value;
        if (soundsSlider && soundsSlider.value != soundsValue)
            soundsValue = soundsSlider.value;
    }
}
