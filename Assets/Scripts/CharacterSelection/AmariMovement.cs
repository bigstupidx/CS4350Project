using UnityEngine;
using System.Collections;

public class AmariMovement : MonoBehaviour {

	
	public static float speed = 3f;            // The speed that Amaris will move at.
	public static float closeToDestinationThreshold = 0.1f; // Amari will stop moving when the distance from the destination is below this threshold
	Vector3 destination;                   // The vector to store the direction of the amari's movement.
	
	
	Rigidbody myRigidbody;          // Reference to the this amari's rigidbody.
	SpriteRenderer spriteRenderer;		// Reference to this amari's spriteRenderer
	
	
	
	// Movement Controls
	bool isWalking;
	Vector3 lastMovement;
	//int noOfFramesNotMoving = 0;
	//int maxNoOfFramesNotMoving =3 ;
	//float noMovementThreshold = 0.005f;
	
	// Sprite Frame Controls
	const int walkFrames = 8;

	const int downConst = 0;
	const int leftConst = 1;
	const int rightConst = 2;
	const int upConst = 3;

	int currDirection =0;
	int currFrame = 0;
	//bool flip = true;
	float currTime = 0;

	public float timePerFrame = 0.025f;	

	bool moveToCalled = false;
	
	Sprite[] sprites;
	
	void Awake ()
	{

		// Set up references.
		spriteRenderer = GetComponent<SpriteRenderer> ();
		myRigidbody = GetComponent<Rigidbody> ();

		AmariSelection selectionScript = transform.GetComponent<AmariSelection> ();
		int gender = selectionScript.gender;

		
		// Load Sprite
		sprites = new Sprite[walkFrames*4];
		
		string pieceName = "walk" + gender;


		
		// Load Down
		for (int i=0; i<walkFrames; i++) {
			sprites[i+downConst*walkFrames] = Resources.Load<Sprite> (PlayerData.FormSpritePath(pieceName, 0) + i);
		}
		
		// Load Left
		for (int i=0; i<walkFrames; i++) {
			sprites[i+leftConst*walkFrames] = Resources.Load<Sprite>(PlayerData.FormSpritePath(pieceName, 1) + i);
		}
		
		// Load Right
		for (int i=0; i<walkFrames; i++) {
			sprites[i+rightConst*walkFrames] = Resources.Load<Sprite>(PlayerData.FormSpritePath(pieceName, 2) + i);
		}
		
		// Load Up
		for (int i=0; i<walkFrames; i++) {
			sprites[i+upConst*walkFrames] = Resources.Load<Sprite>(PlayerData.FormSpritePath(pieceName, 3) + i);
		}
		

		spriteRenderer.sprite = sprites[currDirection + currFrame];

	}


	public bool IsMoving(){
		return isWalking;
	}

	public void FaceFront(){
		currDirection = upConst;
	}
	
    public void StopMoving()
    {
        isWalking = false;
    }

    public int GetCurrDir(){
		return currDirection;
	}

	public void MoveTo(Vector3 newDest){
		destination = newDest;
		moveToCalled = true;
	}
 
	void FixedUpdate ()
	{
		currTime += Time.deltaTime;

        // Check mouse input
        if (moveToCalled)
        {
            isWalking = true;
            moveToCalled = false;
            // Create a vector from the player to the point on the floor the raycast from the mouse hit.
            Vector3 playerToMouse = destination - transform.position;

            // Ensure the vector is entirely along the floor plane.
            playerToMouse.y = 0f;

            // Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);

            // Set the player's rotation to this new rotation.

            float angle = newRotation.eulerAngles.y;

            //Debug.Log(angle);


            currTime = timePerFrame;
            currFrame = 0;
            // Right
            if (angle >= 225 && angle <= 315)
            {
                currDirection = leftConst;
            }
            //Left
            else if (angle >= 45 && angle <= 135)
            {
                currDirection = rightConst;
            }
            // Up
            else if (angle >= 315 || angle <= 45)
            {
                currDirection = upConst;
            }
            // Down
            else
            {
                currDirection = downConst;
            }


        }

        if (isWalking){
			
			
			// Movement
			Vector3 movement = destination - transform.position;
			
			
			float moveDifference = lastMovement.magnitude - movement.magnitude;
			
			/*if(moveDifference < noMovementThreshold)
				noOfFramesNotMoving++;
			else
				noOfFramesNotMoving = 0;
			*/
			lastMovement = movement;
			movement = movement.normalized * speed * Time.deltaTime;
			myRigidbody.MovePosition (transform.position + movement);
			
			// End movement if close to destination
			movement = destination - transform.position;
			if(movement.magnitude <= closeToDestinationThreshold){
				isWalking = false;
				//noOfFramesNotMoving = 0;
			}
			
			//End walk if stuck for too many frames
		/*	if(noOfFramesNotMoving > maxNoOfFramesNotMoving){
				isWalking = false;
				noOfFramesNotMoving = 0;
			}*/
		}

		
		// Check for sprite frame update
		if (currTime >= timePerFrame) {
			if(isWalking){
				currFrame = (currFrame+1) % walkFrames;
			} else {
				
				currFrame = 0;		
				
			}
			

			int frameToLoad = currDirection * 8 + currFrame;
			

			
			//Debug.Log(currFrame);
			spriteRenderer.sprite = sprites[frameToLoad];
			currTime = 0;
			
		}
		
	}

}
