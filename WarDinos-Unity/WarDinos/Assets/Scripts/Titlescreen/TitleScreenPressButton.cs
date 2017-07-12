using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleScreenPressButton : MonoBehaviour {
    public GameObject gui;
    public GameObject music;
    public GameObject textObjectToModify;
    public string messageOfTheModification;

    private bool pressed = false;

	// Update is called once per frame
	void Update () {
        if (!pressed && Input.GetButtonDown("Submit")) {
            pressed = true;

            gui.SetActive(true);
            music.SetActive(true);

            textObjectToModify.GetComponent<Text>().text = messageOfTheModification;
            CanvasGroup cggui = gui.GetComponent<CanvasGroup>();
            StartCoroutine(routine: fadeInCanvas(cggui));
        }
    }

    IEnumerator fadeInCanvas(CanvasGroup cg)
    {
        float time = 0.35f;
        while (cg.alpha < 1.0f)
        {
            cg.alpha += Time.deltaTime / time;
            yield return null;
        }
        cg.alpha = 1.0f;

        // Destroy the canvas components to enable the panel to have its buttons interactable
        Destroy(cg.GetComponentInParent<Canvas>());
        Destroy(cg);
    }
}
