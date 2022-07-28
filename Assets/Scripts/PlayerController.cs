using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // [SerializeField] private Rigidbody rbPlayer;
    [SerializeField] private CharacterController characterControllerPlayer;
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
    private bool isGrounded, isJumping, isLanding, canPressJumpBtn;

    public event System.Action<string, float> OnMove;
    public event System.Action<string> OnRun;
    public event System.Action OnJump;
    public event System.Action<string> OnStatusJump;

    private void Start()
    {
        canPressJumpBtn = true;
    }

    public void ClearEvent()
    {
        OnMove = null;
        OnRun = null;
        OnJump = null;
        OnStatusJump = null;
    }

    // Update is called once per frame
    Coroutine startJump;
    private void Update()
    {
        CheckPlayerInGround();

        // -- Movement
        movement = new Vector3(GameInputManager.Instance.CurrentProfile.Horizontal, 0, GameInputManager.Instance.CurrentProfile.Vertical);
        Vector3 direction = movement.normalized;

        if (!isJumping && !isLanding)
        {
            if (direction.magnitude >= 0.1f)
            {
                // animatorPlayer.applyRootMotion = false;
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);

                moveDir = Quaternion.Euler(0.0f, targetAngle, 0.0f) * Vector3.forward;

                OnMove?.Invoke("Up", moveDir.magnitude);

                if (GameInputManager.Instance.CurrentProfile.Run)
                {
                    OnRun?.Invoke("Up");
                    speed = runSpeed;
                }
                else
                {
                    OnRun?.Invoke("Down");
                    speed = moveSpeed;
                }

                characterControllerPlayer.Move(moveDir.normalized * speed * Time.deltaTime);
            }
            else
            {
                OnMove?.Invoke("Down", 0);
                OnRun?.Invoke("Down");
            }
        }

        // -- Jump
        if (canPressJumpBtn)
        {
            if (GameInputManager.Instance.CurrentProfile.Jump && isGrounded)
            {
                startJump = StartCoroutine(StartJump());
                canPressJumpBtn = false;
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

    private IEnumerator StartJump()
    {
        isJumping = true;
        
        OnJump?.Invoke();
        yield return new WaitUntil(() => animJumpController.startJump == true);
        directionY = jumpForce;
        moveDir.x = 0;
        moveDir.z = 0;
        animJumpController.startJump = false;
        isJumping = false;
        yield return new WaitUntil(() => animJumpController.startLanding == true);
        animJumpController.startLanding = false;
        isLanding = true;
        yield return new WaitUntil(() => animJumpController.endJump == true);
        animJumpController.endJump = false;
        isLanding = false;
        canPressJumpBtn = true;
    }

    private void CheckPlayerInGround()
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

}
