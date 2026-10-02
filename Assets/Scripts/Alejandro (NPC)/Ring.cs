using UnityEngine;

public class Ring : MonoBehaviour, IInteractable
{
    public bool collectedRing = false;

    public bool canInteract()
    {
        return true;
    }

    public bool Interact(Interactor interactor)
    {
        Debug.Log("Ring interacted.");

        collectedRing = true; // set hasRing to true when the player interacts with the ring

        BoxCollider boxCollider = GetComponent<BoxCollider>(); // get the BoxCollider component attached to the phone

        boxCollider.enabled = false; // disable the box collider so the player cannot interact with the phone again

        return true;
    }
}
