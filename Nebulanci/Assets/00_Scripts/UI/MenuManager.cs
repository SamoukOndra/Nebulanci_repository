using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MenuManager : MonoBehaviour
{
    // PRED SPUSTENIM ENABLE GO CANVAS, ALE DISABLE CANVAS COMPONENT !!!!!!!!!!

    CinemachineBrain CM_brain;
    float cmDefaultBlendDuration; // pokud budou custom blends, todle budu muset doladit

    [SerializeField] List<CinemachineVirtualCamera> menuVcams;
    private CinemachineVirtualCamera activeMenuVcam;

    [SerializeField] List<Canvas> vcamsCanvases;
    private Canvas activeVcamCanvas;

    int activeVcamIndex = 0;


    private void Awake()
    {
        // jen pro development?
        DeactivateAllCanvases();

        CM_brain = Camera.main.GetComponent<CinemachineBrain>();
        cmDefaultBlendDuration = CM_brain.m_DefaultBlend.m_Time;
        Debug.Log(cmDefaultBlendDuration);

        //ActivateCam(activeVcamIndex);
        activeMenuVcam = menuVcams[activeVcamIndex];
        activeMenuVcam.Priority += 10;

        activeVcamCanvas = vcamsCanvases[activeVcamIndex];
        activeVcamCanvas.enabled = true;
    }

    public void NextMenu()
    {
        activeVcamIndex++;
        SwitchMenuVcam(activeVcamIndex);
        SwitchMenuCanvas(activeVcamIndex);
    }

    public void PreviousMenu()
    {
        activeVcamIndex--;
        SwitchMenuVcam(activeVcamIndex);
        SwitchMenuCanvas(activeVcamIndex);
    }



    private void SwitchMenuVcam(int index)
    {
        if (index >= 0 && index < menuVcams.Count)
        {
            activeMenuVcam.Priority -= 10;
            activeMenuVcam = menuVcams[index];
            activeMenuVcam.Priority += 10;
        }
    }

    private void SwitchMenuCanvas(int index)
    {
        if (index >= 0 && index < vcamsCanvases.Count)
        {
            activeVcamCanvas.enabled = false;
            activeVcamCanvas = vcamsCanvases[index];
            StartCoroutine(DelayCanvasEnabelingCoroutine(activeVcamCanvas, cmDefaultBlendDuration));
        }
    }

    // mozna jen pro vyvoj, neni asi nutno ve finale
    private void DeactivateAllCanvases()
    {
        foreach(Canvas c in vcamsCanvases)
        {
            c.enabled = false;
        }
    }

    IEnumerator DelayCanvasEnabelingCoroutine(Canvas canvas, float delay)
    {
        yield return new WaitForSeconds(delay);
        canvas.enabled = true;
    }
}
