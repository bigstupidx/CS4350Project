using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	
	public float speed = 6f;            // The speed that the player will move at.
	public float closeToDestinationThreshold = 0.2f; // Player will stop moving when the distance from the destination is below this threshold
	Vector3 destination;                   // The vector to store the direction of the player's movement.
	bool mouseOverButton = false;


	Rigidbody playerRigidbody;          // Reference to the player's rigidbody.
	SpriteRenderer spriteRenderer;		// Reference to player's spriteRenderer
	int floorMask;                      // A layer mask so that a ray can be cast just at gameobjects on the floor layer.
	float camRayLength = 100f;          // The length of the ray from the camera into the scene.



	// Movement Controls
	bool isWalking;
	Vector3 lastMovement;
	int noOfFramesNotMoving = 0;
	int maxNoOfFramesNotMoving = 3;
	float noMovementThreshold = 0.005f;

	// Sprite Frame Controls
	const int walkFrames = 8;
	const int idleFrames = 15;
    const int repeatedFrame = 7;
    const int repeatedFrameTimes = 20;
	const int downCnst = 0;
	const int leftConst = 1;
	const int rightConst = 2;
	const int upConst = 3;
    const int idleConst = 4;
    int currDirection =0;
	int currFrame = 0;
	//bool flip = true;
	float currTime = 0;
    float idleTime = 0;
	float idleMidTime = 0;
	bool isIdlePlaying = false;
    public float timeBeforeIdle = 30.0f;
	public float timePauseInIdle = 3.0f;
    public float timePerFrame = 0.025f;




	Sprite[] sprites;

	void Awake ()
	{
		// Create a layer mask for the floor layer.
		floorMask = LayerMask.GetMask ("Floor");
		
		// Set up references.
		spriteRenderer = GetComponent<SpriteRenderer> ();
		playerRigidbody = GetComponent<Rigidbody> ();

		// Load Sprite
		sprites = new Sprite[walkFrames*4+idleFrames];

        string pieceName = "walk" + PlayerData.GenderId;


		// Load Down
		for (int i=0; i<walkFrames; i++) {
			sprites[i+downCnst*walkFrames] = Resources.Load<Sprite> (PlayerData.FormSpritePath(pieceName, 0) + i);
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

		// Load Idle
		for (int i=0; i<idleFrames; i++) {
			sprites[idleConst*walkFrames+i] = Resources.Load<Sprite>(PlayerData.FormSpritePath(pieceName, 4) + i);
            
		}

	
		spriteRenderer.sprite = sprites[currDirection + currFrame];
	


	}
	public void MouseOverButton(){
		mouseOverButton = true;
	}

	public void MouseLeftButton(){
		mouseOverButton = false;
	}

	public void StopMoving(){
		isWalking = false;
	}


	public int GetCurrDir(){
		return currDirection;
	}

	void FixedUpdate ()
	{
		currTime += Time.deltaTime;
		// Check mouse input
		if (Input.GetKeyDown (KeyCode.Mouse0)&& PlayerData.MoveFlag && !mouseOverButton) {
			// Create a ray from the mouse cursor on screen in the direction of the camera.
			Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);
			
			// Create a RaycastHit variable to store information about what was hit by the ray.
			RaycastHit floorHit;
			
			// Perform the raycast and if it hits something on the floor layer...
			if(Physics.Raycast (camRay, out floorHit, camRayLength, floorMask))
			{
				destination = floorHit.point; 

				// Create a vector from the player to the point on the floor the raycast from the mouse hit.
				Vector3 playerToMouse = floorHit.point - transform.position;
				
				// Ensure the vector is entirely along the floor plane.
				playerToMouse.y = 0f;
				
				// Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
				Quaternion newRotation = Quaternion.LookRotation (playerToMouse);
				
				// Set the player's rotation to this new rotation.

				float angle = newRotation.eulerAngles.y;

				//Debug.Log(angle);


				currTime = timePerFrame;
				currFrame = 0;
				// Right
				if(angle>=225 && angle <= 315){
					currDirection = leftConst;
				}
				//Left
				else if(angle>=45 && angle <= 135){
					currDirection = rightConst;
				}
				// Up
				else if(angle>=315 || angle <= 45){
					currDirection = upConst;
				}
				// Down
				else{
					currDirection = downCnst;
				}
				isWalking = true;
			}
		}


		if(isWalking){


			// Movement
			Vector3 movement = destination - transform.position;

		
			float moveDifference = lastMovement.magnitude - movement.magnitude;

			if(moveDifference < noMovementThreshold)
				noOfFramesNotMoving++;
			else
				noOfFramesNotMoving = 0;

			lastMovement = movement;
			movement = movement.normalized * speed * Time.deltaTime;
			playerRigidbody.MovePosition (transform.position + movement);

			// End movement if close to destination
			movement = destination - transform.position;
			if(movement.magnitude <= closeToDestinationThreshold){
				isWalking = false;
				noOfFramesNotMoving = 0;
			}

			//End walk if stuck for too many frames
			if(noOfFramesNotMoving > maxNoOfFramesNotMoving){
				isWalking = false;
				noOfFramesNotMoving = 0;
			}
		}

        // Change Idle Timer
        if (currDirection == downCnst && !isWalking)
        {
            float prevTime = idleTime;
            idleTime += Time.deltaTime;
            if(idleTime >= timeBeforeIdle)
            {
				isIdlePlaying = true;
            }
        }
        else
        {
			isIdlePlaying = false;
            idleTime = 0;
        }


        // Check for sprite frame update
        if (currTime >= timePerFrame) {
			if(isWalking){
				currFrame = (currFrame+1) % walkFrames;
			} else {

				if (!isIdlePlaying)
                {
                    currFrame = 0;
                }
                else
                {
					if(currFrame == repeatedFrame){
						if(idleMidTime < timePauseInIdle){
							idleMidTime += Time.deltaTime;
						}
						else{
							idleMidTime = 0;
							currFrame++;
						}
					}
					else{
                    	currFrame++;
					}


					if(currFrame >= idleFrames)
                    {
                        currFrame = 0;
                        idleTime = 0;
						isIdlePlaying = false;
                    }


                }


			}

			int frameToLoad = 0;
            // Load New Frame
            //Walkframe 0-7 
            if (idleTime < timeBeforeIdle)
				frameToLoad = currDirection * 8 + currFrame;
            
			else if(isIdlePlaying){
				frameToLoad = idleConst *  8 + currFrame;
			}

            //Debug.Log(currFrame);
			spriteRenderer.sprite = sprites[frameToLoad];
			currTime = 0;

		}

	}

}