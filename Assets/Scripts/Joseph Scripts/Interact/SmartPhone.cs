using UnityEngine;

public class SmartPhone : MonoBehaviour, IInteractable
{
    public Transform playerInventory;
    public bool canInteract()
    {
        return true;
    }

    public bool Interact(Interactor interactor)
    {
        Debug.Log("SmartPhone interacted.");

        transform.SetParent(playerInventory);
        gameObject.SetActive(false); // hide the object in the world
        return true;
    }
}
