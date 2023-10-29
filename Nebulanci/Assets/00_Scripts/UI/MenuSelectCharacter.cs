using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSelectCharacter : MonoBehaviour
{
    [SerializeField] GameObject characterPlaceholder;
    [SerializeField] LayerMask placeholderMask;

    [SerializeField] RuntimeAnimatorController characterController;

    [SerializeField] List<Transform> characterPositions;

    [SerializeField] List<GameObject> availableCharacters;

    private MenuCharacterPlaceholder lastPlaceholderScript;



    private void Awake()
    {
        for (int i = 0; i < availableCharacters.Count; i++)
        {
            GameObject placeholder = Instantiate(characterPlaceholder, characterPositions[i].position, characterPositions[i].rotation);
            MenuCharacterPlaceholder placeholderScript = placeholder.GetComponent<MenuCharacterPlaceholder>();

            placeholderScript.character = Instantiate(availableCharacters[i], placeholder.transform, false);

            placeholderScript.SetAnimatorController(characterController);

            lastPlaceholderScript = placeholderScript;
        }

        
    }

    private void FixedUpdate()
    {
        if (Util.MouseHit(50, placeholderMask, out Collider placeholerCollider))
        {
            MenuCharacterPlaceholder placeholderScript = placeholerCollider.gameObject.GetComponent<MenuCharacterPlaceholder>();
            if (placeholderScript != lastPlaceholderScript)
                lastPlaceholderScript = placeholderScript;

            lastPlaceholderScript.SetIsPointed(true);
        }
        else lastPlaceholderScript.SetIsPointed(false);
    }
}
