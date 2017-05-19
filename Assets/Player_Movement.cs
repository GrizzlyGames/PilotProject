using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Player_Manager))]
[RequireComponent(typeof(Animator))]
public class Player_Movement : MonoBehaviour {

    private Player_Manager player;

    private float _speed;
    private Vector3 _moveDirection = Vector3.zero;
    

    private void Start()
    {
        player = GetComponent<Player_Manager>();
        _speed = player.movement.baseSpeed;
    }

    private void Update()
    {
        if (player.health.isAlive)
        {
            if (player.component.characterController.isGrounded)
            {
                player.component.animator.SetBool("isGrounded", true);                
                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    _speed *= 2;
                    player.component.animator.speed = 1.3f;
                }
                else if (Input.GetKeyUp(KeyCode.LeftShift))
                {
                    _speed /= 2;
                    player.component.animator.speed = 1;
                }

                _moveDirection = transform.forward * Input.GetAxis("Vertical") + transform.right * Input.GetAxis("Horizontal");
                _moveDirection *= _speed;

                if (Input.GetButton("Jump"))
                {
                    player.component.animator.SetTrigger("jump");
                    _moveDirection.y = player.movement.jumpHeight;
                }

                player.component.animator.SetFloat("moveX", Input.GetAxis("Horizontal"));
                player.component.animator.SetFloat("moveZ", Input.GetAxis("Vertical"));
                float tmpMove = Mathf.Abs(Input.GetAxis("Horizontal") + Mathf.Abs(Input.GetAxis("Vertical")));
                player.component.animator.SetFloat("movement", tmpMove);
                player.component.animator.SetFloat("lookX", Input.GetAxis("Mouse X"));
            }
            else if (player.component.characterController.isGrounded || player.component.animator.GetCurrentAnimatorStateInfo(0).IsName("firing_rifle_inPlace"))
            {
                _moveDirection = Vector3.zero;
                player.component.animator.SetBool("isGrounded", false);
            }
            _moveDirection.y -= player.movement.gravity * Time.deltaTime;
            player.component.characterController.Move(_moveDirection * Time.deltaTime);
        }
        else
            return;
    }
}
