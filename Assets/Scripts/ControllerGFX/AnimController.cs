using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimController : MonoBehaviour
{
    [SerializeField] private PlayerController PlayerController;
    [SerializeField] private Animator animatorPlayer;



    private void Start()
    {
        PlayerController.ClearEvent();
        PlayerController.OnMove += MoveCallback;
        PlayerController.OnRun += RunCallback;
        PlayerController.OnJump += JumpCallback;
        PlayerController.OnStatusJump += StatusJumpCallback;
    }

    // private float GetSmoothDamp(float startFloat, float endFloat)
    // {
    //     // return Mathf.Lerp(, 1, 0.1f)
    // }

    private void MoveCallback(string status, float value)
    {
        if (status.Equals("Up"))
        {
            animatorPlayer.SetFloat("Vertical", value);
        }
        if (status.Equals("Down"))
        {
            animatorPlayer.SetFloat("Vertical", GetValueInputMoveDown(value));
        }
    }

    private void RunCallback(string value)
    {
        if (value.Equals("Up"))
        {
            animatorPlayer.SetFloat("Run", GetValueInputRunUp());
        }
        if (value.Equals("Down"))
        {
            animatorPlayer.SetFloat("Run", GetValueInputRunDown());
        }
    }

    private void JumpCallback()
    {
        animatorPlayer.SetTrigger("Jump");
    }

    private void StatusJumpCallback(string status)
    {
        if (status.Equals("IsGround"))
        {
            animatorPlayer.SetBool("Jumping", false);
        }

        if (status.Equals("OnAir"))
        {
            animatorPlayer.SetBool("Jumping", true);
        }
    }

    private float GetValueInputRunUp()
    {
        return Mathf.Lerp(animatorPlayer.GetFloat("Run"), 1, 0.1f);
    }

    private float GetValueInputRunDown()
    {
        return Mathf.Lerp(animatorPlayer.GetFloat("Run"), 0, 0.1f);
    }

    private float GetValueInputMoveDown(float value)
    {
        return Mathf.Lerp(animatorPlayer.GetFloat("Vertical"), value, 0.1f);
    }

}