using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuSelectCharacter : MonoBehaviour
{
    private int currentPlayer = 0;

    [SerializeField] MenuManager menuManager;

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
    string[] controlsDescriptions = { "move: .... ESDF \n \nfire: .... A \n \nchange weapon ..... Q", "move: .... 8456 on numpad \n \nfire: .... + on numpad \n \nchange weapon ..... Enter on numpad", "move: .... IJKL \n \nfire: .... Space \n \nchange weapon ..... Right Alt" };

    //Blueprints
    private readonly string[] controlSchemes = { "Player_1", "Player_2", "Player_3" };

    //private string blueprintName;
    //private int blueprintSchemeIndex;
    //private GameObject selectedCharacter;
    private MenuCharacterPlaceholder selectedCharacterScript;


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

        //GetBlueprint();
        SetControls();

        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        GetBlueprint();
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

    public int GetControlsIndex(PlayerBlueprint blueprint)
    {
        int _controlsIndex = System.Array.IndexOf(controlSchemes, blueprint.controlScheme);

        return _controlsIndex;
    }



    #endregion TOP_SUBMENU

    #region BLUEPRINTS
    public void SetBlueprint()
    {
        PlayerBlueprint blueprint = SetUp.playerBlueprints[currentPlayer];

        blueprint.name = inputFieldName.text;
        blueprint.controlScheme = controlSchemes[selectedControlsIndex];
        //blueprint.character = selectedCharacter;
        blueprint.menuCharacterPlaceholderScript = selectedCharacterScript;

        SetUp.playerBlueprints[currentPlayer] = blueprint;
    }

    public void GetBlueprint()
    {
        
        PlayerBlueprint blueprint;

        if (SetUp.playerBlueprints[currentPlayer] == null)
        {
            blueprint = new();
            blueprint.name = "Player " + (currentPlayer + 1);
            SetUp.playerBlueprints[currentPlayer] = blueprint;
        }
            

        else blueprint = SetUp.playerBlueprints[currentPlayer];


        inputFieldName.text = blueprint.name;
        dropdownControls.value = GetControlsIndex(blueprint);
        selectedCharacterScript = blueprint.menuCharacterPlaceholderScript;

        //model treba doresit
        //blueprint.character = blueprintCharacter;
    }

    //private void NullDuplicatedCharacter(GameObject character)
    //{
    //    foreach(PlayerBlueprint pb in SetUp.playerBlueprints)
    //    {
    //        if (pb.character == character)
    //            pb.character = null;
    //    } // ne, spis zakazu double select, zustanou trsat
    //}

    #endregion BLUEPRINTS

    #region CHARACTERS
    public void DeselectCurrent()
    {
        //nutno vynulovat selectedChar pri novym hraci
        foreach(MenuCharacterPlaceholder script in placeholderScripts)
        {
            if(script == selectedCharacterScript) // case null null by nemel vadit
                script.SetIsSelected(false);
        }
    }

    public void OnMouseLeft()
    {
        if (isPointing)
        {
            
            bool _isSelected = lastPlaceholderScript.GetIsSelected();

            DeselectCurrent();

            lastPlaceholderScript.SetIsSelected(!_isSelected, out selectedCharacterScript);           
        }
    }
    #endregion CHARACTERS

    //public void SetCurrentMaxPlayers(int currentMaxPlayers)
    //{
    //    this.currentMaxPlayers = currentMaxPlayers;
    //}// v SetUpu !!!!!!!!!!!!!

    public void NextDownPlayerSubmenu()
    {
        EndPlayerSelection();
        currentPlayer++;

        if (currentPlayer == SetUp.playersAmount)
        {
            currentPlayer--;
            menuManager.NextMenu();
        }
        else StartPlayerSelection();
    }

    public void PreviousDownPlayerSubmenu()
    {
        EndPlayerSelection();
        currentPlayer--;

        if (currentPlayer == -1)
        {
            currentPlayer = 0;
            menuManager.PreviousMenu();
        }
        else StartPlayerSelection();
    }


    private void StartPlayerSelection()
    {
        GetBlueprint();
        if(selectedCharacterScript != null)
            selectedCharacterScript.Block(false);
    }

    private void EndPlayerSelection()
    {
        if (selectedCharacterScript != null)
            selectedCharacterScript.Block(true);

        SetBlueprint();
    }
}
