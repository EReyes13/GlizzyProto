using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class Pedestrianmovement : MonoBehaviour
{
    public enum PedestrianState
    {
        Walking,
        WaitingAtCart,
        Dizzy,
        Slipped,
        Recovering
    }

    public PedestrianState currentState = PedestrianState.Walking;

    public float dizzyDuration = 3f;

    public float slipRotationSpeed = 360f;
    public float slippedDuration = 3f;

    public float recoveryDuration = 1f;

    public Transform pointA;
    public Transform pointB;
    public Transform HotDogCart;

    public float cartVisitChance = 0.33f; // 33% chance

    public float cartWaitTime = 7f;

    private bool goingToCart = false;
    private float cartWaitTimer = 0f;

    
    public GameObject[] Valuables;

    private NavMeshAgent agent;
    private Transform currentTarget;

    private float slipCurrentRotation;
    private float slipStartYRotation;
    private bool slipFinishedRotating;

    private Quaternion recoveryStartRotation;
    private Quaternion recoveryTargetRotation;
    private float recoveryTimer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        currentTarget = pointB;
        agent.SetDestination(currentTarget.position);
    }

    void Update()
    {
        if (currentState == PedestrianState.Walking)
        {
            WalkingState();
        }

        if (currentState == PedestrianState.WaitingAtCart)
        {
            WaitingAtCartState();
        }

        if (currentState == PedestrianState.Dizzy)
        {
            DizzyState();
        }

        if (currentState == PedestrianState.Slipped)
        {
            SlippedState();
        }

        if (currentState == PedestrianState.Recovering)
        {
            RecoveringState();
        }

        // Test button H key for when NPC gets dizzy
        if (Keyboard.current != null &&
            Keyboard.current.hKey.wasPressedThisFrame)
        {
            EnterDizzyState();
        }

        // Test button J key for when NPC slips
        if (Keyboard.current != null &&
            Keyboard.current.jKey.wasPressedThisFrame)
        {
            EnterSlippedState();
        }
    }

    // NPC walks between Point A and Point B and has a chance to walk to the hot dog cart
    void WalkingState()
    {
        if (!agent.pathPending &&
            agent.remainingDistance <= agent.stoppingDistance)
        {
            // NPC has arrived at the hot dog cart
            if (goingToCart)
            {
                agent.isStopped = true;

                cartWaitTimer = 0f;
                currentState = PedestrianState.WaitingAtCart;

                return;
            }

            // Random chance to visit the hot dog cart
            if (HotDogCart != null &&
                Random.value < cartVisitChance)
            {
                goingToCart = true;

                currentTarget = HotDogCart;
                agent.SetDestination(currentTarget.position);

                return;
            }

            // Continue walking state Point A to B if not visiting the hot dog cart
            if (currentTarget == pointA)
            {
                currentTarget = pointB;
            }
            else
            {
                currentTarget = pointA;
            }

            agent.SetDestination(currentTarget.position);
        }
    }

    // NPC waits at the hot dog cart
    void WaitingAtCartState()
    {
        cartWaitTimer += Time.deltaTime;

        if (cartWaitTimer >= cartWaitTime)
        {
            cartWaitTimer = 0f;
            goingToCart = false;

            currentState = PedestrianState.Walking;

            agent.isStopped = false;

            // After visiting the cart, resume walking state
            currentTarget = pointB;
            agent.SetDestination(currentTarget.position);
        }
    }

    // Checks to see if NPC is in Dizzy state or not
    public void EnterDizzyState()
    {
        if (currentState != PedestrianState.Walking)
            return;

        currentState = PedestrianState.Dizzy;

        agent.isStopped = true;

        // Drop one random valuable
        DropItem();

        Invoke(nameof(ExitDizzyState), dizzyDuration);
    }

    // NPC will be dizzy, rotating the X and Z axis
    void DizzyState()
    {
        float xRotation = Mathf.Sin(Time.time * 5f) * 25f;
        float zRotation = Mathf.Cos(Time.time * 5f) * 25f;

        transform.localRotation = Quaternion.Euler(
            xRotation,
            transform.localEulerAngles.y,
            zRotation
        );
    }

    void ExitDizzyState()
    {
        StartRecovering();
    }

    // Checks to see if NPC is in slipped state or not
    public void EnterSlippedState()
    {
        if (currentState != PedestrianState.Walking)
            return;

        currentState = PedestrianState.Slipped;

        agent.isStopped = true;

        // Drop one random valuable when the NPC slips
        DropItem();

        slipCurrentRotation = 0f;

        slipStartYRotation = transform.localEulerAngles.y;

        slipFinishedRotating = false;
    }

    void SlippedState()
    {
        if (!slipFinishedRotating)
        {
            // Perform the full flip
            slipCurrentRotation += slipRotationSpeed * Time.deltaTime;

            if (slipCurrentRotation >= 450f)
            {
                slipCurrentRotation = 450f;
                slipFinishedRotating = true;

                // End the flip lying horizontally
                transform.localRotation = Quaternion.Euler(
                    90f,
                    slipStartYRotation,
                    0f
                );

                // Stay lying down before recovering
                Invoke(nameof(ExitSlippedState), slippedDuration);

                return;
            }

            transform.localRotation = Quaternion.Euler(
                slipCurrentRotation,
                slipStartYRotation,
                0f
            );
        }
        else
        {
            // Stay horizontal while waiting to recover
            transform.localRotation = Quaternion.Euler(
                90f,
                slipStartYRotation,
                0f
            );
        }
    }

    void ExitSlippedState()
    {
        StartRecovering();
    }

    // Starts the recovery process rotating the NPC back up
    void StartRecovering()
    {
        currentState = PedestrianState.Recovering;

        recoveryStartRotation = transform.localRotation;

        recoveryTargetRotation = Quaternion.Euler(
            0f,
            transform.localEulerAngles.y,
            0f
        );

        recoveryTimer = 0f;
    }

    // Rotates the NPC back upright and makes them walk again
    void RecoveringState()
    {
        recoveryTimer += Time.deltaTime;

        float progress = recoveryTimer / recoveryDuration;

        transform.localRotation = Quaternion.Slerp(
            recoveryStartRotation,
            recoveryTargetRotation,
            progress
        );

        if (progress >= 1f)
        {
            transform.localRotation = recoveryTargetRotation;

            currentState = PedestrianState.Walking;

            agent.isStopped = false;

            agent.SetDestination(currentTarget.position);
        }
    }

    // Creates a random valuable item above the NPC from the array
    void DropItem()
    {
        if (Valuables != null && Valuables.Length > 0)
        {
            
            int randomIndex = Random.Range(0, Valuables.Length);

            GameObject randomValuable = Valuables[randomIndex];

            
            Instantiate(
                randomValuable,
                transform.position + Vector3.up,
                Quaternion.identity
            );
        }
    }
}