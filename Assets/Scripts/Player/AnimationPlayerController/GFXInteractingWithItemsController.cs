using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using System.Linq;

public class GFXInteractingWithItemsController : MonoBehaviour
{
    [SerializeField] private Animator animatorPlayer;
    [SerializeField] private InteractingWithItemsPlayerController interactingWithItems;
    [SerializeField] private MultiAimConstraint aimWhenPickupItems;
    [SerializeField] private TwoBoneIKConstraint twoBoneIKConstraintPlayerForHandPickup;
    [SerializeField] private Transform RightHand;
    public float speed;
    List<Transform> transRightHand = new List<Transform>();
    List<Transform> transRightHandHoldItem = new List<Transform>();

    private float weight;
    // Start is called before the first frame update
    private void Start()
    {
        interactingWithItems.ClearEvent();
        weight = 0;
        interactingWithItems.OnPickUp += PickupCallback;
    }

    private void PickupCallback()
    {
        animatorPlayer.SetTrigger("Pickup");
        GetHand(RightHand, transRightHand);
    }

    public void StartSetPickup()
    {
        weight = 1;
        GetHand(interactingWithItems.handOnItem, transRightHandHoldItem);
    }

    public void EndSetPickup()
    {
        weight = 0;
        interactingWithItems.SetItemOnHand();
    }

    public void ResetPickup()
    {
        interactingWithItems.ResetTransformPointPickupOfPlayer();
    }

    private void Update()
    {
        twoBoneIKConstraintPlayerForHandPickup.weight = Mathf.MoveTowards(twoBoneIKConstraintPlayerForHandPickup.weight, weight, speed * Time.deltaTime);
        //aimWhenPickupItems.weight = Mathf.MoveTowards(aimWhenPickupItems.weight, weight, 1);
    }

    private void LateUpdate()
    {
        SetHandWhenHoldItem(transRightHand, transRightHandHoldItem);
    }

    private void GetHand(Transform Hand, List<Transform> transRightHand)
    {
        transRightHand.Clear();
        foreach (Transform childOfHand in Hand.GetComponentsInChildren<Transform>())
        {
            if (childOfHand.gameObject.tag != "ItemPack")
            {
                transRightHand.Add(childOfHand);
            }
        }
    }

    private void SetHandWhenHoldItem(List<Transform> transRightHandChoose, List<Transform> transRightHandHoldItems)
    {
        for (int i = 0; i < transRightHandHoldItems.Count; i++)
        {
            transRightHandChoose[i].localPosition = transRightHandHoldItems[i].localPosition;
            transRightHandChoose[i].localRotation = transRightHandHoldItems[i].localRotation;
        }
    }
}
