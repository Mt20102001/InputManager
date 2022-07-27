using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // [SerializeField] private Rigidbody rbPlayer;
    [SerializeField] private CharacterController characterControllerPlayer;
    [SerializeField] private Animator animatorPlayer;
    [SerializeField] private CreateBulletPooling FireBullet;
    [SerializeField] private AnimJumpController animJumpController;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform AttackPoint;
    [SerializeField] private Transform cam;
    [SerializeField] private float turnSmoothTime;
    [SerializeField] private float gravity;

    private float directionY;
    private float speed;
    private Vector3 movement;
    private Vector3 moveDir;
    private float turnSmoothVelocity;
    private float axisRun;
    private bool isGrounded;



    // Update is called once per frame
    private void Update()
    {
        CheckPlayerInGround();

        // -- Movement
        movement = new Vector3(GameInputManager.Instance.CurrentProfile.Horizontal, 0, GameInputManager.Instance.CurrentProfile.Vertical);
        Vector3 direction = movement.normalized;

        if (direction.magnitude >= 0.1f)
        {
            // animatorPlayer.applyRootMotion = false;
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);

            moveDir = Quaternion.Euler(0.0f, targetAngle, 0.0f) * Vector3.forward;

            animatorPlayer.SetFloat("Vertical", moveDir.magnitude);

            if (GameInputManager.Instance.CurrentProfile.Run)
            {
                float smoothDampRunUp = Mathf.Lerp(animatorPlayer.GetFloat("Run"), 1, 0.1f);
                animatorPlayer.SetFloat("Run", smoothDampRunUp);
                speed = runSpeed;
            }
            else
            {
                float smoothDampRunDown = Mathf.Lerp(animatorPlayer.GetFloat("Run"), 0, 0.1f);
                animatorPlayer.SetFloat("Run", smoothDampRunDown);
                speed = moveSpeed;
            }

            characterControllerPlayer.Move(moveDir.normalized * speed * Time.deltaTime);
        }
        else
        {
            // animatorPlayer.applyRootMotion = true;
            float smoothDampVerticalDOWN = Mathf.Lerp(animatorPlayer.GetFloat("Vertical"), 0, 0.1f);
            animatorPlayer.SetFloat("Vertical", smoothDampVerticalDOWN);
            animatorPlayer.SetFloat("Run", smoothDampVerticalDOWN);
        }

        // -- Jump


        if (animatorPlayer.GetBool("Jumping") == false)
        {
            if (GameInputManager.Instance.CurrentProfile.Jump && isGrounded)
            {
                if (animJumpController.startJump)
                {
                    directionY = jumpForce;
                    moveDir.x = 0;
                    moveDir.z = 0;
                    animJumpController.startJump = false;
                }
            }
        }
        directionY -= gravity * Time.deltaTime;
        moveDir.y = directionY;
        characterControllerPlayer.Move(moveDir.normalized * speed * Time.deltaTime);

        // -- Fire
        // if (GameInputManager.Instance.CurrentProfile.Fire)
        // {
        //     FireBullet.GetFromBool(AttackPoint.localPosition, AttackPoint.localRotation);
        // }

    }

    private void CheckPlayerInGround()
    {
        RaycastHit hit;
        isGrounded = Physics.Raycast(transform.position, -transform.up, out hit, 0.2f, groundLayer);
        if (isGrounded)
        {
            animatorPlayer.SetBool("Jumping", false);
        }
        else
        {
            animatorPlayer.SetBool("Jumping", true);
        }
    }

}
