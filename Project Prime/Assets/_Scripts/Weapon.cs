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
    private GameObject standardProjectile;
    [SerializeField]
    private GameObject chargedProjectile;
    [SerializeField]
    private LineRenderer beamRenderer;
    [SerializeField]
    private Transform spawnPoint;

    [Header("Weapon Stats")]
    [SerializeField]
    private bool chargeable;
    [SerializeField]
    private float chargeRate;
    [SerializeField]
    private float fireRate;


    private float fireTime;
    private float chargeTime;

    

    // Update is called once per frame
    void Update()
    {
        if (chargeable)
        {
            if (Input.GetButton("Fire 1"))
            {
                chargeTime = Time.time + chargeRate;
            }
            if(Input.GetButtonUp("Fire 1"))
            {
                if (Time.time >= chargeTime) Instantiate(chargedProjectile, spawnPoint.position, spawnPoint.rotation);
                else Instantiate(standardProjectile, spawnPoint.position, spawnPoint.rotation);
            }
        }
        else
        {
            if(fireType == FireType.Projectile)
            {
                if(Input.GetButton("Fire 1"))
                {
                    if(Time.time >= fireTime)
                    {
                        Instantiate(standardProjectile, spawnPoint.position, spawnPoint.rotation);
                        fireTime = Time.time + fireRate;
                    }
                }
            }
            else
            {
                RaycastFunctionality();
            }
        }
    }

    public virtual void RaycastFunctionality()
    {
        RaycastHit hit;
        if(Physics.Raycast(spawnPoint.position,spawnPoint.forward,out hit))
        {
            beamRenderer.SetPosition(0, spawnPoint.position);
            beamRenderer.SetPosition(1, hit.point);
        }
    }
}
