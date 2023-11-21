using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyMagClick : MonoBehaviour
{
    private AudioSource audioSource;
    private AudioClip click;

    public bool attackPressed = false;

    public EmptyMagClick(AudioSource audioSource)
    {
        this.audioSource = audioSource;
        click = AudioManager.audioList.emptyMagClick;
    }

    public void HandleSfx()
    {
        if (!attackPressed) return;

        attackPressed = false;
        audioSource.PlayOneShot(click);
    }
}
