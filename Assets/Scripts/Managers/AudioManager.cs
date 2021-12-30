using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource mainMusic;
    public AudioClip bossFightMusic;
    public AudioSource[] audioSFX;

    private void OnEnable()
    {
        EventBroker.OnBossDead += StopAllAudio;
    }
    private void OnDisable()
    {
        EventBroker.OnBossDead -= StopAllAudio;
    }
    public void StopAllAudio()
    {
        if (mainMusic.isPlaying)
            mainMusic.Pause();

        for (int i = 0; i < audioSFX.Length; i++)
            audioSFX[i].Stop();
    }

    public void PlayMainMusic()
    {
        if (mainMusic.isPlaying == false)
            mainMusic.Play();
    }

    public void PlayBossFightMusic()
    {
        mainMusic.clip = bossFightMusic;
        mainMusic.Play();
    }
}

