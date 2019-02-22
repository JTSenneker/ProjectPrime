using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityAttractor : MonoBehaviour
{
    public float raySize = 1;
   Rigidbody rb;
    public float gravityAmount = 5;
    public float maxRayDistance = 5;
    void Start()
    {
        rb = GetComponent<Rigidbody>(); 
    }

    // Update is called once per frame
    void Update()
    {
        var localDown = transform.rotation * -Vector3.up;
       // var localDown = -Vector3.up;
        RaycastHit hit;
        if( Physics.Raycast(transform.position,  localDown * raySize, out hit, maxRayDistance))
        {


            Debug.Log(hit.transform.name);
            Debug.DrawRay(transform.position, localDown * raySize, Color.red);
        }
        else
        {
            print("We should be applying gravity");
            Debug.DrawRay(transform.position, localDown * raySize, Color.red);
            rb.AddForce( (localDown * gravityAmount), ForceMode.Impulse);
        }
    }
}
