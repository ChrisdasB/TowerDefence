using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] public AudioClip coinPickUpSound;

    AudioSource audioSource;

    // Start is called before the first frame update
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource != null)
            print("Got the source");
    }

    public void PlaySound()
    {
        audioSource.PlayOneShot(coinPickUpSound);
        print("Sound played");
    }
}
