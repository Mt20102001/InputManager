using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesktopInput : MonoBehaviour, IInputProfile
{
    public float Horizontal => Input.GetAxis("Horizontal");

    public float Vertical => Input.GetAxis("Vertical");

    public bool Fire => Input.GetMouseButton(0);

    public InputType Type => InputType.DESKTOP;

}