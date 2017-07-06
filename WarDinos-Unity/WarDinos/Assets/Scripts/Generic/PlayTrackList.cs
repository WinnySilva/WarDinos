using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayTrackList : MonoBehaviour {
    public AudioSource[] trackList;

	void Start () {
        PlaySong();
	}

    void PlaySong () {
        AudioSource track = trackList[Random.Range(0, trackList.Length)];
        track.Play();
        Invoke("PlaySong", track.clip.length);
    }
}
