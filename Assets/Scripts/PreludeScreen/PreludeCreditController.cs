using UnityEngine;
using System.Collections;

public class PreludeCreditController : MonoBehaviour {

	private int zoomPhase;
	public Camera cameraHandler;
	private float cyclingX;
	private float cyclingY;
	private float cyclingZ;
	private bool cyclingDir;
	private float cyclingZoom;

	private float startTime;
	private float upperBound = 5.0f;
	
	void Start () {
		cyclingX = Random.Range(0.0f, 360.0f);
		cyclingY = Random.Range(0.0f, 360.0f);
		cyclingZ = Random.Range(0.0f, 360.0f);
		cyclingZoom = Random.Range(0.5f, 1.0f);

		zoomPhase = 0;
		startTime = Time.time;
	}
	
	void FixedUpdate() {

		if( (Time.time - startTime) > upperBound){
			cameraHandler.fieldOfView = 60.0f;
			startTime = Time.time;
		}
		cyclingX += Random.Range(0.0f, upperBound) * Time.deltaTime;
		cyclingY += Random.Range(0.0f, upperBound) * Time.deltaTime;
		cyclingZ += Random.Range(0.0f, upperBound) * Time.deltaTime;
		cyclingZoom = Random.Range(0.1f, upperBound);
	

		if (cyclingDir) {
			if (cameraHandler.fieldOfView > 30) {
				cameraHandler.fieldOfView -= cyclingZoom;
			}
		}
		else
		{
			if (cameraHandler.fieldOfView < 50) { 
				cameraHandler.fieldOfView += cyclingZoom;
			}
		}
		
		cyclingX = cyclingX % 360;
		cyclingY = cyclingY % 360;
		cyclingZ = cyclingZ % 360;


		switch(zoomPhase)
		{
			case 0://Reset to normal view.
				cyclingDir = false;
				
				if (Random.Range(0.0f, 100.0f) < 0.2f)
					zoomPhase = 1;
				break;
			case 1://Zoom in.
				cyclingDir = true;
				
				if (Random.Range(0.0f, 100.0f) < 0.2f)
					zoomPhase = 2;
				break;
			case 2://Zoom in in.
				cyclingDir = true;
				
				if (Random.Range(0.0f, 100.0f) < 5.0f)
					zoomPhase = 3;
				break;
			case 3://Zoom in out.
				cyclingDir = false;
				
				if (Random.Range(0.0f, 100.0f) < 5.0f)
					zoomPhase = 2;
				
				if (Random.Range(0.0f, 100.0f) < 1.0f)
					zoomPhase = 4;
				break;
			case 4:
				zoomPhase = 0;
				break;
		}


		Vector3 offset = new Vector3(Mathf.Cos(cyclingX)*0.25f, Mathf.Cos(cyclingY) * 0.25f + 1.0f, Mathf.Cos(cyclingZ) * 0.25f);
		cameraHandler.transform.LookAt(offset + new Vector3(-cameraHandler.fieldOfView/20.0f, 0.0f,0.0f),new Vector3(0.0f, 1.0f, 0.0f));
	}
}
