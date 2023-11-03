using System.Collections;
using UnityEngine;

public class PacStudentController : MonoBehaviour
{
    public float moveSpeed = 10.0f; // 移动速度
    private Vector3 targetPosition; // PacStudent 将要到达的目标位置
    private Vector3 lastInput = Vector3.zero; // 最后一次按键的方向
    private Vector3 currentInput = Vector3.zero; // 当前移动的方向

    // 音频相关
    public AudioSource audioSource;
    public AudioClip walkingAudio;

    // 尘土粒子效果
    public ParticleSystem dustParticles;

    private void Start()
    {
        targetPosition = transform.position; // 初始化目标位置为当前位置
    }

    private void Update()
    {
        HandleInput();
        MovePacStudent();
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            lastInput = Vector3.up;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            lastInput = Vector3.down;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            lastInput = Vector3.left;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            lastInput = Vector3.right;
        }

        // 更新目标位置
        if (transform.position == targetPosition)
        {
            currentInput = lastInput;
            targetPosition += currentInput * 10; // 格子与格子之间的距离为10
        }
    }

    private void MovePacStudent()
    {
        // 移动 PacStudent
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // 音频播放控制
        if (transform.position != targetPosition && !audioSource.isPlaying)
        {
            audioSource.clip = walkingAudio;
            audioSource.Play();
        }
        else if (transform.position == targetPosition)
        {
            audioSource.Stop();
        }

        // 尘土粒子效果控制
        if (transform.position != targetPosition && !dustParticles.isPlaying)
        {
            dustParticles.Play();
        }
        else if (transform.position == targetPosition)
        {
            dustParticles.Stop();
        }

        // 如果 PacStudent 已经非常接近目标位置，则直接设置到目标位置
        if (Vector3.Distance(transform.position, targetPosition) < 0.001f)
        {
            transform.position = targetPosition;
        }
    }
}
