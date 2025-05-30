using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public AudioClip shootClip;
    public AudioClip deathClip;
    public AudioClip bgm;

    private AudioSource audioSource;

    void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();

        if (bgm != null)
        {
            audioSource.clip = bgm;
            audioSource.loop = true;
            audioSource.volume = 0.4f;
            audioSource.Play();
        }
    }

    public void PlayShoot()
    {
        audioSource.PlayOneShot(shootClip);
    }

    public void PlayDeath()
    {
        audioSource.PlayOneShot(deathClip);
    }
}
