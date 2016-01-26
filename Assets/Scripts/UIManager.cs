using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;


public class UIManager : MonoBehaviour
{
    public GameObject pausePanel;
    public bool isPaused;
    private AudioSource[] allAudioSources;
    private ArrayList audioSources;
    void Start()
    {
        isPaused = false;
        Cursor.lockState = UnityEngine.CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        
        if (isPaused)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>().enabled = false;
            PauseGame(true);
            Cursor.lockState = UnityEngine.CursorLockMode.None;
            Cursor.visible = true;
        }

        else
        {
            PauseGame(false);
            GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>().enabled = true;
            //Screen.lockCursor = true;
            Cursor.lockState = UnityEngine.CursorLockMode.Locked;
            Cursor.visible = false;
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
        allAudioSources = FindObjectsOfType<AudioSource>(); //Build array of all audio sources
        audioSources = new ArrayList();
        foreach (AudioSource currentSource in allAudioSources)
        {
            if (currentSource.isPlaying)
            {
                audioSources.Add(currentSource);
                currentSource.Pause();
            }
                
        }
    }

    public void UnpauseAudio()
    {
       /*AudioSource[] sources = GameObject.FindGameObjectWithTag("Player").GetComponents<AudioSource>();
       for (int i = 0; i < sources.Length - 1; i++)
       {
           sources[i].Play();
       }
       GameObject.FindGameObjectWithTag("tank").GetComponent<AudioSource>().Play();*/
        foreach (AudioSource currentSource in audioSources)
            currentSource.UnPause();
           
        
    }
}