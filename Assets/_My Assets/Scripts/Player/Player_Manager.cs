﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CharacterController))]
public class Player_Manager : MonoBehaviour {

    public Component component = new Component();
    public class Component
    {
        public Animator animator;
        public CharacterController characterController;
    }

    public Health health = new Health();
    public class Health
    {
        public bool isAlive = true;
        public int currentHealth = 100;
        public int maxHealth = 100;
    }

    public Movement movement = new Movement();
    public class Movement
    {
        public float baseSpeed = 4.0F;
        public float jumpHeight = 8.0F;
        public float gravity = 20.0F;
    }

    private void Start()
    {
        component.animator = GetComponent<Animator>();
        component.characterController = GetComponent<CharacterController>();
    }
}
