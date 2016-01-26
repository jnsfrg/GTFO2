﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;


public class UIManager : MonoBehaviour
{
    public GameObject pausePanel;
    public bool isPaused;
    void Start()
    {
        isPaused = false;
        Cursor.visible = false;
    }

    void Update()
    {
        
        if (isPaused)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>().enabled = false;
            PauseGame(true);
            Screen.lockCursor = false;
        }

        else
        {
            PauseGame(false);
            GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>().enabled = true;
            Screen.lockCursor = true;
        }


        if (Input.GetButtonDown("Cancel"))
        {
            SwitchPause();
        }

    }

    void PauseGame(bool state)
    {
        if (state)
        {
            pausePanel.SetActive(true);
           Time.timeScale = 0.0f;
        }
        else
        {
            Time.timeScale = 1.0f;
            pausePanel.SetActive(false);
        }


    }

    public void SwitchPause()
    {
        if (isPaused)
        {
            UnpauseAudio();
            isPaused = false;

        }

        else
        {
            PauseAudio();
            isPaused = true;
        }


    }

    public void QuitGame()
    {
        Application.LoadLevel("startScreen");
    }

    public void PauseAudio()
    {
        AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>(); //Build array of all audio sources
        foreach (AudioSource currentSource in allAudioSources)
        {
            currentSource.Pause();
        }
    }

    public void UnpauseAudio()
    {
       AudioSource[] sources = GameObject.FindGameObjectWithTag("Player").GetComponents<AudioSource>();
       for (int i = 0; i < sources.Length - 1; i++)
       {
           sources[i].Play();
       }
           
        
    }

}