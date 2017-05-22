using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow_Script : MonoBehaviour
{
    public float force;
    public bool canFire = true;
    private Rigidbody rigidbody;

    private void Start()
    {
        if (canFire)
            rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (canFire)
            rigidbody.AddForce(transform.forward * force);
    }

    private void OnCollisionEnter(Collision collision)
    {
        force = 0;
    }
}
