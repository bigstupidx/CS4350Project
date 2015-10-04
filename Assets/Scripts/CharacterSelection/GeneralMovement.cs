using UnityEngine;
using System.Collections;

public class GeneralMovement : MonoBehaviour {

    public static float speed = 3f;
    public static float closeToDestinationThreshold = 0.1f;

    Vector3 destination;

    // Movement Controls
    bool isWalking;
    Vector3 lastMovement;
    int noOfFramesNotMoving = 0;
    int maxNoOfFramesNotMoving = 3;
    float noMovementThreshold = 0.005f;

    Rigidbody myRigidbody;

    // Use this for initialization
    void Start () {
        myRigidbody = GetComponent<Rigidbody>();
    }

    public bool IsMoving()
    {
        return isWalking;
    }

    public void MoveTo(Vector3 newDest)
    {
        destination = newDest;
        isWalking = true;
    }

    void FixedUpdate()
    {
        if (isWalking)
        {


            // Movement
            Vector3 movement = destination - transform.position;


            float moveDifference = lastMovement.magnitude - movement.magnitude;

            if (moveDifference < noMovementThreshold)
                noOfFramesNotMoving++;
            else
                noOfFramesNotMoving = 0;

            lastMovement = movement;
            movement = movement.normalized * speed * Time.deltaTime;
            myRigidbody.MovePosition(transform.position + movement);

            // End movement if close to destination
            movement = destination - transform.position;
            if (movement.magnitude <= closeToDestinationThreshold)
            {
                isWalking = false;
                noOfFramesNotMoving = 0;
            }

            //End walk if stuck for too many frames
            if (noOfFramesNotMoving > maxNoOfFramesNotMoving)
            {
                isWalking = false;
                noOfFramesNotMoving = 0;
            }
        }
    }
}
