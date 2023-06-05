using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CanvasButtonsActions : MonoBehaviour
{

    //public InputActionAsset inputAsset;
    public InputAction restartAction;
    public InputAction returnAction;

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
            FinishLevel();
        }
        if (returnAction.phase == InputActionPhase.Performed)
        {
            Menu();
        }
    }

    public void StartLevel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(PersistentPlayerConfiguration.Instance.levelNames[PersistentPlayerConfiguration.Instance.currentPlayerLevel], UnityEngine.SceneManagement.LoadSceneMode.Single);
    }

    public void FinishLevel()
    {
        PersistentPlayerConfiguration.Instance.currentPlayerLevel++;

        if(PersistentPlayerConfiguration.Instance.currentPlayerLevel >= PersistentPlayerConfiguration.Instance.levelNames.Count)
        {
            PersistentPlayerConfiguration.Instance.currentPlayerLevel = 0;
        }
        UnityEngine.SceneManagement.SceneManager.LoadScene("IntermissionLevel", UnityEngine.SceneManagement.LoadSceneMode.Single);
    }

    public void Menu()
    {
        if (FindObjectOfType<PersistentPlayerConfiguration>())
            Destroy(FindObjectOfType<PersistentPlayerConfiguration>().gameObject);
        PersistentPlayerConfiguration.Instance.currentPlayerLevel = 0;
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
