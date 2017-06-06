﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrebitosController : MonoBehaviour
{
    public KeyCode button;
    public GameObject gameObjectTitlescreen;
    public GameObject gameObjectCrebitos;
    public AudioSource enteringSound;

    private MenuLocker titlescreenLocker;
    private MenuLocker crebitosLocker;

    // Use this for initialization
    void Start()
    {
        crebitosLocker = GetComponent<MenuLocker>();
        titlescreenLocker = gameObjectTitlescreen.GetComponent<MenuLocker>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(button))
            Debug.Log("Voltou da tela de Crebitos para Titlescreen" + "  " + titlescreenLocker.isLocked() + "  " + crebitosLocker.isLocked());
        if (!titlescreenLocker.isLocked() &&
            !crebitosLocker.isLocked() &&
            Input.GetKeyUp(button))
        {
            Debug.Log("Voltou da tela de Crebitos para Titlescreen");

            enteringSound.Play();

            titlescreenLocker.lockMenu();
            StartCoroutine(routine: fadeInGameObject(gameObjectTitlescreen));
            crebitosLocker.lockMenu();
            StartCoroutine(routine: fadeOutGameObject(gameObjectCrebitos));
        }
    }

    IEnumerator fadeOutGameObject(GameObject go)
    {
        CanvasGroup cg = go.GetComponent<CanvasGroup>();
        float time = 0.5f;
        while (cg.alpha > 0.0f)
        {
            cg.alpha -= Time.deltaTime / time;
            yield return null;
        }
        cg.alpha = 0.0f;
        crebitosLocker.unlockMenu();
        Debug.Log(crebitosLocker.isLocked());
    }

    IEnumerator fadeInGameObject(GameObject go)
    {
        CanvasGroup cg = go.GetComponent<CanvasGroup>();
        float time = 0.5f;
        while (cg.alpha < 1.0f)
        {
            cg.alpha += Time.deltaTime / time;
            yield return null;
        }
        cg.alpha = 1.0f;

        // Once titlescreen is on again, it can be used
        go.GetComponent<CanvasGroup>().interactable = true;

        titlescreenLocker.unlockMenu();
    }
}