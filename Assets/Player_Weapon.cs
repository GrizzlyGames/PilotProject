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
        if (Input.GetMouseButton(0)) // Left click
        {

        }
        if (Input.GetMouseButtonDown(1)) // Right click
        {
            player.component.animator.SetBool("isAimming", true);
        }
        if (Input.GetMouseButtonUp(1)) // Right click
        {
            player.component.animator.SetBool("isAimming", false);
        }

        if (player.component.animator.GetCurrentAnimatorStateInfo(0).IsName("standing_draw_arrow") && !player.component.animator.GetBool("arrowNotched"))
        {
            player.component.animator.SetBool("arrowNotched", true);
        }
        else if (!player.component.animator.GetBool("isAimming"))
            player.component.animator.SetBool("arrowNotched", false);
    }
}
