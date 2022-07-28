using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesktopInput : MonoBehaviour, IInputProfile
{
    public InputType Type => InputType.DESKTOP;
    public float Horizontal => Input.GetAxis("K_MainHorizontal");

    public float Vertical => Input.GetAxis("K_MainVertical");

    public bool Fire => Input.GetMouseButtonDown(0);

    public bool Jump => Input.GetKeyDown(KeyCode.Space);

    public bool Run => Input.GetKey(KeyCode.LeftShift);

    public bool Aim => Input.GetMouseButton(1);

    public float HorizontalCamera => Input.GetAxis("K_HorizontalCamera");

    public float VerticalCamera => Input.GetAxis("K_VerticalCamera");

    public string InputAxisNameCurrentOfXForCamera => "Mouse X";

    public string InputAxisNameCurrentOfYForCamera => "Mouse Y";
}