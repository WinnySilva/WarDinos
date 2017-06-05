using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextFadeInFadeOut : MonoBehaviour {
    public float speed;

    private Text text;

    // Use this for initialization
    void Start () {
        text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        text.color = new Vector4(text.color.r,text.color.b,text.color.b, (Mathf.Sin(Time.time * speed) + 1.0f) / 2.0f);
    }
}
