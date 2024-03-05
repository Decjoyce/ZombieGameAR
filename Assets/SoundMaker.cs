using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMaker : MonoBehaviour
{
    AudioSource source;
    [SerializeField] float interval = 5f;
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
        //source.PlayOneShot(clip);
        SoundDisplayer.instance.TranslateSound(transform.position);
    }
}
