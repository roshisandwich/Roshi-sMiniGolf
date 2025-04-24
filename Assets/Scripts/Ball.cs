using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LineRenderer lr;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip hitSFX;
    [SerializeField] private AudioClip bounceSFX;
    [SerializeField] private AudioClip strikeSFX;
    [SerializeField] private GameObject goalFX;
    [SerializeField] private GameObject bounceFX;

    [Header("Attributes")]
    [SerializeField] private float maxPower = 10f;
    [SerializeField] private float power = 2f;
    [SerializeField] private float minimumSpeed = 0.4f;
    [SerializeField] private float maxGoalSpeed = 4f;

    private bool isDragging;
    private bool inHole;

    Vector3 lastVelocity;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private bool IsReady()
    {
        return rb.velocity.magnitude < 0.25f; // once the ball is actually slow enough, we can click and drag again
    }

    // Update is called once per frame
    void Update()
    {

        if (LevelManager.main.outOfStrokes && rb.velocity.magnitude <= 0.2f && LevelManager.main.levelCompleted != true)
        {
            LevelManager.main.GameOver();
            return;
        }

        PlayerInput();

        lastVelocity = rb.velocity;
    }

    private void PlayerInput()
    {
        if (!IsReady() || LevelManager.main.outOfStrokes) 
        {
            return;
        }
        Vector2 inputPos = Camera.main.ScreenToWorldPoint(Input.mousePosition); 
        float distance = Vector2.Distance(transform.position, inputPos);

        if (Input.GetMouseButtonDown(0) && distance <= 0.5f)
        {
            DragStart();
        }
        if (Input.GetMouseButton(0) && isDragging)
        {
            DragChange(inputPos);
        }
        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            DragRelease(inputPos);
        }
    }

    private void DragStart()
    {
        isDragging = true;
        lr.positionCount = 2;
    }
    private void DragChange(Vector2 inputPos)
    {
        Vector2 direction = (Vector2)transform.position - inputPos;

        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, (Vector2)transform.position + Vector2.ClampMagnitude((direction * power) / 2, maxPower / 2)); // makes it so that the line isn't exactly the entire length fo where we're shooting to give some inaccuracy so it's not PERFECT all the time
    }
    private void DragRelease(Vector2 inputPos)
    {
        float distance = Vector2.Distance(transform.position, inputPos);
        isDragging = false;
        lr.positionCount = 0;

        if (distance < 0.05f) // change her eoif you want more min distance
        {
            return;
        }

        Vector2 direction = (Vector2)transform.position - inputPos;

        audioSource.PlayOneShot(strikeSFX);
        LevelManager.main.IncreaseStroke(); // could have obstacles that increase ur stroke???? POSSIBLE RAHHH
        rb.velocity = Vector2.ClampMagnitude(direction * power, maxPower); // get the direction and times it by the power, and make it so it shouldn't be more than the maximum amount of pwoer we can have
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

            if (collision.gameObject.CompareTag("Bouncer"))
            {
                float speed = lastVelocity.magnitude;
                Vector2 direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);
                audioSource.PlayOneShot(bounceSFX);
                CameraShake.instance.ShakeCamera(0.125f, 2.25f);

                GameObject bounceEffect = Instantiate(bounceFX, transform.position, Quaternion.identity);
                Destroy(bounceEffect, 2f);

                rb.velocity = direction * Mathf.Max(speed, 6f);
            }
            else if (rb.velocity.magnitude > minimumSpeed)
            {
                float randomPitch = Random.Range(0.9f, 1.1f);
                audioSource.pitch = randomPitch;
                audioSource.PlayOneShot(hitSFX);
            }
 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Goal"))
        {
            CheckWinState();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Goal"))
        {
            CheckWinState();
        }
    }

    private void CheckWinState()
    {
        if (inHole)
        {
            return;
        }

        if (rb.velocity.magnitude <= maxGoalSpeed)
        {
            inHole = true;

            rb.velocity = Vector2.zero;
            gameObject.SetActive(false);

            GameObject effect = Instantiate(goalFX, transform.position, Quaternion.identity);

            Destroy(effect, 2f);

            LevelManager.main.LevelComplete();
        }
    }
}
