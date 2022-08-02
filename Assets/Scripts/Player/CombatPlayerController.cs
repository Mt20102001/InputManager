using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatPlayerController : MonoBehaviour
{
    [SerializeField] private CinemachineCameraOffset cinemachineCameraOffsetPlayer;
    [SerializeField] private GameObject aimImg;
    [SerializeField] private Transform cam;
    [SerializeField] private float fireRate;
    [SerializeField] private Transform startFirePoint;
    [SerializeField] private Transform endFirePoint;

    public event System.Action<string> OnAim;
    public event System.Action<Vector3, Vector3> OnShoot;

    private bool aimming;
    private float lastShoot;

    public void ClearEvent()
    {
        OnAim = null;
        OnShoot = null;
    }

    public void Aim(bool aim, float angle)
    {
        if (aim)
        {
            OnAim?.Invoke("On");
            aimming = true;
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
            aimming = false;
            transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);
            cinemachineCameraOffsetPlayer.m_Offset = Vector3.MoveTowards(cinemachineCameraOffsetPlayer.m_Offset, Vector3.zero, 0.1f);
            if (aimImg.activeSelf)
            {
                aimImg.SetActive(false);
            }
        }
    }

    public void Shoot(bool shoot)
    {
        if (aimming && shoot)
        {
            if (Time.time > fireRate + lastShoot)
            {
                //FireBullet.GetFromBool(AttackPoint.localPosition, AttackPoint.localRotation);
                //Debug.DrawRay(startFirePoint.position, (endFirePoint.position - startFirePoint.position), Color.red, 100);
                OnShoot?.Invoke(startFirePoint.position, endFirePoint.position);
                lastShoot = Time.time;
            }
        }
    }
}
