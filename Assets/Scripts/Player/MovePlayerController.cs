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
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float gravity;

    private float targetAngle, angle, speed, turnSmoothVelocity, directionY;
    private bool isGrounded;
    private Vector3 moveDir;

    public event System.Action<string, float> OnMove;
    public event System.Action<string> OnRun;
    public event System.Action OnJump;
    public event System.Action<string> OnStatusJump;

    public void Initialize()
    {
        speed = 10;
    }

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
            //transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);
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
        else
        {
            OnMove?.Invoke("Down", 0);
            OnRun?.Invoke("Down");
        }
    }

    public void Jump(bool jump)
    {
        if (jump && isGrounded)
        {
            directionY = jumpForce;
            moveDir.x = 0;
            moveDir.z = 0;
        }
        directionY -= gravity * Time.deltaTime;
        moveDir.y = directionY;
        characterController.Move(moveDir.normalized * speed * Time.deltaTime);
    }

    public void CheckPlayerInGround()
    {
        RaycastHit hit;
        isGrounded = Physics.Raycast(transform.position, -transform.up, out hit, 0.2f, groundLayer);
        if (isGrounded)
        {
            OnStatusJump?.Invoke("IsGround");
        }
        else
        {
            OnStatusJump?.Invoke("OnAir");
        }
    }

    public float GetAnglePlayer()
    {
        return angle;
    }
}
