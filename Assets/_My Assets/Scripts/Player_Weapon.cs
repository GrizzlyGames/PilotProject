using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Player_Manager))]
public class Player_Weapon : MonoBehaviour {

    private Player_Manager player;
    

	private void Start () {
        player = GetComponent<Player_Manager>();
	}

    private void Update () {
        if (Input.GetMouseButtonDown(0) && player.weapon.canFire && player.component.animator.GetBool("isAimming") && player.component.animator.GetBool("arrowNotched")) // Right click
        {            
            player.weapon.Shoot();
            player.component.animator.SetTrigger("fire");
            player.component.animator.SetBool("arrowNotched", false);            
        }
        if (Input.GetMouseButtonDown(1)) // Right click
        {
            player.weapon.arrowRealForce = player.weapon.arrowBaseForce;
        }
            if (Input.GetMouseButton(1)) // Right click
        {
            player.component.animator.SetBool("isAimming", true);
            
            if (player.movement.isMoving)
                player.weapon.arrowRealForce = player.weapon.arrowBaseForce;
            else
            {
                if(player.weapon.arrowRealForce < player.weapon.arrowMaxForce)
                {                    
                    player.weapon.arrowRealForce +=  (player.weapon.arrowMaxForce / player.weapon.arrowBaseForce)  * Time.deltaTime;
                    //Debug.Log("Player weapon arrowRealSpeed: " + player.weapon.arrowRealSpeed);
                }
            }            
        }
        if (Input.GetMouseButtonUp(1)) // Right click
        {
            player.component.animator.SetBool("isAimming", false);
        }

        if (player.component.animator.GetCurrentAnimatorStateInfo(0).IsName("standing_draw_arrow") && !player.component.animator.GetBool("arrowNotched"))
        {            
            StartCoroutine(CanFireDelay());
        }
        else if (!player.component.animator.GetBool("isAimming"))
        {
            player.component.animator.SetBool("arrowNotched", false);
            player.weapon.staticArrowGO.SetActive(false);
        }            
    }
    private IEnumerator CanFireDelay()
    {
        player.component.animator.SetBool("arrowNotched", true);
        player.weapon.staticArrowGO.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        player.weapon.canFire = true;
    }

}
