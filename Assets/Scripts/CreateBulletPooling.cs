using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBulletPooling : MonoBehaviour
{
    // public static CreateBulletPooling Instance
    // {
    //     get
    //     {
    //         if (instance == null)
    //             instance = FindObjectOfType<CreateBulletPooling>();
    //         return instance;
    //     }
    // }

    // private static CreateBulletPooling instance;
    [SerializeField] private GameObject bullet;
    private List<GameObject> bulletPool = new List<GameObject>();

    public GameObject GetFromBool(Vector3 position, Quaternion rotation)
    {
        for (int i = 0; i < bulletPool.Count; i++)
        {
            var o = bulletPool[i];
            if (!o.activeInHierarchy)
            {
                o.SetActive(true);
                o.transform.SetPositionAndRotation(position, rotation);
                return o;
            }
        }
        GameObject b = Instantiate(bullet, position, rotation);
        b.transform.SetParent(this.transform);
        bulletPool.Add(b);
        return b;
    }
}
