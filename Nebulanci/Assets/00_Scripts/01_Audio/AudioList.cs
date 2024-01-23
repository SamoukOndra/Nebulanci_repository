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
    public AudioClip emptyMagClick;

    [Header("Collision Materials")]
    //public AudioClip cm_flesh;
    public AudioClip cm_propaneTank;
    public AudioClip cm_glass;
    public AudioClip cm_brick;
    public AudioClip cm_metal;
    public AudioClip cm_wood;

    public AudioClip playerDeath;
    public AudioClip npcDeath;

    public List<AudioClip> fleshHits;

    [Header("Zombies")]
    public List<AudioClip> zombieScreams;
    public List<AudioClip> zombieSpawns;

    [Header("Beeps, pops & ticks")]
    public AudioClip beepShort;
    public AudioClip beepLong;
    public AudioClip pop;
    public AudioClip spotlight;

    [Header("Music")]
    public AudioClip resultsMusic;



    public AudioClip GetFleshHit()
    {
        int r = Random.Range(0, fleshHits.Count);
        return fleshHits[r];
    }

    public AudioClip GetZombieScream()
    {
        int r = Random.Range(0, zombieScreams.Count);
        return zombieScreams[r];
    }

    public AudioClip GetZombieSpawn()
    {
        int r = Random.Range(0, zombieSpawns.Count);
        return zombieSpawns[r];
    }
}
