using UnityEngine;

public class Wallet : MonoBehaviour, IInteractable
{
    public bool collectedWallet = false;
    public bool canInteract()
    {
        return true;
    }

    public bool Interact(Interactor interactor)
    {
        Debug.Log("Wallet interacted.");

        collectedWallet = true; // set hasWallet to true when the player interacts with the wallet

        BoxCollider boxCollider = GetComponent<BoxCollider>(); // get the BoxCollider component attached to the phone

        boxCollider.enabled = false; // disable the box collider so the player cannot interact with the phone again
        return true;
    }
}