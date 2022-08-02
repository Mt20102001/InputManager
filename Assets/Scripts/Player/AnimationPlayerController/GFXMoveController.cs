using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GFXMoveController : MonoBehaviour
{
    [SerializeField] private Animator animatorPlayer;
    [SerializeField] private MovePlayerController movePlayerController;

    private void Start()
    {
        movePlayerController.ClearEvent();
        movePlayerController.OnMove += MoveCallback;
        movePlayerController.OnRun += RunCallback;
        movePlayerController.OnJump += JumpCallback;
        movePlayerController.OnStatusJump += StatusJumpCallback;
    }

    private void MoveCallback(string status, float axisInput)
    {
        if (status.Equals("Up"))
        {
            animatorPlayer.SetFloat("Vertical", axisInput);
        }
        if (status.Equals("Down"))
        {
            animatorPlayer.SetFloat("Vertical", GetValueInputMoveDown(axisInput));
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

    private float GetValueInputMoveDown(float value)
    {
        return Mathf.Lerp(animatorPlayer.GetFloat("Vertical"), value, 0.1f);
    }

    private float GetValueInputRunUp()
    {
        return Mathf.Lerp(animatorPlayer.GetFloat("Run"), 1, 0.1f);
    }

    private float GetValueInputRunDown()
    {
        return Mathf.Lerp(animatorPlayer.GetFloat("Run"), 0, 0.1f);
    }
}
