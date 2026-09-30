using UnityEngine;

public class HotdogAmmo : MonoBehaviour
{
[Header("Ammo Settings")]
    public int currentAmmo = 0;    
    public int maxAmmo = 15; // max ammo the player can hold
    public GameObject hotdogAmmo;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentAmmo = Mathf.Clamp(currentAmmo, 0, maxAmmo); // clamps the current ammo to not go over the max ammo
    }

    public void Ammo(int amount) // this function is called when the player picks up ammo
    {
        currentAmmo = Mathf.Clamp(currentAmmo + amount, 0, maxAmmo);
    }
}
