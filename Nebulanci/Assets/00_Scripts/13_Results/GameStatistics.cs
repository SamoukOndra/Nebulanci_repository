using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameStatistics : MonoBehaviour
{
    public static GameStatistics singleton;

    [SerializeField] GameObject resultCam;
    [SerializeField] int resultCamLayer = 11;

    [SerializeField] GameObject resultCharacterPlaceholder;
    [SerializeField] GameObject characterList;

    private List<GameObject> availableCharacters;
    
    private List<GameObject> resultCharacterPlaceholders = new();
    private List<PlayerStatistics> playerStatisticsList = new();
    private Vector3 placeholderOffset = new(2, 0, 0);

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

        ResultCharacterPlaceholder resCharPlaceholderScript = resCharPlaceholderGO.GetComponent<ResultCharacterPlaceholder>();

        GameObject character = Instantiate(availableCharacters[playerBlueprint.characterIndex], resCharPlaceholderGO.transform, false);
        character.transform.Rotate(Vector3.up * 180);
        resCharPlaceholderScript.AddCharacter(character);
        Util.SetLayerToAllChildren(resCharPlaceholderGO.transform, resultCamLayer);

        resCharPlaceholderScript.SetAnimatorControler();

        PlayerStatistics ps = resCharPlaceholderGO.AddComponent<PlayerStatistics>();
        ps.SetPlayerAndName(newPlayer, playerBlueprint.name);
        playerStatisticsList.Add(ps);
        resCharPlaceholderScript.AddPlayerStatistics(ps);

        resCharPlaceholderScript.ActivateCharacter(false);
    }

    public void EndGame()
    {
        DecidePlayerRank();
        SortResultPlaceholders();
        SpawnResultPlaceholders();
        resultCam.SetActive(true);
    }

    #region EndGameMethods
    private void DecidePlayerRank()
    {
        

        List<int> orderdScores = GetOrderedFinalScores();

        SetRanks();
        
        List<int> GetOrderedFinalScores()
        {
            List<int> _finalScores = new();

            Debug.Log("pa: " + playersAmount);
            Debug.Log("psl.count: " + playerStatisticsList.Count);

            for (int i = 0; i < playersAmount; i++)
            {
                int _score = playerStatisticsList[i].finalScore;

                _finalScores.Add(_score);
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
        IEnumerable<PlayerStatistics> orderedStatistics = playerStatisticsList.OrderBy(stat => stat.rank);
        orderedStatistics.ToList();
        
        foreach(PlayerStatistics ps in orderedStatistics)
        {
            Debug.Log("sorted: name: " + ps.playerName + "; psrank: " + ps.rank);

            resultCharacterPlaceholders.Add(ps.gameObject);
        }
    }

    private void SpawnResultPlaceholders()
    {
        
        for(int i = 0; i < playersAmount; i++)
        {
            Transform t = resultCharacterPlaceholders[i].transform;
            t.position = placeholderOffset * i;
            t.LookAt((2* t.position) - resultCam.transform.position);

            ResultCharacterPlaceholder rcpScript = resultCharacterPlaceholders[i].GetComponent<ResultCharacterPlaceholder>();//.ActivateCharacter(true);
            rcpScript.ActivateCharacter(true);
            rcpScript.FillStatisticsUI();
            rcpScript.PlayAnimation();

            HandleResultsTargetGroup.singleton.AddTarget(resultCharacterPlaceholders[i]);            
        }
    }

    #endregion
}
