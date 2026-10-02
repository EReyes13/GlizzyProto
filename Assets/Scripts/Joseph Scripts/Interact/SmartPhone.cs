using UnityEngine;

public class SmartPhone : MonoBehaviour, IInteractable
{
    public bool collectedPhone = false;

    public bool canInteract()
    {
        return true;
    }

    public bool Interact(Interactor interactor) // When the player interacts with the phone, this function is called
    {
        Debug.Log("SmartPhone interacted.");

        collectedPhone = true; // set hasPhone to true when the player interacts with the phone

        BoxCollider boxCollider = GetComponent<BoxCollider>(); // get the BoxCollider component attached to the phone

        boxCollider.enabled = false; // disable the box collider so the player cannot interact with the phone again
        return true;
    }    
}
