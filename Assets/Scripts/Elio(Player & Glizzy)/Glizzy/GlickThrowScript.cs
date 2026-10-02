using UnityEngine;

public class GlickThrowScript : MonoBehaviour
{
    float lifespan = 10;
    Rigidbody rb;

    int speed =10;

    [SerializeField]bool grounded;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        lifespan -= Time.deltaTime;

        if(lifespan <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Sling(Vector3 pos, Quaternion rotate)
    {
        transform.position = pos;
        transform.rotation = rotate;
         rb.AddForce((transform.forward*100)*speed);
    }

    public void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Floor"))
        {
            grounded = true;
        }
        Pedestrianmovement npc = other.gameObject.GetComponent<Pedestrianmovement>();
        if(npc!=null)
        {
            if(grounded)
            {
                npc.EnterSlippedState();
            }
            else
            {
                Rigidbody knock =other.gameObject.GetComponent<Rigidbody>();
                if(rb!= null)
                {
                    knock.AddForce((transform.forward += new Vector3(0,1,0))*10);
                }
                npc.EnterSlippedState();

            }
        }
    }

    public void OCollisionStay(Collision other)
    {
        if(other.gameObject.CompareTag("Floor"))
        {
            grounded = true;
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        grounded = false;
    }

}
