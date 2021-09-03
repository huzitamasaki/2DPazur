using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    public AudioSource bgm;
    public AudioSource hue;

    private void Start()
    {
        AudioSource[] audioSources = GetComponents<AudioSource>();
        bgm = audioSources[0];
        hue = audioSources[1];
    }

    public void Update()
    {
        if (Bird.time==0)
        {
            bgm.Stop();
            hue.PlayOneShot(hue.clip);
        }
    }

}
