using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItemZone : MonoBehaviour
{
    [SerializeField] private InteractingWithItemsPlayerController interactingWithItems;
    [SerializeField] private Transform pickupOfItem;
    [SerializeField] private GameObject Item;
    [SerializeField] private Transform handOnItem;

    void Start()
    {
        interactingWithItems = FindObjectOfType<InteractingWithItemsPlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.LogError("Vao");
            interactingWithItems.pickupOfItem = pickupOfItem;
            interactingWithItems.Item = Item;
            interactingWithItems.handOnItem = handOnItem;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.LogError("Ra");
            interactingWithItems.ClearPickupTarget();
        }
    }
}
