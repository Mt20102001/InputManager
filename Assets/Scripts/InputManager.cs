using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputManager
{
    // -- Axis

    public static float MainHorizontal()
    {
        float r = 0.0f;
        r += Input.GetAxis("J_MainHorizontal");
        r += Input.GetAxis("K_MainHorizontal");
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }

    public static float MainVertical()
    {
        float r = 0.0f;
        r += Input.GetAxis("J_MainVertical");
        r += Input.GetAxis("K_MainVertical");
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }

    public static float SecondJoyHorizontal()
    {
        float r = 0.0f;
        r += Input.GetAxis("J_RightHorizontal");
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }

    public static float SecondJoyVertical()
    {
        float r = 0.0f;
        r += Input.GetAxis("J_RightVertical");
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }

    // -- Joystick

    public static Vector3 MainJoystick()
    {
        return new Vector3(MainHorizontal(), 0, MainVertical());
    }

    public static Vector3 SecondJoyJoystick()
    {
        return new Vector3(SecondJoyHorizontal(), 0, SecondJoyVertical());
    }

    // -- Button

    public static bool AButton()
    {
        return Input.GetButtonDown("A_Button");
    }

    public static bool BButton()
    {
        return Input.GetButtonDown("B_Button");
    }

    public static bool XButton()
    {
        return Input.GetButtonDown("X_Button");
    }

    public static bool YButton()
    {
        return Input.GetButtonDown("Y_Button");
    }
}
