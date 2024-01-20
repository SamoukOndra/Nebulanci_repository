using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameStatistics : MonoBehaviour//, IComparable
{
    public static GameStatistics singleton;

    [SerializeField] GameObject resultCam;
    [SerializeField] int resultCamLayer = 11;

    [SerializeField] GameObject resultCharacterPlaceholder;
    [SerializeField] GameObject characterList;

    private List<GameObject> availableCharacters;
    
    private List<GameObject> resultCharacterPlaceholders = new();
    private List<PlayerStatistics> playerStatisticsList = new();
    private Vector3 placeholderOffset = new(-2, 0, 0);

    int playersAmount;

    private void Awake()
    {
        singleton = this;

        resultCam.SetActive(false);

        availableCharacters = characterList.GetComponent<CharacterList>().characters;
    }

    private void OnEnable()
    {
        playersAmount = SetUp.playersAmount;

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
        //character.layer = resultCamLayer;
        Util.SetLayerToAllChildren(resCharPlaceholderGO.transform, resultCamLayer);

        resCharPlaceholderScript.SetAnimatorControler();

        //PlayerStatistics playerStatistics = new(newPlayer, playerBlueprint.name);
        //resCharPlaceholderScript.AddPlayerStatistics(playerStatistics);
        //playerStatisticsList.Add(playerStatistics);

        PlayerStatistics ps = resCharPlaceholderGO.AddComponent<PlayerStatistics>();
        ps.SetPlayerAndName(newPlayer, playerBlueprint.name);
        playerStatisticsList.Add(ps);

        //resCharPlaceholderGO.SetActive(false);
        resCharPlaceholderScript.ActivateCharacter(false);

        
    }

    public void EndGame()
    {
        DecidePlayerRank();
        //SortResultPlaceholders();
        SpawnResultPlaceholders();
        resultCam.SetActive(true);
    }

    #region EndGameMethods
    private void DecidePlayerRank()
    {
        

        List<int> orderdScores = GetOrderedFinalScoresAndOrderPlaceholders();

        Debug.Log("ordered scores: " + orderdScores);

        SetRanks();

        //methods//
        
        List<int> GetOrderedFinalScoresAndOrderPlaceholders() //I am sorry for anti-SOLID :,(
        {
            List<int> _finalScores = new();
            Dictionary<int, int> _scoreIndexDict = new();
            List<GameObject> _orderedPlaceholders = new();

            Debug.Log("pa: " + playersAmount);
            Debug.Log("psl.count: " + playerStatisticsList.Count);

            for (int i = 0; i < playersAmount; i++)
            {
                int _score = playerStatisticsList[i].finalScore;

                _finalScores.Add(_score);
                if(!_scoreIndexDict.ContainsKey(_score))
                    _scoreIndexDict.Add(_score, i);
                Debug.Log("player " + (i+1) + "; final score: " + _score);
            }

            _finalScores.Sort();
            _finalScores.Reverse();

            for (int j = 0; j < playersAmount; j++)
            {
                if(_scoreIndexDict.TryGetValue(_finalScores[j], out int index))
                {
                    _orderedPlaceholders.Add(resultCharacterPlaceholders[index]);
                }
            }

            resultCharacterPlaceholders = _orderedPlaceholders;

            return _finalScores;
        }

        void SetRanks()
        {
            foreach(PlayerStatistics ps in playerStatisticsList)
            {
                
                int rank = (orderdScores.IndexOf(ps.finalScore))+1;
                ps.rank = rank;

                Debug.Log("name in playerStatistics: " + ps.playerName + "; rank: " + ps.rank); //ok
            }
        }
    }

    //private void SortResultPlaceholders()
    //{
    //    //nevimjaksetodela/*resultCharacterPlaceholders =*/ resultCharacterPlaceholders.OrderBy(GameObject => GameObject.GetComponent<ResultCharacterPlaceholder>().GetRank()).ToList();
    //
    //}

    private void SpawnResultPlaceholders()
    {
        
        for(int i = 0; i < playersAmount; i++)
        {
            resultCharacterPlaceholders[i].transform.position = placeholderOffset * i;
            //resultCharacterPlaceholders[i].SetActive(true);
            resultCharacterPlaceholders[i].GetComponent<ResultCharacterPlaceholder>().ActivateCharacter(true);
            HandleResultsTargetGroup.singleton.AddTarget(resultCharacterPlaceholders[i]);

            //Debug.Log("rank: " + (resultCharacterPlaceholders[i].GetComponent<ResultCharacterPlaceholder>().GetRank())); hlasi error, ale je to picovina
        }
    }

    #endregion
}
