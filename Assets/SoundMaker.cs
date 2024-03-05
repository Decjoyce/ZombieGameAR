using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMaker : MonoBehaviour
{
    AudioSource source;
    [SerializeField] float interval = 5f;
    [SerializeField] bool playSounds;
    [SerializeField] AudioClip[] clips;
    private void Start()
    {
        source = GetComponent<AudioSource>();
        InvokeRepeating("PlaySound", 0f, interval);
    }

    private void Update()
    {

    }

    public void PlaySound()
    {
        if(playSounds)
            source.PlayOneShot(clips[Random.Range(0, clips.Length)]);
        SoundDisplayer.instance.TranslateSound(transform.position);
    }
}
