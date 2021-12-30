using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class RandomAudioPlayer : MonoBehaviour
{
    public AudioClip[] clips;
    public bool randomizePitch = false;
    public float pitchRange = 0.2f;

    private AudioSource audioSource;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayRandomSound()
    {
        int choice = Random.Range(0, clips.Length);

        if (randomizePitch)
            audioSource.pitch = Random.Range(1.0f - pitchRange, 1.0f + pitchRange);

        audioSource.PlayOneShot(clips[choice]);
    }

    public void Stop() => audioSource.Stop();
}
