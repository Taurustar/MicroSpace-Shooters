using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class CinemachineFlux : MonoBehaviour
{
    private GameObject cam01;
    private GameObject cam02;
    private GameObject cam03;
    private GameObject cam04;
    private GameObject cam05;

    public GameObject camera;
    private GameObject canvas;

    // Start is called before the first frame update
    void Start()
    {
        camera = gameObject;
        camera.GetComponent<PlayableDirector>().playOnAwake = false;
    }

    public void SequenceActivate()
    {
        StartCoroutine(DeactivateCam01());
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
