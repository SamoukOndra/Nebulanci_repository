using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCharacterPlaceholder : MonoBehaviour
{
    [HideInInspector]
    public GameObject character;
    public int characterIndex;

    [SerializeField] Color blockedColor;
    [SerializeField] Color pointedColor;
    [SerializeField] Color selectedColor;

    private Light spotlight;
    private AudioSource audioSource;
    private AudioClip spotlightSound;

    Animator animator;

    int _isPointed;
    int _isSelected;

    bool isPointed;
    bool isSelected;
    bool isBlocked;

    bool audioBlocked;
    float audioBlockDuration = 0.2f;

    private void Awake()
    {
        spotlight = GetComponentInChildren<Light>();
        audioSource = GetComponent<AudioSource>();
        spotlightSound = AudioManager.audioList.spotlight;
    }

    public void SetAnimatorController(RuntimeAnimatorController animatorController)
    {
        animator = character.GetComponent<Animator>();
        animator.runtimeAnimatorController = animatorController;

        _isPointed = Animator.StringToHash("IsPointed");
        _isSelected = Animator.StringToHash("IsSelected");
    }

    public void SetIsPointed(bool isPointed)
    {
        if(this.isPointed != isPointed)
        {
            animator.SetBool(_isPointed, isPointed);
            this.isPointed = isPointed;

            SetSpotlight(true);
            PlaySpotlightAudio(isPointed);
        }

            
    }

    public void SetIsSelected(bool isSelected)
    {
        if(this.isSelected != isSelected)
        {
            this.isSelected = isSelected;
            animator.SetBool(_isSelected, isSelected);

            SetSpotlight(true);
            PlaySpotlightAudio(isSelected);
        }
        
    }


    public void SetIsSelected(bool isSelected, out MenuCharacterPlaceholder selectedCharacterScript)
    {
        if (isBlocked)
        {
            selectedCharacterScript = null;
            return;
        }

        this.isSelected = isSelected;
        animator.SetBool(_isSelected, isSelected);
 
        if (isSelected)
        {
            selectedCharacterScript = this;
            SetSpotlight(true);
            PlaySpotlightAudio(true);
        }
            
        else selectedCharacterScript = null;
    }

    public bool GetIsSelected()
    {
        return isSelected;
    }

    public void Block(bool isBlocked)
    {
        this.isBlocked = isBlocked;
        //spotlight.enabled = isBlocked;
        SetSpotlight(true);
    }

    public bool GetIsBlocked()
    {
        return isBlocked;
    }

    private void SetSpotlight(bool enabled)
    {
        spotlight.enabled = enabled;

        if (!enabled) return;

        if (isBlocked)
        {
            spotlight.color = blockedColor;
        }
        else if (isSelected)
        {
            spotlight.color = selectedColor;
        }
        else if (isPointed)
        {
            spotlight.color = pointedColor;
        }
        else
        {
            spotlight.enabled = false;
            return;
        }  
    }

    private void PlaySpotlightAudio(bool play)
    {
        if (!play || audioBlocked) return;
        audioSource.PlayOneShot(spotlightSound);
        StartCoroutine(BlockAudioCoroutine());
    }

    IEnumerator BlockAudioCoroutine()
    {
        audioBlocked = true;
        yield return new WaitForSeconds(audioBlockDuration);
        audioBlocked = false;
    }
}
