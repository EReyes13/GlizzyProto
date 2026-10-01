// using System.Numerics;

using System;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] InputActionAsset input;

    [SerializeField] Transform cameraDirection;
    Rigidbody rb;
    Vector2 moveInput;

    Vector2 mouseInput;

    Vector3 newCameraRotation;

    float vert;

    public float movemult;

    public float Sensitivity;

    public Transform Camera;

    Vector3 playerMovement;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        newCameraRotation = Camera.localRotation.eulerAngles;

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      
            vert -= 10*Time.deltaTime;
        
        // Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;
        newCameraRotation.x = Mathf.Clamp(newCameraRotation.x,-90,90);
        // rb.linearVelocity = move*movemult;
        transform.localRotation = Quaternion.Euler(newCameraRotation *Sensitivity);
        playerMovement = cameraDirection.TransformDirection(new Vector3(moveInput.x,0,moveInput.y)).normalized;
        
       
        rb.linearVelocity =new Vector3(playerMovement.x* movemult,Mathf.Clamp(vert,-3,3),playerMovement.z* movemult);
         Debug.Log(rb.linearVelocity.y);
    }
    public void Jump(InputAction.CallbackContext cntxt)
    {
        if(cntxt.started)
        {
            Debug.Log("Jump");
           vert = 5;
        }
    }

    public void Movement(InputAction.CallbackContext cntxt)
    {
        moveInput = cntxt.ReadValue<Vector2>();
        
    }

    public void MouseLook(InputAction.CallbackContext cntx)
    {
        mouseInput = cntx.ReadValue<Vector2>();
        newCameraRotation.x -= mouseInput.y;
        newCameraRotation.y += mouseInput.x;
    }
}
