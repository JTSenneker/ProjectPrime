using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))] //We always want to have a rigidbody on a moving object
public class Movement : MonoBehaviour
{
    [SerializeField]
    private float speed = 5;

    [SerializeField]
    private float lookSpeed = 5;

    //The length of the raycast shot down from this moving object
    [SerializeField]
    private float rayLength = .2f;

    public float maxRayDistance = 5;

    //Amount of gravity force applied to this moving object
    [SerializeField]
    public float gravityForce = 5;

    private float xRotation;
    private float yRotation;

    private bool gravity;

    public Vector2 pitchBounds;

    private Vector3 velocity = Vector3.zero;

    private Quaternion rotation;

    [SerializeField]
    private float acceleration = 0;


    //Get Components Rigidbody
    private Rigidbody body;

    public Camera eyes;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.E))
        {
            body.MoveRotation(Quaternion.Euler(-100, 0, 0));
            rotation = Quaternion.Euler(-100, 0, 0);
            xRotation = -100;
            //eyes.transform.localEulerAngles = new Vector3(-100, 0, 0);
            print("Is this working");
        }
        Vector3 localDown = transform.rotation * -Vector3.up;
        Gravity(localDown);
        RaycastHit hit;

        if(Physics.Raycast(transform.position, localDown * rayLength, out hit, maxRayDistance))
        {

            gravity = true;
        }//End of if statement
        else
        {
            gravity = false;
        }
        Debug.DrawRay(transform.position, localDown * rayLength, Color.red);
        //TODO FACTOR IN ACCELERATION
        //Get axis //TODO: Move into seperate method maybe?
        float xAxis = Input.GetAxisRaw("Horizontal");
        float zAxis = Input.GetAxisRaw("Vertical");

        //We use transform.right and transform.forward to move around relative to the objects
        //local transform so we can take into account rotation
        Vector3 moveH = (transform.right * xAxis);
        Vector3 moveV = (transform.forward * zAxis);
        //Get final moveent vector
        Vector3 newVelocity = (moveH + moveV).normalized * speed;
        SetObject(newVelocity);

         yRotation += Input.GetAxis("Mouse X") * lookSpeed * Time.deltaTime;
         xRotation -= Input.GetAxis("Mouse Y") * lookSpeed * Time.deltaTime;

        xRotation = Mathf.Clamp(xRotation, pitchBounds.x, pitchBounds.y);
       
    }

    public void SetObject(Vector3 newVelocity)
    {

        velocity = newVelocity;
            
    }

   

    //Runs after physics update
    private void FixedUpdate()
    {
        body.MoveRotation(Quaternion.Euler(0, yRotation, 0));
        transform.eulerAngles = new Vector3(-100, yRotation, 0);
        eyes.transform.localEulerAngles = new Vector3(xRotation, 0, 0);
        body.MovePosition(body.position + velocity * Time.deltaTime);
        
    }


    private void Gravity(Vector3 localDown)
    {
        if(!gravity)
        {
            body.AddForce(localDown * gravityForce);
        }

    }

    
}
