using UnityEngine;

public class SellMechanic : MonoBehaviour
{
    public bool canSell = false;
    public bool readyToSell = false;

    PlayerInventory playerInventory; // reference to the PlayerInventory script
    SmartPhone smartPhone; // reference to the SmartPhone script
    Ring ring; // reference to the Ring script
    Wallet wallet; // reference to the Wallet script

    public TMPro.TMP_Text moneyEarnedText; // reference to the TextMeshProUGUI component that displays the money earned

    public int moneyEarned;

    public int phonePrice = 100; // price of the phone
    public int ringPrice = 50; // price of the ring
    public int walletPrice = 200; // price of the wallet

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerInventory = FindFirstObjectByType<PlayerInventory>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerInventory.hasPhone == true && readyToSell == true) // check if the player has the phone to sell it
        {
            canSell = true;
            smartPhone = FindFirstObjectByType<SmartPhone>();
    
            if(canSell == true)
            {
                PhoneSold();
                Debug.Log("Player sold phone.");
            }
            else
            {
                Debug.Log("Player cannot sell the phone.");
            }
        }
        else
        {
            canSell = false;
        }

        if(playerInventory.hasRing == true && readyToSell == true) // check if the player has the ring to sell it
        {
            canSell = true;
            ring = FindFirstObjectByType<Ring>();
    
            if(canSell == true)
            {
                RingSold();
                Debug.Log("Player sold ring.");
            }
            else
            {
                Debug.Log("Player cannot sell the ring.");
            }
        }
        else
        {
            canSell = false;
        }

        if(playerInventory.hasWallet == true && readyToSell == true) // check if the player has the wallet to sell it
        {
            canSell = true;
            wallet = FindFirstObjectByType<Wallet>();
    
            if(canSell == true)
            {
                WalletSold();
                Debug.Log("Player sold wallet.");
            }
            else
            {
                Debug.Log("Player cannot sell the wallet.");
            }
        }
        else
        {
            canSell = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player")) // check if the player is in range of the hotdog cart
        {
            readyToSell = true; 
        }    
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            readyToSell = false;
        }  
    }

    public void PhoneSold() // when the player sells the phone, this function is called
    {
        playerInventory.hasPhone = false; // set hasPhone to false when the player sells the phone
        smartPhone.collectedPhone = false; // set hasPhone to false when the player sells the phone
        moneyEarned += phonePrice; // add the phone price to the money earned
        moneyEarnedText.text = "$" + moneyEarned.ToString(); // update the money earned text
    }

    public void RingSold() // when the player sells the ring, this function is called
    {
        playerInventory.hasRing = false; // set hasRing to false when the player sells the ring
        ring.collectedRing = false; // set collectedRing to false when the player sells the ring
        moneyEarned += ringPrice; // add the ring price to the money earned
        moneyEarnedText.text = "$" + moneyEarned.ToString(); // update the money earned text
    }

    public void WalletSold() // when the player sells the wallet, this function is called
    {
        playerInventory.hasWallet = false; // set hasWallet to false when the player sells the wallet
        wallet.collectedWallet = false; // set collectedWallet to false when the player sells the wallet
        moneyEarned += walletPrice; // add the wallet price to the money earned
        moneyEarnedText.text = "$" + moneyEarned.ToString(); // update the money earned text
    }
}
