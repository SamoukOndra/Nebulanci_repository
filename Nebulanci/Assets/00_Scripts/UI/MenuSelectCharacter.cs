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
    private readonly string[] selectedControlsTextOptions = { "Keyboard 1", "Keyboard 2", "Keyboard 3" };
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
    //private readonly string[] controlSchemes = { "Player_1", "Player_2", "Player_3" };
    private List<int> usedControlSchemesIndexes = new();

    private MenuCharacterPlaceholder selectedCharacterScript;


    private void Awake()
    {
        InitialzeCharacterPlaceholders();

        //GetBlueprint();
        //SetControls();
        //NextControlScheme(true);

        gameObject.SetActive(false);

        void InitialzeCharacterPlaceholders()
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
        }
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

    public void UpdateControlsDescription()
    {
        Debug.Log("Selected controls index: " + selectedControlsIndex);
        controlsDescriptionText.text = controlsDescriptions[selectedControlsIndex];
        selectedControlsText.text = selectedControlsTextOptions[selectedControlsIndex];
    }

    #endregion TOP_SUBMENU

    #region BLUEPRINTS
    public void SetBlueprint()
    {
        PlayerBlueprint blueprint = SetUp.playerBlueprints[currentPlayer];

        blueprint.name = inputFieldName.text;
        blueprint.controlsIndex = selectedControlsIndex;
        blueprint.menuCharacterPlaceholderScript = selectedCharacterScript;

        SetUp.playerBlueprints[currentPlayer] = blueprint;
    }

    public void GetBlueprint()
    {

        PlayerBlueprint blueprint;
        //SetUp.DebugMsg();
        //Debug.Log(currentPlayer);
        //Debug.Log(SetUp.playerBlueprints.Count);

        if (SetUp.playerBlueprints.Count == currentPlayer)
        {
            blueprint = new();
            blueprint.name = "Player " + (currentPlayer + 1);
            NextControlScheme(true);
            SetUp.playerBlueprints.Add(blueprint);

            //UpdateControlScheme();
        }


        else blueprint = SetUp.playerBlueprints[currentPlayer];


        inputFieldName.text = blueprint.name;
        selectedCharacterScript = blueprint.menuCharacterPlaceholderScript;
        selectedControlsIndex = blueprint.controlsIndex;

        UpdateControlScheme();
    }

    #endregion BLUEPRINTS

    #region CHARACTERS
    public void DeselectCurrent()
    {
        //nutno vynulovat selectedChar pri novym hraci
        foreach (MenuCharacterPlaceholder script in placeholderScripts)
        {
            if (script == selectedCharacterScript) // case null null by nemel vadit
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

    //public void Next_DownPlayerSubmenu(bool add)
    //{
    //    int addition;
    //
    //    if (add) addition = +1;
    //    else addition = -1;
    //
    //    
    //}

    public void InitializePlayerSubmenu()
    {
        StartPlayerSelection();
        ClearPairsDictionary();
    }

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
        if (selectedCharacterScript != null)
            selectedCharacterScript.Block(false);

        UpdateControlsDescription();
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

        ClampIndex(add);

        while (takenControls.Contains(selectedControlsIndex))
        {
            selectedControlsIndex += addition;
            ClampIndex(add);
        }

        UpdatePair(currentPlayer, selectedControlsIndex);
        UpdateControlsDescription();

        
    }

    private void UpdateControlScheme()
    {
        List<int> takenControls = ControlsTaken(currentPlayer);

        while (takenControls.Contains(selectedControlsIndex))
        {
            selectedControlsIndex++;
            ClampIndex(true);
        }

        UpdatePair(currentPlayer, selectedControlsIndex);
        UpdateControlsDescription();
    }

    private void ClampIndex(bool add)
    {
        if (add && selectedControlsIndex >= (SetUp.maxPlayers))
        {
            selectedControlsIndex = 0;
        }

        else if (!add && selectedControlsIndex < 0)
        {
            selectedControlsIndex = (SetUp.maxPlayers - 1);
        }
    }

    public void ClearPairsDictionary()
    {
        pairs.Clear();
    }
}
