using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
#if UNITY_ANDROID
        Destroy(gameObject);
#endif
    }

   public void ExitGame()
    {
        Application.Quit();
    }
}
