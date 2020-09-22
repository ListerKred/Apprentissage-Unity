using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] playlist;
    public AudioSource audioSource;
    private int musicIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Envoyer la 1er musique de la liste
        audioSource.clip = playlist[0];
        // Jouer la musique qu'on a chargé 
        audioSource.Play();
    }

    // Update is called once per frame
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
}
