using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D playerRb;
    private float verticalInput;
    private float speed = 7.5f;
    private float verticalBoundary = 3.9f;

    private Camera mainCamera;
    private Vector3 leftEgde;
    private float leftOffset = 6.0f / 32.0f;

    // Start is called before the first frame update
    void Start()
    {
        playerRb= GetComponent<Rigidbody2D>();

        mainCamera = Camera.main;
        leftEgde = mainCamera.ViewportToWorldPoint(new Vector3(0, 0.5f, 1));
        leftEgde.x += leftOffset;

        transform.position = leftEgde;
    }

    void Update()
    {
        //keep player below ceiling
        if (transform.position.y >= verticalBoundary)
        {
            transform.position = new Vector3(transform.position.x, verticalBoundary, transform.position.z);
        }

        //keep player above ground
        if (transform.position.y <= -verticalBoundary)
        {
            transform.position = new Vector3(transform.position.x, -verticalBoundary, transform.position.z);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //PLAYER MOVEMENT

        //handle player input
        verticalInput = Input.GetAxisRaw("Vertical");

        playerRb.velocity = new Vector3(0, verticalInput * speed, 0);

    }
}
