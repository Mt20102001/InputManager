using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerInput : MonoBehaviour, IInputProfile
{
    public InputType Type => InputType.CONTROLLER;

    public float Horizontal => Input.GetAxis("J_MainHorizontal");

    public float Vertical => Input.GetAxis("J_MainVertical");

    public bool Fire => Input.GetButtonDown("X_Button");

    public bool Jump => Input.GetButtonDown("Y_Button");

    public bool Run => Input.GetAxis("LT") > 0;

    public bool Aim => Input.GetAxis("RT") > 0;

    public bool Pickup => Input.GetButtonDown("A_Button");

    public float HorizontalCamera => Input.GetAxis("J_RightHorizontal");

    public float VerticalCamera => Input.GetAxis("J_RightVertical");

    public string InputAxisNameCurrentOfXForCamera => "J_RightHorizontal";

    public string InputAxisNameCurrentOfYForCamera => "J_RightVertical";
}
