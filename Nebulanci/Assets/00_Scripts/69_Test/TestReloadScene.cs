using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestReloadScene : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            int scIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(scIndex);
        }
    }
}
