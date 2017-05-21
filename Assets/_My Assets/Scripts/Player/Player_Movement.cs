using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Player_Manager))]
[RequireComponent(typeof(Animator))]
public class Player_Movement : MonoBehaviour
{
    private Player_Manager player;
    private Vector3 _moveDirection = Vector3.zero;

    private void Start()
    {
        player = GetComponent<Player_Manager>();
    }

    private void Update()
    {
        if (player.health.isAlive)
        {
            player.component.animator.SetFloat("moveX", Input.GetAxis("Horizontal"));
            player.component.animator.SetFloat("moveZ", Input.GetAxis("Vertical"));
            player.component.animator.SetFloat("lookX", Input.GetAxis("Mouse X"));
            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
                player.component.animator.SetBool("isMoving", true);
            else
                player.component.animator.SetBool("isMoving", false);

            if (Input.GetKeyUp(KeyCode.LeftShift) && player.movement.isSprinting)
            {
                player.component.animator.SetBool("isSprinting", false);
                player.movement.isSprinting = false;
            }

            if (Input.GetKeyDown(KeyCode.LeftShift) && !player.component.animator.GetBool("isSprinting"))
            {
                player.movement.isSprinting = true;
                player.component.animator.SetBool("isSprinting", true);
            }

            if (Input.GetButton("Jump") && player.component.animator.GetBool("isSprinting"))
            {
                if (player.movement.canDive)
                {
                    StartCoroutine(DiveDelay());
                }
            }
            _moveDirection = transform.forward * Input.GetAxis("Vertical") + transform.right * Input.GetAxis("Horizontal");
            _moveDirection.y -= player.movement.gravity * Time.deltaTime;
            if (!player.component.animator.GetCurrentAnimatorStateInfo(0).IsName("standing_draw_arrow"))
            {
                if (!player.movement.isSprinting || player.component.animator.GetBool("isAimming"))
                    _moveDirection *= player.movement.walkSpeed;
                else if (!player.component.animator.GetBool("isAimming"))
                    _moveDirection *= player.movement.sprintSpeed;
                player.component.characterController.Move(_moveDirection * Time.deltaTime);
            }            
        }
        else // Is dead
        {
            player.component.animator.SetFloat("moveX", 0);
            player.component.animator.SetFloat("moveZ", 0);
            player.component.animator.SetFloat("lookX", 0);
        }
    }


    private IEnumerator DiveDelay()
    {
        player.movement.canDive = false;
        player.component.animator.SetTrigger("dive");
        yield return new WaitForSeconds(1.5f);
        player.movement.canDive = true;
    }
}