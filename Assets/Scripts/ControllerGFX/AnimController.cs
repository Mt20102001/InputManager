using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class AnimController : MonoBehaviour
{
    [SerializeField] private PlayerController PlayerController;
    [SerializeField] private Animator animatorPlayer;
    [SerializeField] private Rig rigPlayer;
    [SerializeField] private TwoBoneIKConstraint twoBoneIKConstraintPlayerForHandNoWeapon;
    [SerializeField] private ParticleSystem muzzleEffect;
    [SerializeField] private TrailRenderer bullet;
    [SerializeField] private LayerMask Mask;

    private void Start()
    {
        PlayerController.ClearEvent();
        PlayerController.OnMove += MoveCallback;
        PlayerController.OnRun += RunCallback;
        PlayerController.OnJump += JumpCallback;
        PlayerController.OnStatusJump += StatusJumpCallback;
        PlayerController.OnAim += AimCallback;
        PlayerController.OnShoot += ShootCallback;
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

    private void AimCallback(string status)
    {
        if (status.Equals("On"))
        {
            animatorPlayer.SetBool("Aimming", true);
            animatorPlayer.SetFloat("Aim", 1);
            rigPlayer.weight = Mathf.MoveTowards(rigPlayer.weight, 1, 0.1f);
            twoBoneIKConstraintPlayerForHandNoWeapon.weight = Mathf.MoveTowards(twoBoneIKConstraintPlayerForHandNoWeapon.weight, 1,0.01f);
            animatorPlayer.SetLayerWeight(1, 1);
        }
        if (status.Equals("Off"))
        {
            animatorPlayer.SetBool("Aimming", false);
            animatorPlayer.SetFloat("Aim", 0);
            rigPlayer.weight = Mathf.MoveTowards(rigPlayer.weight, 0, 0.1f);
            twoBoneIKConstraintPlayerForHandNoWeapon.weight = Mathf.MoveTowards(twoBoneIKConstraintPlayerForHandNoWeapon.weight, 0, 0.1f);
            animatorPlayer.SetLayerWeight(1, 0);
        }
        
    }

    private void ShootCallback(Vector3 startFirePoint, Vector3 dir, float dis)
    {
        muzzleEffect.Play();
        animatorPlayer.SetTrigger("Shooting");
        if (Physics.Raycast(startFirePoint, dir, out RaycastHit hit, float.MaxValue, Mask))
        {
            TrailRenderer trail = Instantiate(bullet, startFirePoint, Quaternion.identity);
            StartCoroutine(SpawnTrailBullet(trail, hit));
        }
    }

    private IEnumerator SpawnTrailBullet(TrailRenderer trail, RaycastHit hit)
    {
        float time = 0;
        Vector3 startPoint = trail.transform.position;

        while (time < 1)
        {
            trail.transform.position = Vector3.Lerp(startPoint, hit.point, time);
            time += 40*Time.deltaTime / trail.time;

            yield return null;
        }
        Destroy(trail.gameObject);
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
