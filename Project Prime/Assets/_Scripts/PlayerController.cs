using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5;
    public float sensitivity = 1;
    public Vector2 pitchBounds;
    public float jumpForce = 10;

    public Transform eyes;

    private float yaw;
    private float pitch;

    private float moveH;
    private float moveV;

    private Rigidbody body;
    private Vector3 movement;

    void Start()
    {
        movement = transform.position;
        Cursor.lockState = CursorLockMode.Locked;
        body = GetComponent<Rigidbody>();
    }


    void Update()
    {
       
        moveH = Input.GetAxis("Horizontal");
        moveV = Input.GetAxis("Vertical");

        yaw += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        pitch -= Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        pitch = Mathf.Clamp(pitch, pitchBounds.x, pitchBounds.y);

        movement += transform.forward * moveV * speed * Time.deltaTime;
        movement += transform.right * moveH * speed * Time.deltaTime;
    }
    // Update is called once per frame
    void FixedUpdate()
    {

        //body.velocity += movement;
        //body.MoveRotation(Quaternion.Euler(0, yaw, 0));
        //transform.eulerAngles = new Vector3(0, yaw, 0);
        eyes.localEulerAngles = new Vector3(pitch, 0, 0);
        if (Input.GetButtonDown("Jump"))
        {
            body.AddForce(Vector3.up * jumpForce);
        }

        if (Input.GetButtonDown("Cancel")) Cursor.lockState = CursorLockMode.None;
    }
}
