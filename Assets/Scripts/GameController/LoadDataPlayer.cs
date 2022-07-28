using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadDataPlayer : MonoBehaviour
{
    public static LoadDataPlayer Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<LoadDataPlayer>();
            return instance;
        }
    }

    private static LoadDataPlayer instance;

    public float moveSpeedPlayer;
    public float runSpeedPlayer;
    public float jumpForcePlayer;
    public Vector3 moveDirPlayer;
}
