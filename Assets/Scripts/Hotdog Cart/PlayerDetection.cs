using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDetection : MonoBehaviour
{
[Header("Bools")]
    public bool CanInteract;

[Header("Interaction Settings")] 
    public InputActionReference interact;

[Header("UI Settings")]
    public GameObject interactionUI;
    public GameObject upgradeUI;

[Header("Player Positioning Settings")]
    public Transform player;
    public Transform playerSide;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CanInteract = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(CanInteract == true) // when player is in range of the hotdog cart, show the interaction UI
        {
            interactionUI.SetActive(true);
        }
        else
        {
            interactionUI.SetActive(false);
        }

        if(CanInteract == true && interact.action.triggered) // Move player behind cart if pressed E, Show 2 options to use the hotdog cart, and move the player to the side of the hotdog cart
        {
            Debug.Log("I Interacted!");
            MovePlayer(); 
        }
    }

    public void Interact(InputAction.CallbackContext cntxt) // When player presses E, this function is called
    {
        if(cntxt.started)
        {
            Debug.Log("I Interacted!");
        }
    }

    public void MovePlayer() // move the player to the side of the hotdog cart when they interact with it
    {
        player.position = playerSide.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            CanInteract = true;
        }    
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            CanInteract = false;
        }    
    }
}
