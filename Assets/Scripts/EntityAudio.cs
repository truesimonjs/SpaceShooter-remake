using UnityEngine;
using UnityEngine.U2D;

public class EntityAudio : MonoBehaviour
{
    [SerializeField] private Entity entity;
    private AudioSource audioSource;
    [SerializeField] AudioClip hurtSound;
    private void Start()
    {
        if (entity == null) entity = gameObject.GetComponent<Entity>();
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = hurtSound;
        entity.OnTakeDamage += Entity_OnTakeDamage;

    }
    private void Entity_OnTakeDamage(object sender, System.EventArgs e)
    {
        audioSource.Play();
    }
}
