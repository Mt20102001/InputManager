using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayerController : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float turnSmoothTime;

    private float targetAngle, angle, speed, turnSmoothVelocity;
    private Vector3 moveDir;

    public event System.Action<string, float> OnMove;
    public event System.Action<string> OnRun;
    public event System.Action OnJump;

    public void ClearEvent()
    {
        OnMove = null;
        OnRun = null;
        OnJump = null;
    }

    public void Move(Vector3 movement, bool isRunning, Transform cam)
    {
        Vector3 direction = movement.normalized;

        if (direction.magnitude >= 0.1f)
        {
            targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            moveDir = Quaternion.Euler(0.0f, targetAngle, 0.0f) * Vector3.forward;

            OnMove?.Invoke("Up", moveDir.magnitude);

            if (isRunning)
            {
                OnRun?.Invoke("Up");
                speed = runSpeed;
            }
            else
            {
                OnRun?.Invoke("Down");
                speed = moveSpeed;
            }

            characterController.Move(moveDir.normalized * speed * Time.deltaTime);
        }
    }

    public void Jump()
    {

    }
}
