using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractingWithItemsPlayerController : MonoBehaviour
{
    [SerializeField] private Transform pickupPointOfPlayer;
    [SerializeField] private Transform pickupHandOfPlayer;
    [SerializeField] private Transform pointHoldItemOnHandPlayer;
    public Transform pickupOfItem;
    public GameObject Item;
    public Transform handOnItem;
    public bool pickupDone, isHaveItem;

    private bool enablePickup;

    public event System.Action OnPickUp;

    public void ClearEvent()
    {
        OnPickUp = null;   
    }

    public void Initialize()
    {
        pickupDone = true;
        isHaveItem = false;
    }

    private void Update()
    {
        if (pickupOfItem != null && Item != null && handOnItem != null)
        {
            enablePickup = true;
        }
        else
        {
            enablePickup = false;
        }
    }

    public void PickUpItem(bool pickup)
    {
        if (pickup && enablePickup && !isHaveItem)
        {
            pickupPointOfPlayer.position = pickupOfItem.position;
            pickupPointOfPlayer.rotation = pickupOfItem.rotation;

            OnPickUp?.Invoke();
            pickupDone = false;
        }
        if (pickup && isHaveItem)
        {
            Item.transform.SetParent(null);
            Item.GetComponent<Rigidbody>().isKinematic = false;
            isHaveItem = false;
            Item = null;
        }
    }

    public void ResetTransformPointPickupOfPlayer()
    {
        if (enablePickup && !isHaveItem)
        {
            pickupPointOfPlayer.position = pickupHandOfPlayer.position;
            pickupPointOfPlayer.rotation = pickupHandOfPlayer.rotation;
            pickupDone = true;
            isHaveItem = true;
            ClearPickupTarget();
        }
    }

    public void SetItemOnHand()
    {
        if (enablePickup && !isHaveItem)
        {
            Item.transform.SetParent(pointHoldItemOnHandPlayer);
            Item.transform.localPosition = Vector3.zero;
            Item.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    public void ClearPickupTarget()
    {
        pickupOfItem = null;
        handOnItem = null;
    }
}