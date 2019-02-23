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

    private bool needGravity = false;
    public bool moveForward = false;
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

    public float rayLength;
    public float secondRayLength;
    public float maxRayDistance;

    public Camera eyes;


    private Vector3 moveH;
    private Vector3 moveV;

    private Rigidbody body;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 localDown = transform.rotation * -Vector3.up; //We get the localDown direction of the player regardless of rotation
        print(localDown);
        Gravity(localDown);

        RaycastHit groundHit;
        if(Physics.Raycast(transform.position, localDown * rayLength, out groundHit, maxRayDistance))
        {
            needGravity = false;
        }
        else
        {
            needGravity = true;
        }
        Debug.DrawRay(transform.position, localDown * rayLength, Color.red);

        

        RaycastHit forwardRay;
        if(Physics.Raycast(transform.position, transform.forward* secondRayLength, out forwardRay, maxRayDistance))
        {
            float angle = Vector3.Angle(forwardRay.normal, transform.forward);
            body.freezeRotation = false;
            transform.Rotate(-angle/2,0, 0);
            needGravity = false;
            //body.freezeRotation = true;
        }
        else
        {
           
        }
        Debug.DrawRay(transform.position, transform.forward * secondRayLength, Color.red);
        //Physics and character controller below
        

        float xMovement = Input.GetAxisRaw("Horizontal");
        float zMovement = Input.GetAxisRaw("Vertical");

         moveH = (transform.right * xMovement);
       
         moveV = (transform.forward * zMovement);
        
       
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

    public void Gravity(Vector3 localDown)
    {
        if(needGravity)
        {
           // print(localDown);
            transform.position += (localDown * gravityForce);
        }
    }
}
