using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CanvasButtonsActions : MonoBehaviour
{

    //public InputActionAsset inputAsset;
    public InputAction restartAction;

    private void OnEnable()
    {
        restartAction.Enable();

    }

    private void OnDisable()
    {
        restartAction.Disable();

    }

    public void Update()
    {
        if (restartAction.phase == InputActionPhase.Performed)
        {
            Restart();
        }
    }

    public void Restart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level", UnityEngine.SceneManagement.LoadSceneMode.Single);
    }

    public void Menu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu", UnityEngine.SceneManagement.LoadSceneMode.Single);
    }

    public void Controls()
    {
#if UNITY_ANDROID
        GameObject.Find("Controls_Android").GetComponent<Canvas>().enabled = true;
#else
        GameObject.Find("Controls").GetComponent<Canvas>().enabled = true;
#endif
    }
}
