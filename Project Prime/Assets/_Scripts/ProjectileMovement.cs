using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    public float speed = 20;
    public float lifeTime = 5;
    public float maxCharge = 5;
    public bool chargeable = true;
    private float chargeTime = 1;

    private float timeToDie;
    private bool fired = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && !fired)
        {
            chargeTime += Time.deltaTime;
            if (chargeTime >= maxCharge) chargeTime = maxCharge;
            transform.localScale = new Vector3(chargeTime, chargeTime, chargeTime);
        }
        if (Input.GetButtonUp("Fire1")) fired = true;
        if(fired){ 
            transform.SetParent(null);
            transform.position += transform.forward * speed * Time.deltaTime;
            lifeTime -= Time.deltaTime;
            if(lifeTime <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
