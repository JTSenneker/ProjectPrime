using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FireType
{
    Raycast,
    Projectile
}

public class Weapon : MonoBehaviour
{
    [Header("Fire Type")]
    [SerializeField]
    private FireType fireType;

    [Header("Spawned Objects")]
    [SerializeField]
    private GameObject projectile;
    [SerializeField]
    private LineRenderer beamRenderer;
    [SerializeField]
    private Transform spawnPoint;

    [Header("Weapon Stats")]
    [SerializeField]
    private bool chargeable;
    [SerializeField]
    private float beamSpeed;
    [SerializeField]
    private float fireRate;


    private float fireTime;
    private float chargeTime;

    private float beamLength;
    

    // Update is called once per frame
    void Update()
    {
        if(fireType == FireType.Projectile)
        {
            if(Input.GetButtonDown("Fire1"))
            {
                if(Time.time >= fireTime)
                {
                    Instantiate(projectile, spawnPoint);
                    fireTime = Time.time + fireRate;
                }
            }
        }
        else
        {
            RaycastFunctionality();
        }
    }

    public virtual void RaycastFunctionality()
    {
        beamRenderer.SetPosition(0, spawnPoint.position);
        //Debug.DrawLine(spawnPoint.position, spawnPoint.position + (spawnPoint.forward * 100), Color.red, 2);
        if (Input.GetButton("Fire1"))
        {
            beamLength += beamSpeed*Time.deltaTime;
            RaycastHit hit;
            if (Physics.Raycast(spawnPoint.position, spawnPoint.forward, out hit,beamLength))
            {
                beamRenderer.SetPosition(1, spawnPoint.position + spawnPoint.forward*1.25f);
                beamRenderer.SetPosition(2, hit.point);
            }
            else
            {
                
                beamRenderer.SetPosition(1, spawnPoint.position + spawnPoint.forward * 1.25f);
                beamRenderer.SetPosition(2, spawnPoint.position + spawnPoint.forward * 100);
            }
        }
        else beamRenderer.SetPosition(1, spawnPoint.position);
        beamLength = 0;
    }
}
