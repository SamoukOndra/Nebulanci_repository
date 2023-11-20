using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioList : MonoBehaviour
{

    public AudioClip explosion;

    [Header("Weapons")]
    public AudioClip defaultReload;
    public AudioClip shot_melee;
    public AudioClip shot_pistol;
    public AudioClip shot_shotgun;
    public AudioClip shot_rifle;
    public AudioClip shot_rocketLauncher;
    public AudioClip rocket;

    [Header("Collision Materials")]
    public AudioClip cm_flesh;
    public AudioClip cm_propaneTank;
    public AudioClip cm_glass;

    public AudioClip playerDeath;
    public AudioClip npcDeath;
}
