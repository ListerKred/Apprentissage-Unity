using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] playlist;
    public AudioSource audioSource;
    private int musicIndex = 0;

    public AudioMixerGroup soundEffectMixer;

    public static AudioManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de AudioManager dans la scène");
            return;
        }
        instance = this;
    }
    void Start()
    {
        // Envoyer la 1er musique de la liste
        audioSource.clip = playlist[0];
        // Jouer la musique qu'on a chargé 
        audioSource.Play();
    }

    void Update()
    {
        // Permet de savoir si une musique est joué
        if (!audioSource.isPlaying)
        {
            PlayNextSong();
        }
    }

    void PlayNextSong()
    {
        // Passer a la musique suivente
        musicIndex = (musicIndex + 1) % playlist.Length;
        // Savoir si on doit passé a la suivente ou recommencer depuis le debut
        audioSource.clip = playlist[musicIndex];
        // Jouer la musique
        audioSource.Play();
    }

    public AudioSource PlayClipAt(AudioClip clip, Vector3 pos)
    {
        // Création d'une empty TempAudio
        GameObject tempGo = new GameObject("TempAudio");
        // Changer ca possition
        tempGo.transform.position = pos;
        // Stocker l'audio source
        AudioSource audioSource = tempGo.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.outputAudioMixerGroup = soundEffectMixer;
        audioSource.Play();
        Destroy(tempGo, clip.length);
        return audioSource;
    }
}
