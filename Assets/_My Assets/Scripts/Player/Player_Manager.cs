﻿using System.Collections;
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
        public bool isMoving = false;
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
        public int arrowBaseForce = 25;
        public float arrowRealForce = 25;
        public int arrowMaxForce = 100;
        public GameObject staticArrowGO;
        public GameObject dynamicArrowGO;
        public Transform arrowSpawnTrans;
        public Transform arrowContainerTrans;

        private RaycastHit raycastHit;


        public void Shoot()
        {
            canFire = false;
            staticArrowGO.SetActive(false);

            Vector3 rayOrigin = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
            Vector3 rayDirection = (Camera.main.transform.TransformDirection(Vector3.forward));
            Debug.DrawRay(rayOrigin, rayDirection, Color.green);

            if (Physics.Raycast(rayOrigin, rayDirection, out raycastHit))
                arrowSpawnTrans.LookAt(raycastHit.point);
            else
                arrowSpawnTrans.rotation = Quaternion.identity;

            GameObject arrowInstance = Instantiate(dynamicArrowGO, staticArrowGO.transform.position, arrowSpawnTrans.rotation) as GameObject;
            arrowInstance.GetComponent<Arrow_Script>().force = arrowRealForce;
            arrowInstance.transform.SetParent(arrowContainerTrans);
            arrowRealForce = arrowBaseForce;
        }
    }
}
