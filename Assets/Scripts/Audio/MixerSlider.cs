using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

[RequireComponent(typeof(Slider))]
public class MixerSlider : MonoBehaviour
{
    public AudioMixer mixer;
    public string mixerParamater;

    public float maxAttenuation = 0.0f;
    public float minAttenuation = -60f;

    private Slider slider;

    private float mixerValue;
    private void Awake()
    {
        slider = GetComponent<Slider>();

        if (PlayerPrefs.HasKey(mixerParamater) == true)
            mixerValue = PlayerPrefs.GetFloat(mixerParamater);  
        else
            mixer.GetFloat(mixerParamater, out mixerValue);

        slider.value = (mixerValue - minAttenuation) / (maxAttenuation - minAttenuation);

        slider.onValueChanged.AddListener(SliderValueChange);
    }


    void SliderValueChange(float value)
    {
        mixerValue = minAttenuation + value * (maxAttenuation - minAttenuation);
        mixer.SetFloat(mixerParamater, mixerValue);

        PlayerPrefs.SetFloat(mixerParamater, mixerValue);
    }
}
