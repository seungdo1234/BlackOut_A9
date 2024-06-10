using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurpriseZoneEventHandler : MonoBehaviour
{
    public Action surpriseEvent;
    public AudioClip surpriseAudioClip;
    public GameObject surpriseUi;
    
    private AudioSource audioSource;
    private Animator animator;
    private bool onlyOneTime = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        animator = surpriseUi.GetComponentInChildren<Animator>();
        surpriseEvent += PlaySound;
        surpriseEvent += MonsterAnimation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!onlyOneTime && other.CompareTag("Player"))
        {
            surpriseEvent.Invoke();
            onlyOneTime = true;
        }
    }

    private void PlaySound()
    {
        audioSource.PlayOneShot(surpriseAudioClip);
    }

    private void MonsterAnimation()
    {
        animator.SetBool("IsPassed", true);
    }
}
