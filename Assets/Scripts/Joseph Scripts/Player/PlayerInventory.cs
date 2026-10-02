using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    SmartPhone smartPhone; // reference to the SmartPhone script
    Ring ring; // reference to the Ring script
    Wallet wallet; // reference to the Wallet script

    public bool hasPhone = false; // variable to track if the player has collected the phone
    public bool hasRing = false; // variable to track if the player has collected the ring
    public bool hasWallet = false; // variable to track if the player has collected the wallet

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        smartPhone = FindFirstObjectByType<SmartPhone>(); // find the SmartPhone script in the scene
        ring = FindFirstObjectByType<Ring>(); // find the Ring script in the scene
        wallet = FindFirstObjectByType<Wallet>(); // find the Wallet script in the scene
        
        if (smartPhone != null && smartPhone.collectedPhone) // check if the player has collected the phone
        {
            hasPhone = true;
        }

        if (ring != null && ring.collectedRing) // check if the player has collected the ring
        {
            hasRing = true;
        }

        if (wallet != null && wallet.collectedWallet) // check if the player has collected the wallet
        {
            hasWallet = true;
        }
    }
}
