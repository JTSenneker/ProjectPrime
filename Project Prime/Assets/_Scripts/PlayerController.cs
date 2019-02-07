using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5;
    public float sensitivity = 1;
    public Vector2 pitchBounds;

    public Transform eyes;

    private float yaw;
    private float pitch;

    private float moveH;
    private float moveV;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;    
    }

    // Update is called once per frame
    void Update()
    {
        moveH = Input.GetAxis("Horizontal");
        moveV = Input.GetAxis("Vertical");

        transform.position += transform.forward * moveV * speed * Time.deltaTime;
        transform.position += transform.right * moveH * speed * Time.deltaTime;

        yaw += Input.GetAxis("Mouse X")*sensitivity*Time.deltaTime;
        pitch -= Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        pitch = Mathf.Clamp(pitch,pitchBounds.x, pitchBounds.y);

        transform.eulerAngles = new Vector3(0, yaw, 0);
        eyes.localEulerAngles = new Vector3(pitch, 0, 0);

        if (Input.GetButtonDown("Cancel")) Cursor.lockState = CursorLockMode.None;
    }
}
