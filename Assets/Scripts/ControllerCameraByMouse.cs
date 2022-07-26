using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerCameraByMouse : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float Speed;

    void Update()
    {
        // var rot = Quaternion.AngleAxis(this.Speed * Time.deltaTime, InputManager.SecondJoyJoystick());
        // var v = this.transform.position - player.transform.position;
        // v = rot * v;
        // this.transform.position = player.transform.position + v;

        
    }

    private Vector3 RotateCamera()
    {
        Vector3 dir = transform.TransformDirection(InputManager.SecondJoyJoystick());
        dir.Set(dir.x, 0, dir.z);
        return dir.normalized * InputManager.SecondJoyJoystick().magnitude;
    }
}
