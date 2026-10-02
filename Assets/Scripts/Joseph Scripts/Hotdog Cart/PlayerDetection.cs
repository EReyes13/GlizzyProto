using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDetection : MonoBehaviour
{
[Header("Bools")]
    public bool CanInteract;
    public bool CanPressUpgradeButton;

[Header("Interaction Settings")] 
    public InputActionReference interact;
    public InputActionReference upgradeButtonKey;

[Header("UI Settings")]
    public GameObject interactionUI;
    public GameObject upgradeButton;

[Header("Player Positioning Settings")]
    public Transform player;
    public Transform playerSide;    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CanInteract = false;
        CanPressUpgradeButton = false;
    }

    // Update is called once per frame
    void Update() // this whole part work but will not show for prototype, will show for actual build
    {   
        // if(CanInteract == true) // when player is in range of the hotdog cart, show the interact UI
        // {
        //     interactionUI.SetActive(true);
        // }
        // else
        // {
        //     interactionUI.SetActive(false);
        // } 

        // if(CanInteract == true && interact.action.triggered) // Move player behind cart if pressed E, Show 2 options to use the hotdog cart, and move the player to the side of the hotdog cart
        // {
        //     Debug.Log("I Interacted!");
        //     MovePlayer(); 
        //     interactionUI.SetActive(false); // hide the interact UI when player presses E
        //     CanInteract = false;
        // }
        // if(CanPressUpgradeButton == true && CanInteract == true && upgradeButtonKey.action.triggered) // testing out
        // {
        //     Debug.Log("I Pressed Button!");
             
        // }
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
        if(other.gameObject.CompareTag("Player")) // for hotdot cart
        {
            CanInteract = true; 
        }    

        if(other.gameObject.CompareTag("Player")) // for upgrade button (this is here temperarily)
        {
            CanPressUpgradeButton = true;
        }  
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            CanInteract = false;
        }  

        if(other.gameObject.CompareTag("Player"))
        {
            CanPressUpgradeButton = false;
        }   
    }
}
