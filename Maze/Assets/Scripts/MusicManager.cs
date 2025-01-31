using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;
    public AudioSource audioSource;
    public AudioClip timerBoom, boom;
    public AudioClip audioMenu, buttonSound, audioPlay;
    
    void Start()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void Change(AudioClip audio)
    {
        audioSource.clip = audio;
        audioSource.Play();
    }
    public void AddSound(AudioClip audio)
    {
        audioSource.PlayOneShot(audio);
    }

}
