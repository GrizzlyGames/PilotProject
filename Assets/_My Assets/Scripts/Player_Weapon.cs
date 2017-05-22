using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Player_Manager))]
public class Player_Weapon : MonoBehaviour {

    public GameObject propArrowGO;
    public GameObject ArrowGO;

    private Player_Manager player;
    private bool canFire = false;

	private void Start () {
        player = GetComponent<Player_Manager>();
	}

    private void Update () {
        if (Input.GetMouseButtonDown(0) && canFire && player.component.animator.GetBool("isAimming") && player.component.animator.GetBool("arrowNotched")) // Right click
        {
            canFire = false;

            Vector3 rayOrigin = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
            RaycastHit _hit;
            if (Physics.Raycast(rayOrigin, Camera.main.transform.forward, out _hit, Mathf.Infinity, 1 << 8))
            {
                Debug.Log("Rayhit: " + _hit.transform.name);
                /*
                if (_hit.transform.GetComponent<Enemy_Health_Script>())
                {
                    _hit.transform.GetComponent<Enemy_Health_Script>().Damage(player.weapon.realDamage);
                }
                */
            }

            Instantiate(ArrowGO, propArrowGO.transform.position, propArrowGO.transform.rotation);
            propArrowGO.SetActive(false);

            player.component.animator.SetTrigger("fire");
            player.component.animator.SetBool("arrowNotched", false);            
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
            StartCoroutine(CanFireDelay());
        }
        else if (!player.component.animator.GetBool("isAimming"))
        {
            player.component.animator.SetBool("arrowNotched", false);
            propArrowGO.SetActive(false);
        }            
    }
    private IEnumerator CanFireDelay()
    {
        player.component.animator.SetBool("arrowNotched", true);
        propArrowGO.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        canFire = true;
    }

}
