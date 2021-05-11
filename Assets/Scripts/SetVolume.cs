// https://gamedevbeginner.com/the-right-way-to-make-a-volume-slider-in-unity-using-logarithmic-conversion/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
    public AudioMixer mixer;
    public string str;

    public void SetLevel (float sliderValue)
    {
        mixer.SetFloat(str, Mathf.Log10(sliderValue) * 20);
    }
}