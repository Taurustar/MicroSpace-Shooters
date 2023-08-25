using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public UnityEvent eventos;
    public int indexScene;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CallChange());
    }

    public IEnumerator CallChange()
    {
        Debug.Log("Inicio del Cambio de Escena");
        yield return new WaitForSeconds(1.5f);
        ChangeScene(indexScene);
    }

    public void ChangeScene(int indexScene)
    {
        SceneManager.LoadScene(indexScene);
    }
}
