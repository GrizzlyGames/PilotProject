using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CharacterController))]
public class Player_Manager : MonoBehaviour
{

    public Component component = new Component();
    [System.Serializable]
    public class Component
    {
        public Animator animator;
        public CharacterController characterController;
    }

    public Health health = new Health();
    [System.Serializable]
    public class Health
    {
        public bool isAlive = true;
        public int currentHealth = 100;
        public int maxHealth = 100;
    }

    public Movement movement = new Movement();
    [System.Serializable]
    public class Movement
    {
        public bool isSprinting = false;
        public float walkSpeed = 3.0f;
        public float sprintSpeed = 6.0f;
        public bool canDive = true;
        public float gravity = 20.0F;
    }

    public Weapon weapon = new Weapon();
    [System.Serializable]
    public class Weapon
    {
        public bool canFire = false;
        public GameObject staticArrowGO;
        public GameObject dynamicArrowGO;
        public Transform arrowSpawnTrans;

        private float rayDistance = 10;
        private RaycastHit raycastHit;


        public void Shoot()
        {
            canFire = false;
            staticArrowGO.SetActive(false);

            Vector3 rayOrigin = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
            Vector3 rayDirection = (Camera.main.transform.TransformDirection(Vector3.forward));
            Debug.DrawRay(rayOrigin, rayDirection * rayDistance, Color.green);

            // Check if our raycast has hit anything
            if (Physics.Raycast(rayOrigin, rayDirection, out raycastHit, rayDistance))
            {
                arrowSpawnTrans.LookAt(raycastHit.point);
                Instantiate(dynamicArrowGO, staticArrowGO.transform.position, arrowSpawnTrans.rotation);
            }
        }
    }
}
