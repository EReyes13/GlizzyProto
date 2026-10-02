using UnityEngine;

public class Ring : MonoBehaviour, IInteractable
{
    public Transform playerInventory;

    public bool canInteract()
    {
        return true;
    }

    public bool Interact(Interactor interactor)
    {
        Debug.Log("Ring interacted.");

        transform.SetParent(playerInventory);
        gameObject.SetActive(false);

        return true;
    }
}
