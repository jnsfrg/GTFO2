﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent (typeof(AudioSource))]

public class PlayVideo : MonoBehaviour {

    public MovieTexture movie;
    private AudioSource audio;
    public string nextScene;


	// Use this for initialization
	void Start () {
        GetComponent<RawImage>().texture = movie as MovieTexture;
        audio = GetComponent<AudioSource>();
        audio.clip = movie.audioClip;
        movie.Play();
        audio.Play();
	}
	
	// Update is called once per frame
	void Update () {
        if (!movie.isPlaying || Input.GetKeyDown("space"))
        {
            Debug.Log("movie end");
            Application.LoadLevel(nextScene);
        }

    }
}
