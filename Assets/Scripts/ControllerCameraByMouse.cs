using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ControllerCameraByMouse : MonoBehaviour
{
    [SerializeField] private CinemachineFreeLook CameraPlayer;

    void Update()
    {
        CameraPlayer.m_XAxis.m_InputAxisName = GameInputManager.Instance.CurrentProfile.InputAxisNameCurrentOfXForCamera;
        CameraPlayer.m_YAxis.m_InputAxisName = GameInputManager.Instance.CurrentProfile.InputAxisNameCurrentOfYForCamera;
    }
}
