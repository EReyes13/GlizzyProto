using UnityEngine;

public class Wallet : MonoBehaviour, IInteractable
{
    public Transform playerInventory;
    public bool canInteract()
    {
        return true;
    }

    public bool Interact(Interactor interactor)
    {
        Debug.Log("Wallet interacted.");

        transform.SetParent(playerInventory);
        gameObject.SetActive(false); // hide the object in the world
        return true;
    }
}