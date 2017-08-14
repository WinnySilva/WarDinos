using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollText : MonoBehaviour {
    private RectTransform rectTransform;
    private bool isScrolling = false;
    public float positionToStop;
    private float initialPosition;

	// Use this for initialization
	void Start () {
        rectTransform = GetComponent<RectTransform>();
        initialPosition = rectTransform.position.y;
    }
	
	// Update is called once per frame
	void Update () {
		if (isScrolling) {
            Vector2 vec2 = rectTransform.position;
            if (rectTransform.position.y < positionToStop) {
                vec2.y += Time.deltaTime;
                rectTransform.position = vec2;
            }
            else {
                vec2.y = positionToStop;
                isScrolling = false;
            }
        }
	}

    public void Scroll () {
        if (rectTransform.position.y < positionToStop) {
            isScrolling = true;
        }
    }

    public void ResetScrolling () {
        Vector2 vec2 = rectTransform.position;
        vec2.y = initialPosition;
        rectTransform.position = vec2;
        isScrolling = false;
    }
}
