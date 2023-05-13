using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class breathing : MonoBehaviour
{
    public AudioSource playerSoundSource;
    
    public AudioClipAndVolume[] runningBreathSounds;

    public AudioClipAndVolume runningOutOfBreathSound;

    private float breathLength;

    private float outOfBreathLength;

    public breathing()
    {
        breathLength = 0;
        outOfBreathLength = 0;
    }

    public void playBreathSound()
    {
        if(breathLength > 0)
        {
            breathLength -= Time.deltaTime;
        }
        else
        {
            int randomIndex = Random.Range(0, runningBreathSounds.Length);
            AudioClip randomBreath = runningBreathSounds[randomIndex].clip;
            float randomVolume = runningBreathSounds[randomIndex].volume;
            
            playerSoundSource.PlayOneShot(randomBreath, randomVolume);
            breathLength = randomBreath.length;
        }
    }

    public void playOutOfBreathSound()
    {
        if(outOfBreathLength > 0)
        {
            outOfBreathLength -= Time.deltaTime;
        }
        else
        {
            playerSoundSource.PlayOneShot(runningOutOfBreathSound.clip, runningOutOfBreathSound.volume);
            breathLength = outOfBreathLength = runningOutOfBreathSound.clip.length;
        }
    }
}
