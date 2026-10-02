using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThrowHotdog : MonoBehaviour
{    
    public InputActionReference Throw;

    public int useHotdog = 1;

[Header("Throw Posiition")]
    public Transform throwPosition;
    public GameObject hotdogPrefab;

    public HotdogAmmo hotdogAmmo; // reference to the HotdogAmmo script

    //Elio var
     int mult;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hotdogAmmo = GetComponent<HotdogAmmo>();
    }

    // Update is called once per frame
    void Update()
    {
        if(hotdogAmmo.currentAmmo > 0 && Throw.action.triggered) // if there is ammo and player presses T throw hotdog
        {
            ThrowHotDog();
            hotdogAmmo.UseAmmo(useHotdog); // take away one ammo from currentAmmo 
        }
        
    }

    public void Throwing(InputAction.CallbackContext cntxt) // When player presses T, this function is called
    {
        if(cntxt.started)
        {
            Debug.Log("I Threw A hotdog!");
        }
    } 

    public void ThrowHotDog() // Throw hotdog
    {
        mult = UnityEngine.Random.Range(0,10);
        for(int i = 0; i < mult;i++ ){
        GameObject Glizzy = Instantiate(hotdogPrefab, throwPosition.position , throwPosition.rotation);

        GlickThrowScript Glick = Glizzy.GetComponent<GlickThrowScript>();
        if(Glick!= null)
        {
            Glick.Sling(throwPosition.position, throwPosition.rotation);
        }
        }
    }
}
