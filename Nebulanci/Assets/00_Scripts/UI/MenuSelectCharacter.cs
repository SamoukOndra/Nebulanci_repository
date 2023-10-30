using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Runtime;

public class MenuSelectCharacter : MonoBehaviour
{
    private int currentPlayer = 0;

    [Header("Top Submenu")]
    [SerializeField] TMP_InputField inputFieldName;
    [SerializeField] TMP_Dropdown dropdownControls;
    [SerializeField] TextMeshProUGUI controlsDescriptionText;

    private int selectedControlsIndex;


    [Header("Characters")]
    [SerializeField] GameObject characterPlaceholder;
    [SerializeField] LayerMask placeholderMask;

    [SerializeField] RuntimeAnimatorController characterController;

    [SerializeField] List<Transform> characterPositions;

    [SerializeField] List<GameObject> availableCharacters;


    //Top Submenu




    //Characters
    private List<MenuCharacterPlaceholder> placeholderScripts = new();
    private MenuCharacterPlaceholder lastPlaceholderScript;

    private bool isPointing = false;
    private bool isSelected = false;

    //Controls Description
    string[] controlsDescriptions = {"move: .... ESDF \n \n fire: .... A \n \n change weapon ..... Q", "move: .... 8456 on numpad \n \n fire: .... 0 on numpad \n \n change weapon ..... 1 on numpad", "move: .... IJKL \n \n fire: .... M \n \n change weapon ..... N"};

    //Blueprints
    private readonly string[] controlSchemes = { "Player_1", "Player_2", "Player_3" };

    //private string blueprintName;
    //private int blueprintSchemeIndex;
    private GameObject blueprintCharacter;


    private void Awake()
    {
        for (int i = 0; i < availableCharacters.Count; i++)
        {
            GameObject placeholder = Instantiate(characterPlaceholder, characterPositions[i].position, characterPositions[i].rotation);
            MenuCharacterPlaceholder placeholderScript = placeholder.GetComponent<MenuCharacterPlaceholder>();

            placeholderScript.character = Instantiate(availableCharacters[i], placeholder.transform, false);

            placeholderScript.SetAnimatorController(characterController);
                        
            lastPlaceholderScript = placeholderScript;

            placeholderScripts.Add(lastPlaceholderScript);

            Debug.Log(i);
            
        }

        SetControls();
    }

    private void FixedUpdate()
    {
        if (Util.MouseHit(50, placeholderMask, out Collider placeholerCollider))
        {
            MenuCharacterPlaceholder placeholderScript = placeholerCollider.gameObject.GetComponent<MenuCharacterPlaceholder>();
            if (placeholderScript != lastPlaceholderScript)
                lastPlaceholderScript = placeholderScript;

            lastPlaceholderScript.SetIsPointed(true);
            
            isPointing = true;
        }
        else
        {
            lastPlaceholderScript.SetIsPointed(false);
            isPointing = false;
        }
    }

    #region TOP_SUBMENU
    public void SetControls()
    {
        selectedControlsIndex = dropdownControls.value;
        controlsDescriptionText.text = controlsDescriptions[selectedControlsIndex];
    }

    public int GetControls(PlayerBlueprint blueprint)
    {
        int _controlsIndex = 0;

        //controlSchemes.(blueprint.controlScheme);
        //controlSchemes
        

        return _controlsIndex;
    }



    #endregion TOP_SUBMENU

    #region BLUEPRINTS

    //public void AddBlueprint()
    //{
    //    PlayerBlueprint blueprint = CreateNewBlueprint();
    //    SetUp.playerBlueprints[currentPlayer] = blueprint;
    //}

    //public PlayerBlueprint CreateNewBlueprint()
    //{
    //    PlayerBlueprint newBlueprint = new();
    //
    //    newBlueprint.name = inputFieldName.text;
    //    newBlueprint.controlScheme = controlSchemes[selectedControlsIndex];
    //    newBlueprint.model = blueprintCharacter;
    //
    //    return newBlueprint;
    //}

    public void SetBlueprint()
    {
        PlayerBlueprint blueprint;

        if (SetUp.playerBlueprints[currentPlayer] == null)
            blueprint = new();

        else blueprint = SetUp.playerBlueprints[currentPlayer];

        blueprint.name = inputFieldName.text;
        blueprint.controlScheme = controlSchemes[selectedControlsIndex];
        blueprint.model = blueprintCharacter;

        SetUp.playerBlueprints[currentPlayer] = blueprint;
    }

    public void GetBlueprint()
    {
        PlayerBlueprint blueprint = SetUp.playerBlueprints[currentPlayer];

        inputFieldName.text = blueprint.name;



        blueprint.controlScheme = controlSchemes[selectedControlsIndex];
        blueprint.model = blueprintCharacter;
    }

    #endregion BLUEPRINTS

    #region CHARACTERS
    public void DeselectAll()
    {
        foreach(MenuCharacterPlaceholder script in placeholderScripts)
        {
            script.SetIsSelected(false);
        }
    }

    public void OnMouseLeft()
    {
        if (isPointing)
        {
            
            bool _isSelected = lastPlaceholderScript.GetIsSelected();

            DeselectAll();

            lastPlaceholderScript.SetIsSelected(!_isSelected);           
        }
    }
    #endregion CHARACTERS
}
