using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_Script : MonoBehaviour
{
    public AudioClip[] SoundList;

    public AudioSource SAudio;
    
    void Start()
    {
        
    }
    
    void Update()
    {
        
    }
    
    public void SoundAudio(int index)
    {
        SAudio.PlayOneShot(SAudio.clip = SoundList[index]);
        SAudio.volume = 1.2f;
    }
}
