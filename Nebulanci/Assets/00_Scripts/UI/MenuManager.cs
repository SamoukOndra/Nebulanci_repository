using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MenuManager : MonoBehaviour
{
    // PRED SPUSTENIM ENABLE GO CANVAS, ALE DISABLE CANVAS COMPONENT !!!!!!!!!!

    CinemachineBrain CM_brain;
    float cmDefaultBlendDuration; // pokud budou custom blends, todle budu muset doladit

    [SerializeField] List<CinemachineVirtualCamera> vcams;
    private CinemachineVirtualCamera activeVcam;

    [SerializeField] List<Canvas> vcamsCanvases;
    private Canvas activeVcamCanvas;

    [SerializeField] List<GameObject> vcamsMenuManagers;
    private GameObject activeVcamMenuManager;

    int activeVcamIndex = 0;

    [Header("Submenus")]
    [SerializeField] GameObject playerCountSubmenu;

    //private int playersAmount;

    private void Awake()
    {
        // jen pro development?
        DeactivateAllCanvases();
        PlayerCountSubmenuActive(false);

        CM_brain = Camera.main.GetComponent<CinemachineBrain>();
        cmDefaultBlendDuration = CM_brain.m_DefaultBlend.m_Time;

        //ActivateCam(activeVcamIndex);
        activeVcam = vcams[activeVcamIndex];
        activeVcam.Priority += 10;

        activeVcamCanvas = vcamsCanvases[activeVcamIndex];
        activeVcamCanvas.enabled = true;

        activeVcamMenuManager = vcamsMenuManagers[activeVcamIndex];
        activeVcamMenuManager.SetActive(true);
    }

    public void NextMenu()
    {
        activeVcamIndex++;
        SwitchMenuVcam(activeVcamIndex);
        SwitchMenu(activeVcamIndex);
    }

    public void PreviousMenu()
    {
        activeVcamIndex--;
        SwitchMenuVcam(activeVcamIndex);
        SwitchMenu(activeVcamIndex);
    }



    private void SwitchMenuVcam(int index)
    {
        if (index >= 0 && index < vcams.Count)
        {
            activeVcam.Priority -= 10;
            activeVcam = vcams[index];
            activeVcam.Priority += 10;
        }
    }

    private void SwitchMenu(int index)
    {
        if (index >= 0 && index < vcamsCanvases.Count)
        {
            activeVcamCanvas.enabled = false;
            activeVcamCanvas = vcamsCanvases[index];

            activeVcamMenuManager.SetActive(false);
            activeVcamMenuManager = vcamsMenuManagers[index];
            StartCoroutine(DelayMenuEnabelingCoroutine(activeVcamCanvas, activeVcamMenuManager, cmDefaultBlendDuration));
        }
    }

    #region Submenus
    public void PlayerCountSubmenuActive(bool active)
    {
        playerCountSubmenu.SetActive(active);
    }

    #endregion Submenus

    public void SetPlayersAmount(int amount)
    {
        SetUp.playersAmount = amount;
        SetUp.EraseAllBlueprints();
    }

    // mozna jen pro vyvoj, neni asi nutno ve finale
    private void DeactivateAllCanvases()
    {
        foreach(Canvas c in vcamsCanvases)
        {
            c.enabled = false;
        }
    }


    IEnumerator DelayMenuEnabelingCoroutine(Canvas canvas, GameObject menuManager, float delay)
    {
        yield return new WaitForSeconds(delay);
        canvas.enabled = true;
        menuManager.SetActive(true);
    }
}
