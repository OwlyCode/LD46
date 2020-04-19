using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSoundModulation : MonoBehaviour
{
    bool fadeIn = true;

    const float fadeRatio = 2.5f;

    void OnFadeOutMusic()
    {
        fadeIn = false;
    }

    void OnFadeInMusic()
    {
        fadeIn = true;
    }

    void Update()
    {
        float currentVolume = GetComponent<AudioSource>().volume;
        float target = fadeIn ? 1f : 0f;

        GetComponent<AudioSource>().volume = Mathf.Lerp(currentVolume, target, Time.deltaTime * fadeRatio);
    }
}
