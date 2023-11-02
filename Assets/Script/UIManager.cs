using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Button level1Button;       
    public AudioSource audioSource;   

    void Start()
    {
        level1Button.onClick.AddListener(LoadMainScene);
        if (audioSource && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    public void LoadMainScene()
    {
        if (audioSource && audioSource.isPlaying)
        {
            audioSource.Stop();
        }

        SceneManager.LoadScene("Main");
    }
}
