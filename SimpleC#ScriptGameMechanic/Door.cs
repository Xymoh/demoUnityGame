using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    Animator anim;
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip doorClip;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        anim.SetTrigger("DoorOpen");
    }

    private void OnTriggerExit(Collider other)
    {
        anim.enabled = true;
    }

    void pauseAnimation()
    {
        anim.enabled = false;
    }

    public void PlayDoorSound()
    {
        audioSource.clip = doorClip;
        audioSource.Play();
    }
}
