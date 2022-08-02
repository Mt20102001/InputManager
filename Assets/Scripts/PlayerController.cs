using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]private Rigidbody rbPlayer;
    [SerializeField]private float moveSpeed;
    private Vector3 movement;
<<<<<<< Updated upstream
=======
    private Vector3 moveDir;
    private float turnSmoothVelocity;
    private bool isGrounded, isJumping, isLanding, canPressJumpBtn, canMovement;
    private float targetAngle;
    private float angle;
    private float lastShoot;

    public event System.Action<string, float> OnMove;
    public event System.Action<string> OnRun;
    public event System.Action OnJump;
    public event System.Action<string> OnStatusJump;
    public event System.Action<string> OnAim;
    public event System.Action<Vector3, Vector3> OnShoot;
    public event System.Action<string, bool> OnPickup;

    private void Start()
    {
        canPressJumpBtn = true;
        canMovement = true;
    }
>>>>>>> Stashed changes

    public void Initialize()
    {
        GameInputManager.Instance.SwitchProfile(InputType.CONTROLLER);
    }

    // Update is called once per frame
    private void Update()
    {
<<<<<<< Updated upstream
        movement.x = GameInputManager.Instance.CurrentProfile.Horizontal;
        movement.z = GameInputManager.Instance.CurrentProfile.Vertical;
=======
        CheckPlayerInGround();

        // -- Movement
        if (canMovement)
        {
            movement = new Vector3(GameInputManager.Instance.CurrentProfile.Horizontal, 0, GameInputManager.Instance.CurrentProfile.Vertical);
            Vector3 direction = movement.normalized;

            if (!isJumping && !isLanding)
            {
                if (direction.magnitude >= 0.1f)
                {
                    targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                    angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

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
        }


        // -- Jump
        if (canPressJumpBtn && canMovement)
        {
            if (GameInputManager.Instance.CurrentProfile.Jump && isGrounded)
            {
                if (!GameInputManager.Instance.CurrentProfile.Aim)
                {
                    startJump = StartCoroutine(StartJump());
                    canPressJumpBtn = false;
                }
                else
                {
                    directionY = jumpForce;
                    moveDir.x = 0;
                    moveDir.z = 0;
                }
            }
        }
        directionY -= gravity * Time.deltaTime;
        moveDir.y = directionY;
        characterControllerPlayer.Move(moveDir.normalized * speed * Time.deltaTime);

        // -- Aim
        if (canMovement)
        {
            if (GameInputManager.Instance.CurrentProfile.Aim)
            {
                OnAim?.Invoke("On");
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0.0f, cam.eulerAngles.y, 0.0f), 0.1f);
                cinemachineCameraOffsetPlayer.m_Offset = Vector3.MoveTowards(cinemachineCameraOffsetPlayer.m_Offset, new Vector3(0.76f, -0.18f, 9.16f), 0.2f);
                if (!aimImg.activeSelf && cinemachineCameraOffsetPlayer.m_Offset.Equals(new Vector3(0.76f, -0.18f, 9.16f)))
                {
                    aimImg.SetActive(true);
                }
            }
            else
            {
                OnAim?.Invoke("Off");
                transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);
                cinemachineCameraOffsetPlayer.m_Offset = Vector3.MoveTowards(cinemachineCameraOffsetPlayer.m_Offset, Vector3.zero, 0.1f);
                if (aimImg.activeSelf)
                {
                    aimImg.SetActive(false);
                }
            }
        }

        // -- Fire
        if (canMovement)
        {
            if (GameInputManager.Instance.CurrentProfile.Aim)
            {
                if (GameInputManager.Instance.CurrentProfile.Fire)
                {
                    if (Time.time > fireRate + lastShoot)
                    {
                        //FireBullet.GetFromBool(AttackPoint.localPosition, AttackPoint.localRotation);
                        //Debug.DrawRay(startFirePoint.position, (endFirePoint.position - startFirePoint.position), Color.red, distanceBullet);
                        OnShoot?.Invoke(startFirePoint.position, endFirePoint.position);
                        lastShoot = Time.time;
                    }
                }
            }
        }

        // -- Pickup Item
        if (GameInputManager.Instance.CurrentProfile.Pickup)
        {
            OnPickup?.Invoke("StartPickup", canMovement);
            canMovement = false;
        }
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
>>>>>>> Stashed changes
    }

    private void FixedUpdate()
    {
        // Movement
        rbPlayer.MovePosition(rbPlayer.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
