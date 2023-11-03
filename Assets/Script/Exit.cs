using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExitManager : MonoBehaviour
{
    public Button exitButton; // Exit��ť����
    public AudioSource audioSource; // ��ƵԴ����

    void Start()
    {
        // ��Ӱ�ť����¼�������
        exitButton.onClick.AddListener(ExitToStartScene);

        // �����ƵԴ�����Ҳ����ڲ��ţ���ô��ʼ����
        if (audioSource && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    public void ExitToStartScene()
    {
        Debug.Log("Trying to load StartScene"); // �����һ��

        if (audioSource && audioSource.isPlaying)
        {
            audioSource.Stop();
        }

        SceneManager.LoadScene("StartScene");
    }


}
