using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MorphballController : MonoBehaviour
{
    public Transform camTransform;
    public Vector3 camOffset;

    public float speed = 5;
    public float turnSpeed = 90;
    public float lerpSpeed = 10;
    public float gravityForce = 0.5f;
    public float deltaGround = 0.2f;
    public float jumpForce = 10;
    public float wallRange = 10;

    private Vector3 surfaceNormal;
    private Vector3 localUp;
    private Vector3 localDown;
    private float distanceToGround;
    private float vertSpeed = 0;
    private bool jumping;
    private bool grounded;
    private Rigidbody body;
    private Collider collider;
    
    // Start is called before the first frame update
    void Start()
    {
        localUp = transform.up;
        localDown = -transform.up;
        body = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
        body.freezeRotation = true;
        distanceToGround = transform.position.y- collider.bounds.extents.y;
    }
    private void FixedUpdate()
    {
        body.AddForce(gravityForce * body.mass * localDown);
    }

    // Update is called once per frame
    void Update()
    {
        localDown = -localUp;
        print(grounded);
        if (jumping) return;
        Ray ray;
        RaycastHit hit;
        if (Input.GetButtonDown("Jump"))
        {
            if (!grounded)
            {
                ray = new Ray(transform.position, transform.forward);
                if (Physics.Raycast(ray, out hit, wallRange))
                {
                    StartCoroutine(JumpToWall(hit.point, hit.normal));
                }
            }
            else
            {
                body.velocity += jumpForce * localUp;
            }
        }
        transform.Rotate(0, Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime, 0);
        ray = new Ray(transform.position, localDown);
        Debug.DrawRay(transform.position,localDown);
        if(Physics.Raycast(ray,out hit,2))
        {
            print("I hit the ground");
            print(hit.distance + ":" +(distanceToGround + deltaGround));
            grounded = hit.distance <= distanceToGround + deltaGround;
            surfaceNormal = hit.normal;
        }
        else
        {
            grounded = false;
            surfaceNormal = Vector3.up;
        }
        localUp = Vector3.Lerp(transform.up, surfaceNormal, lerpSpeed * Time.deltaTime);
        Vector3 forward = Vector3.Cross(transform.right, localUp);
        Quaternion targetRot = Quaternion.LookRotation(forward, localUp);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, lerpSpeed * Time.deltaTime);
        transform.Translate(0, 0, Input.GetAxis("Vertical") * speed * Time.deltaTime);

    }

    IEnumerator JumpToWall(Vector3 point, Vector3 normal)
    {
        jumping = true;
        body.isKinematic = false;
        Vector3 originalPos = transform.position;
        Quaternion originalRot = transform.rotation;
        Vector3 targetPos = point + normal * (distanceToGround + .5f);
        Vector3 forward = Vector3.Cross(transform.right, normal);
        Quaternion targetRot = Quaternion.LookRotation(forward, normal);
        for(float t = 0; t < 1.0; t += Time.deltaTime*(lerpSpeed/2))
        {
            transform.position = Vector3.Lerp(originalPos, targetPos, t);
            transform.rotation = Quaternion.Slerp(originalRot, targetRot, t);
            yield return new WaitForEndOfFrame();
        }
        localUp = normal;
        body.isKinematic = false;
        jumping = false;
    }

   
}
