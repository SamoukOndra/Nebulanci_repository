using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMenuSpeedUp : MonoBehaviour
{
    public GameObject menuManager;
    private MenuManager mm;

    public GameObject menuSelectCharacter;
    private MenuSelectCharacter msc;

    public GameObject menuSelectMap;
    private MenuSelectMap msm;
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(.1f);

        mm = menuManager.GetComponent<MenuManager>();
        msc = menuSelectCharacter.GetComponent<MenuSelectCharacter>();
        msm = menuSelectMap.GetComponent<MenuSelectMap>();

        mm.PlayerCountSubmenuActive(true);
        mm.NextMenu(); ////
        mm.SetPlayersAmount(1);
        msc.InitializePlayerSubmenu();

        msc.NextDownPlayerSubmenu();
        msc.NextDownPlayerSubmenu();

        msm.SelectScene(2);
        msm.LoadSelectedScene();
        

    }
}
