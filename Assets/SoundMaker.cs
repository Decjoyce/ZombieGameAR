using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMaker : MonoBehaviour
{
    AudioSource source;
    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    private void Update()
    {

    }

    public void PlaySound()
    {
        //source.PlayOneShot(clip);
        SoundDisplayer.instance.TranslateSound(transform.position);
    }
}
