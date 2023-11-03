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
    public AudioClip collisionSound;
    public ParticleSystem dustParticles;
    public ParticleSystem collisionParticles;

    private Animator animator;
    private Vector3 previousPosition;

    // Teleporter positions
    public Vector3 leftTeleporterPosition = new Vector3(5, 115, 0);
    public Vector3 rightTeleporterPosition = new Vector3(275, 115, 0);

    // Teleport cooldown variables
    private float lastTeleportTime = 0.0f;
    private float teleportCooldown = 1.0f; // 1 second cooldown

    private void Start()
    {
        targetPosition = transform.position;
        animator = GetComponent<Animator>();
        previousPosition = transform.position;
    }

    private void Update()
    {
        HandleInput();
        MovePacStudent();
        CheckForTeleport();
    }

    private bool CanMove(Vector3 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 1.0f);
        if (hit.collider != null && hit.collider.CompareTag("Wall"))
        {
            PlayCollisionEffects(hit.point);
            return false;
        }
        return true;
    }

    private void PlayCollisionEffects(Vector3 collisionPoint)
    {
        if (!audioSource.isPlaying)
        {
            audioSource.clip = collisionSound;
            audioSource.Play();
        }

        collisionParticles.transform.position = collisionPoint;
        collisionParticles.Play();
    }

    private void HandleInput()
    {
        Vector3 newInput = Vector3.zero;

        if (Input.GetKeyDown(KeyCode.W) && CanMove(Vector3.up))
        {
            newInput = Vector3.up;
            animator.SetFloat("DirX", 0);
            animator.SetFloat("DirY", 1);
        }
        else if (Input.GetKeyDown(KeyCode.S) && CanMove(Vector3.down))
        {
            newInput = Vector3.down;
            animator.SetFloat("DirX", 0);
            animator.SetFloat("DirY", -1);
        }
        else if (Input.GetKeyDown(KeyCode.A) && CanMove(Vector3.left))
        {
            newInput = Vector3.left;
            animator.SetFloat("DirX", -1);
            animator.SetFloat("DirY", 0);
        }
        else if (Input.GetKeyDown(KeyCode.D) && CanMove(Vector3.right))
        {
            newInput = Vector3.right;
            animator.SetFloat("DirX", 1);
            animator.SetFloat("DirY", 0);
        }

        if (newInput != Vector3.zero)
        {
            lastInput = newInput;
        }

        if (transform.position == targetPosition)
        {
            currentInput = lastInput;
            targetPosition += currentInput * 10;
        }
    }

    private void MovePacStudent()
    {
        previousPosition = transform.position;
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

    private void CheckForTeleport()
    {
        if (Time.time - lastTeleportTime < teleportCooldown)
        {
            return;
        }

        if (Vector3.Distance(transform.position, leftTeleporterPosition) < 0.5f)
        {
            transform.position = rightTeleporterPosition;
            targetPosition = transform.position + currentInput * 10;
            lastTeleportTime = Time.time;
        }
        else if (Vector3.Distance(transform.position, rightTeleporterPosition) < 0.5f)
        {
            transform.position = leftTeleporterPosition;
            targetPosition = transform.position + currentInput * 10;
            lastTeleportTime = Time.time;
        }
    }
}
