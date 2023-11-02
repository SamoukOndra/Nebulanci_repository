using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MenuSelectCharacter : MonoBehaviour
{
    private int currentPlayer = 0;

    [SerializeField] MenuManager menuManager;

    [Header("Top Submenu")]
    [SerializeField] TMP_InputField inputFieldName;
    //[SerializeField] TMP_Dropdown dropdownControls;
    [SerializeField] TextMeshProUGUI selectedControlsText;
    private readonly string[] selectedControlsTextOptions = { "Keyboard 1", "Keyboard 2", "Keyboaerd 3" };
    [SerializeField] TextMeshProUGUI controlsDescriptionText;

    private int selectedControlsIndex = -1;


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

    //Controls
    string[] controlsDescriptions = { "move: .... ESDF \n \nfire: .... A \n \nchange weapon ..... Q", "move: .... 8456 on numpad \n \nfire: .... + on numpad \n \nchange weapon ..... Enter on numpad", "move: .... IJKL \n \nfire: .... Space \n \nchange weapon ..... Right Alt" };
    private Dictionary<int, int> pairs = new();

    //Blueprints
    private readonly string[] controlSchemes = { "Player_1", "Player_2", "Player_3" };
    private List<int> usedControlSchemesIndexes = new();

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
        //SetControls();
        NextControlScheme(true);

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
    //public void SetControls()
    //{
    //    if (usedControlSchemesIndexes.Contains(selectedControlsIndex))
    //    {
    //        usedControlSchemesIndexes.Remove(selectedControlsIndex);
    //    }
    //    //selectedControlsIndex = HandleControlSchemes(dropdownControls.value);
    //
    //
    //
    //    usedControlSchemesIndexes.Add(selectedControlsIndex);
    //
    //    controlsDescriptionText.text = controlsDescriptions[selectedControlsIndex];
    //}

    public void UpdateControlsDescription()
    {
        controlsDescriptionText.text = controlsDescriptions[selectedControlsIndex];
        selectedControlsText.text = selectedControlsTextOptions[selectedControlsIndex];
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
        //dropdownControls.value = GetControlsIndex(blueprint);
        //dropdownControls.
        selectedCharacterScript = blueprint.menuCharacterPlaceholderScript;
        
        //model treba doresit
        //blueprint.character = blueprintCharacter;
    }

    //private void BlockDropdownOptions()
    //{
    //    List<int> blockedControls = ControlsTaken(currentPlayer);
    //
    //    for(int i = 0; i < dropdownControls.options.Count; i++)
    //    {
    //        if (blockedControls.Contains(i))
    //        {
    //            //dropdownControls.options[i].
    //            
    //        }
    //    }
    //
    //
    //    //foreach(TMP_Dropdown.OptionData option in dropdownControls.options)
    //    //{
    //    //    if(option.)
    //    //}
    //}

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

        //selectedControlsIndex = HandleControlSchemes(dropdownControls.value);
    }

    private void EndPlayerSelection()
    {
        if (selectedCharacterScript != null)
            selectedCharacterScript.Block(true);

        //HandleControlSchemes(selectedControlsIndex);

        SetBlueprint();
    }

    //private int HandleControlSchemes(int controlsIndex)
    //{
    //    int maxIterations = 4;
    //    int iterations = 0;
    //
    //    int index = controlsIndex;
    //
    //    bool isAvailable = !usedControlSchemesIndexes.Contains(index);
    //
    //    while (!isAvailable && iterations < maxIterations)
    //    {
    //        iterations++;
    //
    //        index = ClampedIndex(index++);
    //
    //        isAvailable = !usedControlSchemesIndexes.Contains(index);
    //
    //    }
    //
    //    dropdownControls.value = index;
    //
    //    return index;
    //
    //    int ClampedIndex(int _index)
    //    {
    //        if (_index >= controlSchemes.Length) return 0;
    //        else return _index;
    //    }
    //}

    private void UpdatePair(int player, int controls)
    {
        if (pairs.TryGetValue(player, out int _value))
        {
            pairs.Remove(player);
        }

        pairs.Add(player, controls);
    }

    //blokne dropdown.values
    private List<int> ControlsTaken(int player)
    {
        List<int> takenControls = new();
        Dictionary<int, int> _pairs = pairs;
        _pairs.Remove(player);

        Dictionary<int, int>.ValueCollection schemes = _pairs.Values;
        foreach (int scheme in schemes)
        {
            takenControls.Add(scheme);
        }

        return takenControls;
    }

    

    public void NextControlScheme(bool add)
    {
        int addition;

        if (add) addition = +1;
        else addition = -1;

        List<int> takenControls = ControlsTaken(currentPlayer);
        
        selectedControlsIndex += addition;

        if (add && selectedControlsIndex >= SetUp.maxPlayers)
        {
            selectedControlsIndex = 0;
        }

        else if (!add && selectedControlsIndex < 0)
        {
            selectedControlsIndex = SetUp.maxPlayers;
        }

        while (takenControls.Contains(selectedControlsIndex))
        {
            selectedControlsIndex += addition;
        }

        UpdatePair(currentPlayer, selectedControlsIndex);
        UpdateControlsDescription();
    }
}
