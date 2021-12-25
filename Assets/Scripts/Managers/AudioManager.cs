using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource mainMusic;
    public AudioSource[] audioSFX;
    
    public void StopAllAudio()
    {
        if(mainMusic.isPlaying)
            mainMusic.Pause();

        for(int i = 0; i < audioSFX.Length; i++)
            audioSFX[i].Stop();
    }

    public void PlayMainMusic()
    {
        if(mainMusic.isPlaying == false)
            mainMusic.Play();
    }

}
