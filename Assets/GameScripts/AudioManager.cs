using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [Header ("Audio Source")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("Audio Clip")]
    public AudioClip mainMenuBackground;
    public AudioClip LevelBackground;
    public AudioClip UIClick;
    public AudioClip bugExplosion;
    public AudioClip levelComplete;
    public AudioClip levelFail;
    public AudioClip placingTower;
    public AudioClip towerShoot;
    public AudioClip waveStart;

    public void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if (sceneName == "Main Menu")
        {
            musicSource.clip = mainMenuBackground;
        } else if (sceneName == "Level 1" || sceneName == "Level 2")
        {
            musicSource.clip = LevelBackground;
        }
        
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    public void StopMusic()
    {
        if (musicSource.isPlaying)
        {
            musicSource.Stop();
        }
    }
}
