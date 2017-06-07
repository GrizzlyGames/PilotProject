using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Zoom : MonoBehaviour
{
    float Dis = -1f;

    private void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0) // forward scroll
            {
                if (Dis >= -3.0f)
                {
                    Dis = this.transform.localPosition.z + -0.1f;
                    this.transform.localPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y, Dis);
                }
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0) // back scroll
            {
                if(Dis < -0.5f)
                {
                    Dis = this.transform.localPosition.z + 0.1f;
                    Debug.Log("Dis: " + Dis);
                    this.transform.localPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y, Dis);
                }                
            }
        }
    }
}
