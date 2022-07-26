using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerInput : MonoBehaviour, IInputProfile
{
    public InputType Type => InputType.CONTROLLER;

    public float Horizontal => Input.GetAxis("J_MainHorizontal");

    public float Vertical => Input.GetAxis("J_MainVertical");

    public bool Fire => false;

}
