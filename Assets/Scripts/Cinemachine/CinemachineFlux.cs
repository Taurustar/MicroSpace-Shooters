using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemachineFlux : MonoBehaviour
{
    public GameObject cam01;
    public GameObject cam02;
    public GameObject cam03;
    public GameObject cam04;
    public GameObject cam05;

    // Start is called before the first frame update
    void Start()
    {
        cam01.SetActive(true);
        cam02.SetActive(false);
        cam03.SetActive(false);
        cam04.SetActive(false);
        cam05.SetActive(false);

        
    }

    public void SequenceActivate()
    {
        if(gameObject.name == "erdos")
        {
            StartCoroutine(DeactivateCam01());
        }
    }

    IEnumerator DeactivateCam01()
    {
        Debug.Log("Deactivate Cam01");
        yield return new WaitForSeconds(1f);
        StartCoroutine(ActivateCam02());
    }

    IEnumerator ActivateCam02()
    {
        cam01.SetActive(false);
        cam02.SetActive(true);
        yield return new WaitForSeconds(2f);
        StartCoroutine(ActivateCam03());
    }

    IEnumerator ActivateCam03()
    {
        cam02.SetActive(false);
        cam03.SetActive(true);
        yield return new WaitForSeconds(2f);
        StartCoroutine(ActivateCam04());
    }

    IEnumerator ActivateCam04()
    {
        cam03.SetActive(false);
        cam04.SetActive(true);
        yield return new WaitForSeconds(2f);
        StartCoroutine(ActivateCam05());

    }

    IEnumerator ActivateCam05()
    {
        cam04.SetActive(false);
        cam05.SetActive(true);
        yield return new WaitForSeconds(2f);
        Debug.Log("End of Intro");
    }
}
