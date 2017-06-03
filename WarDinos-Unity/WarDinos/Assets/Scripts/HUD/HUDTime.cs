using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDTime : MonoBehaviour {
    private float startTime;
    private float currentTime;

    private Text text;

	// Use this for initialization
	void Start () {
        startTime = Time.realtimeSinceStartup;
        text = GetComponent<Text>();
        text.text = "00:00";
	}
	
	// Update is called once per frame
	void Update () {
        float minutes = Mathf.Floor((Time.realtimeSinceStartup - startTime) / 60);
        float seconds = Mathf.Floor((Time.realtimeSinceStartup - startTime) % 60);
        text.text = minutes.ToString("00")+":"+seconds.ToString("00");
	}
}
