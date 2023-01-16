using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Mono.Cecil;
using UnityEditor;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    private Rigidbody2D ballRb;
    private Vector3 direction;
    private Vector3 ballMovement;

    private float speed = 5;
    private float speedIncrement = 1.25f;
    private int maxSpeed = 20;
    private float verticalBoundary = 4.8f;
    private float xBoundary = 12;
    private bool leftOfScreen = false;
    private bool rightOfScreen = false;

    private bool collidingWithPlayer = false;

    private AudioSource audioSource;
    public AudioClip[] soundEffects;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        //get rigid body
        ballRb= GetComponent<Rigidbody2D>();

        //get random direction for ball to move
        direction = RandomDirection();
        
        //set ball moevement to the direction with magnitude of set speed
        ballMovement = direction * speed;

        //get audio source component
        audioSource= GetComponent<AudioSource>();

        //get game manager reference
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void FixedUpdate()
    {
        //keep ball in bounds and "bounce" off of floor and ceiling
        if (transform.position.y >= verticalBoundary)
        {
            ballMovement = FlipVectorY(ballMovement);
            PlaySoundEffect(audioSource, soundEffects[0]);

        }
        if (transform.position.y <= -verticalBoundary)
        {
            ballMovement = FlipVectorY(ballMovement);
            PlaySoundEffect(audioSource, soundEffects[0]);

        }

        //handle all actions when ball collides with player
        if (collidingWithPlayer)
        {
            ballMovement = FlipVectorX(ballMovement);
            if (ballMovement.magnitude < maxSpeed)
            {
                ballMovement *= speedIncrement;
            }
            

            collidingWithPlayer = false;
        }


        //ALWAYS CONTROLLING BALL MOVEMENT WITH VECTOR3
        ballRb.velocity = ballMovement;
    }

    void Update()
    {
        //check for ball position off side of screen and flag it
        if (transform.position.x < -xBoundary)
        {
            leftOfScreen = true;
        } 
        else if (transform.position.x > xBoundary)
        {
            rightOfScreen = true;
        }

        //remove flag, destroy ball, update score
        if (leftOfScreen)
        {
            Destroy(gameObject);
            gameManager.UpdatePlayerOneScore(1);
            leftOfScreen = false;
        }
        else if (rightOfScreen)
        {
            Destroy(gameObject);
            gameManager.UpdatePlayerTwoScore(1);
            rightOfScreen = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlaySoundEffect(audioSource, soundEffects[0]);

        //set colliding flag when entering player hitbox
        if (other.CompareTag("Player"))
        {
            collidingWithPlayer = true;

        }
    }

    //produce a unit vector in random direction exluding values close to 0
    //this prevents a vert/horiz shot that is not fun gameplay
    Vector3 RandomDirection()
    {
        float randomY;
        do
        {
            randomY = Random.Range(-1f, 1f);
        } while (randomY > -0.75f && randomY < 0.75f);

        float randomX;
        do
        {
            randomX = Random.Range(-1f, 1f);
        } while (randomX > -0.75f && randomX < 0.75f);

        return new Vector3(randomX, randomY).normalized;
    }

    //return the same vector with y in opposite direction
    Vector3 FlipVectorY(Vector3 vector)
    {
        return new Vector3(vector.x, vector.y * -1, vector.z);
    }

    //return the same vector with x in the opposite direction
    Vector3 FlipVectorX(Vector3 vector)
    {
        return new Vector3(vector.x * -1, vector.y, vector.z);
    }

    void PlaySoundEffect(AudioSource audioSource, AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }


}
