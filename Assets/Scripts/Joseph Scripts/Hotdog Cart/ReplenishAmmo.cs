using UnityEngine;

public class ReplenishAmmo : MonoBehaviour
{
[Header("Replenish Settings")]
    public float replenishRate = 2f; // how much time it takes to replenish ammo 
    public int replenishAmount = 5; // how much ammo the player gets

    public bool isReplenishing;

    public HotdogAmmo hotdogAmmo; // reference to the HotdogAmmo script

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isReplenishing = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isReplenishing == true) // countdown starts when player is in range of the hotdog cart
        {
            replenishRate -= Time.deltaTime;
        }
        if(isReplenishing == false) // if player leaves the hotdog cart, replenish rate resets to 5 seconds
        {
            replenishRate = 2f;
        }

        if(replenishRate <= 0f) // when replenish rate reaches 0, the player gets ammo and replenish rate resets to 5 seconds
        {
            Debug.Log("Replenished Ammo!");
            replenishRate = 2f;

            if(hotdogAmmo != null) // checks if the HotdogAmmo script is attached to the player
            {
                hotdogAmmo.Ammo(replenishAmount); // Adds 5 ammo to the player from the HotdogAmmo script
            }
            else
            {
                Debug.LogError("HotdogAmmo script not found on player!");
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        hotdogAmmo = other.GetComponent<HotdogAmmo>(); // gets the HotdogAmmo script from the player
        if(other.gameObject.CompareTag("Player"))
        {
            isReplenishing = true;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            isReplenishing = false;
        }
    }
}
