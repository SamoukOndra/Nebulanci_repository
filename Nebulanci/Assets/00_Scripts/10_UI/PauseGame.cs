using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class PauseGame : MonoBehaviour
{
    [SerializeField] Canvas pauseMenu;

    [Header("Snapshots")]
    [SerializeField] AudioMixerSnapshot unpaused;
    [SerializeField] AudioMixerSnapshot paused;
    [SerializeField] float transitionTime = 0.5f;

    float timeFlow = 1;

    private void Awake()
    {
        PlayerInput playerInput = GetComponent<PlayerInput>();
        playerInput.SwitchCurrentControlScheme("Menu", Keyboard.current);

        pauseMenu.enabled = false;
        PauseGameMethod(false);
    }


    public void OnPauseMenu()
    {
        pauseMenu.enabled = !pauseMenu.enabled;
        PauseGameMethod(pauseMenu.enabled);
    }

    private void PauseGameMethod(bool isPaused)
    {
        StopTime(isPaused);
        StartCoroutine(HandleSoundCoroutine(isPaused));
        Cursor.visible = isPaused;
    }

    private void StopTime(bool isPaused)
    {
        if (isPaused) timeFlow = 0.000001f;
        else timeFlow = 1;

        Time.timeScale = timeFlow;
    }

    IEnumerator HandleSoundCoroutine(bool isPaused)
    {
        yield return new WaitForEndOfFrame();
        
        if (isPaused) paused.TransitionTo(transitionTime * timeFlow);
        else unpaused.TransitionTo(transitionTime * timeFlow);
    }

    ///////////////////////////////
    
    public void MainMenu()
    {
        SceneManager.LoadScene(Scenes.startMenu);
        //takhle budou muset znova klikat postavy atd...
    }

    public void ResetLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        
        //SceneManager.LoadScene(currentScene);
    }
}
