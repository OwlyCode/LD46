using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepSounds : MonoBehaviour
{
    public AudioClip damaged;
    public AudioClip upgraded;
    public AudioClip dead;

    public AudioClip jump;

    public void PlayDamaged()
    {
        GetComponent<AudioSource>().PlayOneShot(damaged);
    }

    public void PlayUpgraded()
    {
        GetComponent<AudioSource>().PlayOneShot(upgraded);
    }

    public void PlayDead()
    {
        GetComponent<AudioSource>().PlayOneShot(dead);
    }

    public void PlayJump()
    {
        GetComponent<AudioSource>().PlayOneShot(jump);
    }
}
