using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // [SerializeField] private Rigidbody rbPlayer;
    [SerializeField] private CharacterController characterControllerPlayer;
    [SerializeField] private CreateBulletPooling FireBullet;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform AttackPoint;
    [SerializeField] private Transform cam;
    [SerializeField] private float turnSmoothTime;

    private float directionY;
    private float gravity = 10f;
    private Vector3 movement;
    private Vector3 moveDir;
    private float turnSmoothVelocity;
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
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);

            moveDir = Quaternion.Euler(0.0f, targetAngle, 0.0f) * Vector3.forward;
            characterControllerPlayer.Move(moveDir.normalized * moveSpeed * Time.deltaTime);
        }

        // -- Jump
        if (GameInputManager.Instance.CurrentProfile.Jump && isGrounded)
        {
            directionY = jumpForce;
            moveDir.x = 0;
            moveDir.z = 0;
        }

        directionY -= gravity * Time.deltaTime;
        moveDir.y = directionY;
        characterControllerPlayer.Move(moveDir.normalized * moveSpeed * Time.deltaTime);

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
    }

}
