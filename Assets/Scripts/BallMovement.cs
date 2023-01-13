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
    private float verticalBoundary = 4.8f;

    private bool collidingWithPlayer = false;

    private AudioSource audioSource;
    public AudioClip[] soundEffects;

    // Start is called before the first frame update
    void Start()
    {
        ballRb= GetComponent<Rigidbody2D>();
        direction = RandomDirection();
        ballMovement = direction * speed;

        audioSource= GetComponent<AudioSource>();
    }

    // Update is called once per frame
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
            if (ballMovement.magnitude < 20)
            {
                ballMovement *= speedIncrement;
            }
            

            collidingWithPlayer = false;
        }


        //ALWAYS CONTROLLING BALL MOVEMENT WITH VECTOR3
        ballRb.velocity = ballMovement;
    }

    //produce a unit vector in random direction exluding xvalues close to zero
    Vector3 RandomDirection()
    {
        float randomY = Random.Range(-1f, 1f);

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

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlaySoundEffect(audioSource, soundEffects[0]);

        if (other.CompareTag("Player"))
        {
            collidingWithPlayer = true;

        }
    }

    void PlaySoundEffect(AudioSource audioSource, AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }


}
