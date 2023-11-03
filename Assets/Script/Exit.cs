using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExitManager : MonoBehaviour
{
    public Button exitButton; // Exit按钮引用
    public AudioSource audioSource; // 音频源引用

    void Start()
    {
        // 添加按钮点击事件监听器
        exitButton.onClick.AddListener(ExitToStartScene);

        // 如果音频源存在且不正在播放，那么开始播放
        if (audioSource && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    public void ExitToStartScene()
    {
        Debug.Log("Trying to load StartScene"); // 添加这一行

        if (audioSource && audioSource.isPlaying)
        {
            audioSource.Stop();
        }

        SceneManager.LoadScene("StartScene");
    }


}
