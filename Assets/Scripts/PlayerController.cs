using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D playerRb;
    private float verticalInput;
    private float speed = 7.5f;
    private float verticalBoundary = 3.9f;

    // Start is called before the first frame update
    void Start()
    {
        playerRb= GetComponent<Rigidbody2D>();
    }

    private void Update()
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
