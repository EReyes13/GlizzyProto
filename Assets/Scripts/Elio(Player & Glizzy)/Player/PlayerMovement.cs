// using System.Numerics;

using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] InputActionAsset input;
    Rigidbody rb;
    Vector2 moveInput;

    Vector2 mouseInput;

    Vector3 newCameraRotation;

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
        newCameraRotation.x = Mathf.Clamp(newCameraRotation.x,-90,90);
        rb.linearVelocity = new Vector3(moveInput.x * movemult, rb.linearVelocity.y,moveInput.y * movemult);
        transform.localRotation = Quaternion.Euler(newCameraRotation *Sensitivity);
        
     
    }
    public void Jump(InputAction.CallbackContext cntxt)
    {
        if(cntxt.started)
        {
            Debug.Log("Jump");
            rb.AddForce(new Vector3(0,200,0));
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
