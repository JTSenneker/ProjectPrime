using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float lookSpeed;

    public float speed;

    public float acceleration;

    public float mass;

    public Transform position;

    private bool gravity;
    public float gravityForce;

    public float xRotation;
    public float yRotation;
    public float eyesYRotation;
    public float eyesZRotation;

    public float charXRotation;
    public float charYRotation;
    public float charZRotation;

    public float accelerationMax;

    public float accelerationCap;

    public Vector2 pitchBounds;

    

    public Camera eyes;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        
        //Physics and character controller below
        Vector3 localDown = transform.rotation * -Vector3.up; //We get the localDown direction of the player regardless of rotation

        float xMovement = Input.GetAxisRaw("Horizontal");
        float zMovement = Input.GetAxisRaw("Vertical");

        Vector3 moveH = (transform.right * xMovement);
        Vector3 moveV = (transform.forward * zMovement);

        position.position = (moveH + moveV).normalized * Time.deltaTime;
        position.position *= acceleration * Time.deltaTime;
        acceleration += (speed / mass) * Time.deltaTime;
        if(acceleration > accelerationMax)
        {
            acceleration = accelerationCap;
        }
        transform.position += position.position;


        yRotation += Input.GetAxis("Mouse X") * lookSpeed * Time.deltaTime;
        xRotation -= Input.GetAxis("Mouse Y") * lookSpeed * Time.deltaTime;

        xRotation = Mathf.Clamp(xRotation, pitchBounds.x, pitchBounds.y);

    }


    private void FixedUpdate()
    {
        // transform.eulerAngles = new Vector3(charXRotation, yRotation, charZRotation);
        transform.eulerAngles = new Vector3(0, yRotation, 0);
        eyes.transform.localEulerAngles = new Vector3(xRotation, eyesYRotation, eyesZRotation);
    }
}
