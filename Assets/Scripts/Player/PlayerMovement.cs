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
	float camRayLength = 1000f;          // The length of the ray from the camera into the scene.



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
	const int downConst = 0;
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


	// Destination Marker
	public GameObject marker;

	// Button Restricted area
	private float screenTopLeft_x_chapter2Activated;
	private float screenTopLeft_x;
	private float screenTopLeft_y;
	private float screenBottomRight_x;
	private float screenBottomRight_y;



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

		// Load Idle
		for (int i=0; i<idleFrames; i++) {
			sprites[idleConst*walkFrames+i] = Resources.Load<Sprite>(PlayerData.FormSpritePath(pieceName, 4) + i);
            
		}

	
		spriteRenderer.sprite = sprites[currDirection + currFrame];

		if (marker == null)
			marker = GameObject.Find ("DestinationMarker");


		screenTopLeft_x_chapter2Activated = (Screen.width * 0.12f);
		screenTopLeft_x = (Screen.width * 0.20f);
		screenTopLeft_y = (Screen.height * 0.9f);
		screenBottomRight_x = (Screen.width * 0.9f);
		screenBottomRight_y = (Screen.height * 0.2f);
	

	}
	public void MouseOverButton(){
        PlayerData.MoveFlag = false;
	}

	public void MouseLeftButton(){
		mouseOverButton = false;
	}

    public bool isMoving() {
        return isWalking;
    }


	public void StopMoving(){
		isWalking = false;
	}


	public int GetCurrDir(){
		return currDirection;
	}

	public void ForceIdle(){
		StopMoving ();
		currDirection = downConst;
		currFrame = 0;
		idleTime = timeBeforeIdle;
	}

	void FixedUpdate ()
	{

		currTime += Time.deltaTime;

		// Check mouse input
		if (Input.GetKey (KeyCode.Mouse0)) {

			if(EndingController.instance.isChapter2Activated)
				screenTopLeft_x = screenTopLeft_x_chapter2Activated;

			if ((Input.mousePosition.x > screenBottomRight_x && Input.mousePosition.y < screenBottomRight_y) || 
			    (Input.mousePosition.x < screenTopLeft_x && Input.mousePosition.y > screenTopLeft_y)) {
				mouseOverButton = true;

				isWalking = false;
				currFrame = 0;
				int frameToLoad = currDirection * 8 + currFrame;
				spriteRenderer.sprite = sprites [frameToLoad];
			} else 
				mouseOverButton = false;

			if (PlayerData.MoveFlag && !GamePause.isPaused && !mouseOverButton) {
				// Create a ray from the mouse cursor on screen in the direction of the camera.
				Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		
				// Create a RaycastHit variable to store information about what was hit by the ray.
				RaycastHit floorHit;
		
				// Perform the raycast and if it hits something on the floor layer...
				if (Physics.Raycast (camRay, out floorHit, camRayLength, floorMask)) {

					//GameObject.Find("InteractionButton").GetComponent<BubbleBehaviour>().alreadySelected = false;
					destination = floorHit.point;
			
					marker.transform.position = new Vector3 (destination.x, destination.y + 0.3f, destination.z - 0.3f);
					//marker.GetComponent<MeshRenderer>().enabled = true;

			
					// Create a vector from the player to the point on the floor the raycast from the mouse hit.
					Vector3 playerToMouse = floorHit.point - transform.position;
			
					// Ensure the vector is entirely along the floor plane.
					playerToMouse.y = 0f;

					Vector3 camForward = GameObject.Find ("Main Camera").transform.forward;

					camForward.y = 0f;

					float angle = Mathf.Rad2Deg * Mathf.Atan2 (playerToMouse.x * camForward.z - playerToMouse.z * camForward.x, playerToMouse.x * camForward.x + playerToMouse.z * camForward.z) + 180;

					// Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
					//Quaternion newRotation = Quaternion.LookRotation (playerToMouse);
			
					//float angle = newRotation.eulerAngles.y;
			
					//Debug.Log(angle);
			
					int prevDirection = currDirection;
			
					// Right
					if (angle >= 225 && angle <= 315) {
						//currDirection = leftConst;
						currDirection = rightConst;
					}
			//Left
			else if (angle >= 45 && angle <= 135) {
						//currDirection = rightConst;
						currDirection = leftConst;
					}
			// Down
			else if (angle >= 315 || angle <= 45) {
						//currDirection = upConst;
						currDirection = downConst;
					}
			// up
			else {
						//currDirection = downCnst;
						currDirection = upConst;
					}
					if (currDirection != prevDirection) {
						currTime = timePerFrame;
						currFrame = 0;
					}
					isWalking = true;
					noOfFramesNotMoving = 0;
				}
			}// end of move flag is true
		}// end of mouse down


		if (isWalking) {
			marker.GetComponent<MeshRenderer> ().enabled = true;
			// Movement
			Vector3 movement = destination - transform.position;


			float moveDifference = lastMovement.magnitude - movement.magnitude;

			if (moveDifference < noMovementThreshold) {
				noOfFramesNotMoving++;
			} else
				noOfFramesNotMoving = 0;

			lastMovement = movement;
			movement = movement.normalized * speed * Time.deltaTime;
			playerRigidbody.MovePosition (transform.position + movement);

			// End movement if close to destination
			movement = destination - transform.position;

			if (movement.magnitude < 0.7f)
				marker.GetComponent<MeshRenderer> ().enabled = false;

			if (movement.magnitude <= closeToDestinationThreshold) {
				isWalking = false;
				noOfFramesNotMoving = 0;
			}

			//End walk if stuck for too many frames
			if (noOfFramesNotMoving > maxNoOfFramesNotMoving) {
				isWalking = false;
				noOfFramesNotMoving = 0;
			}
		} else
			marker.GetComponent<MeshRenderer> ().enabled = false;

		// Change Idle Timer
		if (currDirection == downConst && !isWalking) {
			float prevTime = idleTime;
			idleTime += Time.deltaTime;
			if (idleTime >= timeBeforeIdle) {
				isIdlePlaying = true;
			}
		} else {
			isIdlePlaying = false;
			idleTime = 0;
		}


		// Check for sprite frame update
		if (currTime >= timePerFrame) {
			if (isWalking) {
				currFrame = (currFrame + 1) % walkFrames;
			} else {

				if (!isIdlePlaying) {
					currFrame = 0;
				} else {
					if (currFrame == repeatedFrame) {
						if (idleMidTime < timePauseInIdle) {
							idleMidTime += Time.deltaTime;
						} else {
							idleMidTime = 0;
							currFrame++;
						}
					} else {
						currFrame++;
					}


					if (currFrame >= idleFrames) {
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
			else if (isIdlePlaying) {
				frameToLoad = idleConst * 8 + currFrame;
			}

			//Debug.Log(currFrame);
			spriteRenderer.sprite = sprites [frameToLoad];
			currTime = 0;

		}
	}

}