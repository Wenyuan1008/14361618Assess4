using System.Collections;
using UnityEngine;

public class PacStudentController : MonoBehaviour
{
    public float moveSpeed = 10.0f; // �ƶ��ٶ�
    private Vector3 targetPosition; // PacStudent ��Ҫ�����Ŀ��λ��
    private Vector3 lastInput = Vector3.zero; // ���һ�ΰ����ķ���
    private Vector3 currentInput = Vector3.zero; // ��ǰ�ƶ��ķ���

    // ��Ƶ���
    public AudioSource audioSource;
    public AudioClip walkingAudio;

    // ��������Ч��
    public ParticleSystem dustParticles;

    // �������
    private Animator animator;

    private void Start()
    {
        targetPosition = transform.position; // ��ʼ��Ŀ��λ��Ϊ��ǰλ��
        animator = GetComponent<Animator>();
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
            animator.SetFloat("DirX", 0);
            animator.SetFloat("DirY", 1);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            lastInput = Vector3.down;
            animator.SetFloat("DirX", 0);
            animator.SetFloat("DirY", -1);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            lastInput = Vector3.left;
            animator.SetFloat("DirX", -1);
            animator.SetFloat("DirY", 0);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            lastInput = Vector3.right;
            animator.SetFloat("DirX", 1);
            animator.SetFloat("DirY", 0);
        }

        // ����Ŀ��λ��
        if (transform.position == targetPosition)
        {
            currentInput = lastInput;
            targetPosition += currentInput * 10; // ���������֮��ľ���Ϊ10
        }
    }

    private void MovePacStudent()
    {
        // �ƶ� PacStudent
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // ��Ƶ���ſ���
        if (transform.position != targetPosition && !audioSource.isPlaying)
        {
            audioSource.clip = walkingAudio;
            audioSource.Play();
        }
        else if (transform.position == targetPosition)
        {
            audioSource.Stop();
        }

        // ��������Ч������
        if (transform.position != targetPosition && !dustParticles.isPlaying)
        {
            dustParticles.Play();
        }
        else if (transform.position == targetPosition)
        {
            dustParticles.Stop();
        }

        // ��� PacStudent �Ѿ��ǳ��ӽ�Ŀ��λ�ã���ֱ�����õ�Ŀ��λ��
        if (Vector3.Distance(transform.position, targetPosition) < 0.001f)
        {
            transform.position = targetPosition;
        }
    }
}
