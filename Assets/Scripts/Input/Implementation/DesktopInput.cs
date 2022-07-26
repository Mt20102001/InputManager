using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesktopInput : MonoBehaviour, IInputProfile
{
    public float Horizontal => Input.GetAxis("K_MainHorizontal");

    public float Vertical => Input.GetAxis("K_MainVertical");

    public bool Fire => Input.GetMouseButton(0);

    public InputType Type => InputType.DESKTOP;

}