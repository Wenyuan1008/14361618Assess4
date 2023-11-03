using System.Collections;
using UnityEngine;

public class PacStudentController : MonoBehaviour
{
    public float moveSpeed = 10.0f; 
    private Vector3 targetPosition; 
    private Vector3 lastInput = Vector3.zero; 
    private Vector3 currentInput = Vector3.zero; 


    public AudioSource audioSource;
    public AudioClip walkingAudio;


    public ParticleSystem dustParticles;


    private Animator animator;

    private void Start()
    {
        targetPosition = transform.position; 
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

        if (transform.position == targetPosition)
        {
            currentInput = lastInput;
            targetPosition += currentInput * 10; 
        }
    }

    private void MovePacStudent()
    {

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);


        if (transform.position != targetPosition && !audioSource.isPlaying)
        {
            audioSource.clip = walkingAudio;
            audioSource.Play();
        }
        else if (transform.position == targetPosition)
        {
            audioSource.Stop();
        }


        if (transform.position != targetPosition && !dustParticles.isPlaying)
        {
            dustParticles.Play();
        }
        else if (transform.position == targetPosition)
        {
            dustParticles.Stop();
        }


        if (Vector3.Distance(transform.position, targetPosition) < 0.001f)
        {
            transform.position = targetPosition;
        }
    }
}
