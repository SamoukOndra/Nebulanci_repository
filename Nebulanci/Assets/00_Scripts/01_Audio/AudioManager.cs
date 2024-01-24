using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] GameObject audioList_Prefab;
    public static AudioList audioList;

    private void Awake()
    {
        audioList = audioList_Prefab.GetComponent<AudioList>();
    }

}
