using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow_Script : MonoBehaviour
{
    public float speed = 10;
    public float gravityForce = 1;
    public bool canFire = true;
    private Rigidbody myRigidbody;

    private void Start()
    {
        if (canFire)
            myRigidbody = GetComponent<Rigidbody>();
        Debug.Log("Arrow actual speed: " + speed);
    }

    private void Update()
    {
        transform.position += transform.forward * Time.deltaTime * speed;
    }

    private void FixedUpdate()
    {
        if (canFire)
            myRigidbody.velocity = Vector3.up * -gravityForce;
    }

    private void OnCollisionEnter(Collision collision)
    {
        speed = 0;
    }
}
