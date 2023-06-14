using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Volume : MonoBehaviour
{
    public AudioSource soundtrack;
    public Slider volumeSlider;

    AudioSource audioSource;


    void Awake()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("Volume");
        soundtrack.volume = volumeSlider.value;
    }

    // Use this for initialization
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    void OnEnable()
    {
        //Register Slider Events
        volumeSlider.onValueChanged.AddListener(delegate { changeVolume(volumeSlider.value); });
    }

    //Called when Slider is moved
    void changeVolume(float sliderValue)
    {
        PlayerPrefs.SetFloat("Volume", volumeSlider.value);
        audioSource.volume = sliderValue;
    }

    void OnDisable()
    {
        //Un-Register Slider Events
        volumeSlider.onValueChanged.RemoveAllListeners();
    }
}