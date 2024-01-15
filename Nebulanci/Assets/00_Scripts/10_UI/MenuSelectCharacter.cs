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
    [SerializeField] TextMeshProUGUI controlsDescriptionText;
    [SerializeField] TextMeshProUGUI selectedControlsText;
    private readonly string[] selectedControlsTextOptions = { "Keyboard 1", "Keyboard 2", "Keyboard 3", "Gamepad", "Keyboard Singleplayer" };
    
    private int selectedControlsIndex = -1;


    [Header("Characters")]
    [SerializeField] GameObject characterPlaceholder;
    [SerializeField] LayerMask placeholderMask;

    [SerializeField] RuntimeAnimatorController characterController;

    [SerializeField] List<Transform> characterPositions;

    [SerializeField] GameObject characterList;
    private List<GameObject> availableCharacters; //tohle asi prefabnout

    public static List<MenuCharacterPlaceholder> placeholderScripts;
    private MenuCharacterPlaceholder lastPlaceholderScript;

    private bool isPointing = false;

    //Controls
    string[] controlsDescriptions = { 
        "move: .... ESDF \n \nfire: .... A \n \nchange weapon ..... Q",
        "move: .... 8456 on numpad \n \nfire: .... + on numpad \n \nchange weapon ..... Enter on numpad",
        "move: .... IJKL \n \nfire: .... Space \n \nchange weapon ..... Right Alt",
        "move: .... Left Stick \n \nfire: .... Right Trigger \n \nchange weapon ..... Left Trigger",
        "move: .... Arrows \n \nfire: .... C \n \nchange weapon ..... X",
    };

    private Dictionary<int, int> pairs = new();

    private MenuCharacterPlaceholder selectedCharacterScript;


    private void Awake()
    {
        availableCharacters = characterList.GetComponent<CharacterList>().characters;

        InitialzeCharacterPlaceholders();

        gameObject.SetActive(false);

        void InitialzeCharacterPlaceholders()
        {
            placeholderScripts = new();

            for (int i = 0; i < availableCharacters.Count; i++)
            {
                GameObject placeholder = Instantiate(characterPlaceholder, characterPositions[i].position, characterPositions[i].rotation);
                MenuCharacterPlaceholder placeholderScript = placeholder.GetComponent<MenuCharacterPlaceholder>();

                placeholderScript.character = Instantiate(availableCharacters[i], placeholder.transform, false);

                placeholderScript.SetAnimatorController(characterController);

                placeholderScript.characterIndex = i;

                lastPlaceholderScript = placeholderScript;

                placeholderScripts.Add(lastPlaceholderScript);
            }
        }
    }

    private void OnEnable()
    {
        //GetBlueprint(); //natavit taken controls, aby to hodilo singleplayer pokud je sp; po StartPlayerSelection je zase upravit. Nebo se na to vysrat, bo je to komplikovany
        StartPlayerSelection();
    }

    private void Update()
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
        if(selectedCharacterScript != null)
        {
            blueprint.characterIndex = selectedCharacterScript.characterIndex;
        }

        SetUp.playerBlueprints[currentPlayer] = blueprint;
    }

    public void GetBlueprint()
    {
        bool selectSinglePlayerScheme = false;

        PlayerBlueprint blueprint;

        if (SetUp.playerBlueprints.Count == currentPlayer)
        {
            blueprint = new();
            blueprint.name = "Player " + (currentPlayer + 1);
            //NextControlScheme(true);
            SetUp.playerBlueprints.Add(blueprint);

            if (SetUp.playersAmount == 1) selectSinglePlayerScheme = true;
        }

        else blueprint = SetUp.playerBlueprints[currentPlayer];

        inputFieldName.text = blueprint.name;
        selectedCharacterScript = blueprint.menuCharacterPlaceholderScript;
        selectedControlsIndex = blueprint.controlsIndex;

        if (selectSinglePlayerScheme)
        {
            NextControlScheme(false);
            selectSinglePlayerScheme = false;
        }

        else UpdateControlScheme(); //tohle setne CntrlScheme na prvni nezabrany
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
        Debug.Log("OnMouseLeft call");

        if (isPointing)
        {

            bool _isSelected = lastPlaceholderScript.GetIsSelected();

            DeselectCurrent();

            lastPlaceholderScript.SetIsSelected(!_isSelected, out selectedCharacterScript);
        }
    }
    #endregion CHARACTERS

    public void InitializePlayerSubmenu()
    {
        //StartPlayerSelection();
        ClearPairsDictionary();
    }

    public void TestInitializePlayerSubmenu() // for TestMenuSpeedUp only
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

        
    }

    private void EndPlayerSelection()
    {
        if (selectedCharacterScript != null)
            selectedCharacterScript.Block(true);

        SetBlueprint();
    }

    //private void UnblockPlayerModel(int currentPlayer)
    //{
    //
    //}

    #region CONTROLS

    public void ClearPairsDictionary()
    {
        pairs.Clear();
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

    private void UpdatePair(int player, int controls)
    {
        if (pairs.TryGetValue(player, out int _value))
        {
            pairs.Remove(player);
        }

        pairs.Add(player, controls);
    }

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
        if (add && selectedControlsIndex >= (selectedControlsTextOptions.Length))
        {
            selectedControlsIndex = 0;
        }

        else if (!add && selectedControlsIndex < 0)
        {
            selectedControlsIndex = (selectedControlsTextOptions.Length - 1);
        }
    }
    #endregion CONTROLS
}
