using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class Pedestrianmovement : MonoBehaviour
{
    public enum PedestrianState
    {
        Walking,
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

    
    public GameObject Valuable;

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

        // test button H key for when NPC gets dizzy
        if (Keyboard.current != null &&
            Keyboard.current.hKey.wasPressedThisFrame)
        {
            EnterDizzyState();
        }

        // test button J key for when NPC slips
        if (Keyboard.current != null &&
            Keyboard.current.jKey.wasPressedThisFrame)
        {
            EnterSlippedState();
        }
    }

    // NPC will walk from point A to Point B in a loop

    void WalkingState()
    {
        if (!agent.pathPending &&
            agent.remainingDistance <= agent.stoppingDistance)
        {
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

    // Checks to see if NPC is in Dizzy state or not

    void EnterDizzyState()
    {
        if (currentState != PedestrianState.Walking)
            return;

        currentState = PedestrianState.Dizzy;

        agent.isStopped = true;

        
        DropItem();

        Invoke(nameof(ExitDizzyState), dizzyDuration);
    }

    // NPC will be dizzy, rotating the x and z axis
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

    void EnterSlippedState()
    {
        if (currentState != PedestrianState.Walking)
            return;

        currentState = PedestrianState.Slipped;

        agent.isStopped = true;

        // Drop an item when the NPC slips
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
            // Stay in horizontal position while waiting to recover to vertical position
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

        // Remember EXACTLY where the NPC currently is
        recoveryStartRotation = transform.localRotation;

        recoveryTargetRotation = Quaternion.Euler(
            0f,
            transform.localEulerAngles.y,
            0f
        );

        recoveryTimer = 0f;
    }

    // Rotates the NPC back to an upright position over time and then makes them walk again
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

    // Creates one placeholder item above the NPC
    void DropItem()
    {
        
        if (Valuable != null)
        {
            Instantiate(
                Valuable,
                transform.position + Vector3.up,
                Quaternion.identity
            );
        }
    }
}