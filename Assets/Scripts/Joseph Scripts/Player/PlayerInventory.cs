using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    SmartPhone smartPhone; // reference to the SmartPhone script

    public bool hasPhone = false; // variable to track if the player has collected the phone

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        smartPhone = FindFirstObjectByType<SmartPhone>(); // find the SmartPhone script in the scene

        if (smartPhone != null && smartPhone.collectedPhone) // check if the player has collected the phone
        {
            hasPhone = true;
        }

    }
}
