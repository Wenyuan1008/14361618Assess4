using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Button level1Button;

    void Start()
    {
        level1Button.onClick.AddListener(LoadMainScene);
    }

    public void LoadMainScene()
    {
        SceneManager.LoadScene("Main");
    }
}
