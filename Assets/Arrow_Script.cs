using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow_Script : MonoBehaviour
{
    public float force = 10;
    public float gravityForce = 1;
    private Rigidbody myRigidbody;
    private bool applyGravity = false;
    private bool hasHitGround = false;

    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        Debug.Log("Arrow actual speed: " + force);
    }
    private void Update()
    {
        if (this.transform.position.y <= 0) // Out of bounds.
            Destroy(this.gameObject);
    }

    private void FixedUpdate()
    {
        if (applyGravity && !hasHitGround)
            myRigidbody.velocity = Vector3.up * -gravityForce;
        else
            myRigidbody.velocity = transform.forward * force;
    }

    private void OnCollisionEnter(Collision collision)
    {
        force = 0;
        applyGravity = true;

        if (collision.transform.CompareTag("Ground"))
            hasHitGround = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.CompareTag("Ground"))
            hasHitGround = false;
    }
}
