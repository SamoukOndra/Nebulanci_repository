using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameStatistics : MonoBehaviour//, IComparable
{
    public static GameStatistics singleton;

    [SerializeField] GameObject resultCharacterPlaceholder;
    [SerializeField] GameObject characterList;

    private List<GameObject> availableCharacters;
    
    private List<GameObject> resultCharacterPlaceholders;
    private List<PlayerStatistics> playerStatisticsList;


    private void Awake()
    {
        singleton = this;

        availableCharacters = characterList.GetComponent<CharacterList>().characters;
    }

    private void OnEnable()
    {
        EventManager.OnPlayerAdded += InitializeResultCharacterPlaceholder;
    }

    private void OnDisable()
    {
        EventManager.OnPlayerAdded -= InitializeResultCharacterPlaceholder;
    }

    private void InitializeResultCharacterPlaceholder(GameObject newPlayer, PlayerBlueprint playerBlueprint)
    {
        GameObject resCharPlaceholderGO = Instantiate(resultCharacterPlaceholder);
        resultCharacterPlaceholders.Add(resCharPlaceholderGO);

        ResultCharacterPlaceholder resCharPlaceholderScript = resCharPlaceholderGO.GetComponent<ResultCharacterPlaceholder>();

        GameObject character = Instantiate(availableCharacters[playerBlueprint.characterIndex], resCharPlaceholderGO.transform, false);
        resCharPlaceholderScript.AddCharacter(character);

        resCharPlaceholderScript.SetAnimatorControler();

        PlayerStatistics playerStatistics = new(newPlayer, playerBlueprint.name);
        resCharPlaceholderScript.AddPlayerStatistics(playerStatistics);
        playerStatisticsList.Add(playerStatistics);

        resCharPlaceholderGO.SetActive(false);
    }

    public void EndGame()
    {
        DecidePlayerRank();
    }

    private void DecidePlayerRank()
    {
        int playersAmount = SetUp.playersAmount;

        List<int> orderdScores = GetOrderedFinalScores();

        SetRanks();

        //methods//
        
        List<int> GetOrderedFinalScores()
        {
            List<int> _finalScores = new();

            for (int i = 0; i < playersAmount; i++)
            {
                _finalScores.Add(playerStatisticsList[i].finalScore);
            }

            _finalScores.Sort();
            _finalScores.Reverse();

            return _finalScores;
        }

        void SetRanks()
        {
            foreach(PlayerStatistics ps in playerStatisticsList)
            {
                int rank = (orderdScores.IndexOf(ps.finalScore))+1;
                ps.rank = rank;
            }
        }
    }

    private void SortResultPlaceholders()
    {

    }
}
