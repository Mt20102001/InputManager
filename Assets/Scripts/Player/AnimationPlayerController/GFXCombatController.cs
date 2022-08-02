using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class GFXCombatController : MonoBehaviour
{
    [SerializeField] private Animator animatorPlayer;
    [SerializeField] private CombatPlayerController combatPlayerController;
    [SerializeField] private MultiAimConstraint headPlayer;
    [SerializeField] private MultiAimConstraint DirPlayer;
    [SerializeField] private MultiAimConstraint leftHandPlayer;
    [SerializeField] private TwoBoneIKConstraint twoBoneIKConstraintPlayerForHandNoWeapon;
    [SerializeField] private TwoBoneIKConstraint twoBoneIKConstraintPlayerForHandPickup;
    [SerializeField] private ParticleSystem muzzleEffect;
    [SerializeField] private TrailRenderer bullet;
    [SerializeField] private LayerMask Mask;

    private void Start()
    {
        combatPlayerController.ClearEvent();
        combatPlayerController.OnAim += AimCallback;
        combatPlayerController.OnShoot += ShootCallback;
    }

    private void AimCallback(string status)
    {
        if (status.Equals("On"))
        {
            animatorPlayer.SetBool("Aimming", true);

            headPlayer.weight = Mathf.MoveTowards(headPlayer.weight, 1, 0.1f);
            DirPlayer.weight = Mathf.MoveTowards(DirPlayer.weight, 1, 0.1f);
            leftHandPlayer.weight = Mathf.MoveTowards(leftHandPlayer.weight, 1, 0.1f);
            twoBoneIKConstraintPlayerForHandNoWeapon.weight = Mathf.MoveTowards(twoBoneIKConstraintPlayerForHandNoWeapon.weight, 1, 0.01f);
            animatorPlayer.SetLayerWeight(1, 1);
        }
        if (status.Equals("Off"))
        {
            animatorPlayer.SetBool("Aimming", false);

            headPlayer.weight = Mathf.MoveTowards(headPlayer.weight, 0, 0.1f);
            DirPlayer.weight = Mathf.MoveTowards(DirPlayer.weight, 0, 0.1f);
            leftHandPlayer.weight = Mathf.MoveTowards(leftHandPlayer.weight, 0, 0.1f);
            twoBoneIKConstraintPlayerForHandNoWeapon.weight = Mathf.MoveTowards(twoBoneIKConstraintPlayerForHandNoWeapon.weight, 0, 0.1f);
            animatorPlayer.SetLayerWeight(1, 0);
        }

    }
    private void ShootCallback(Vector3 startFirePoint, Vector3 endFirePoint)
    {
        muzzleEffect.Play();
        animatorPlayer.SetTrigger("Shooting");
        TrailRenderer trail = Instantiate(bullet, startFirePoint, Quaternion.identity);
        StartCoroutine(SpawnTrailBullet(trail, endFirePoint));
        /*if (Physics.Raycast(startFirePoint, dir, out RaycastHit hit, float.MaxValue, Mask))
        {
        }*/
    }

    private IEnumerator SpawnTrailBullet(TrailRenderer trail, Vector3 hit)
    {
        float time = 0;
        Vector3 startPoint = trail.transform.position;

        while (time < 1)
        {
            trail.transform.position = Vector3.Lerp(startPoint, hit, time);
            time += 40 * Time.deltaTime / trail.time;

            yield return null;
        }
        Destroy(trail.gameObject);
    }
}
