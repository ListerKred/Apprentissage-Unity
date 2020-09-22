using UnityEngine;

public class WeakSpot : MonoBehaviour
{
    public GameObject objectToDestroy;
    public AudioClip killSound;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            AudioManager.instance.PlayClipAt(killSound, transform.position);
            Destroy(objectToDestroy);
        }
    }
}
