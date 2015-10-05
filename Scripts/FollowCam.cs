using UnityEngine;
using System.Collections;

public class FollowCam : MonoBehaviour {

	static public FollowCam S;
	public float maxX;
	public bool objectsBelowThisBooleanAreSetByTheGameButNeedToBePublic;
	
	public GameObject poi;

	private Vector3 originalPos;
	private float camZ;
	private float cameraHeight; //also determines the minimum heigh of the camera and is used in sizing the camera
	
	void Awake(){//god damn capital letters
		
		S = this;
		camZ = this.transform.position.z;
		originalPos = transform.position;
		cameraHeight = this.camera.orthographicSize;
		
	}
	
	void FixedUpdate(){//GAWD DAMMIT!!!!!!!! REMEMBER TO USE CAPITAL LETTERS
		
		if (poi == null) { 

			SetToOrigin ();
		
		} else {

			Vector3 destination = Vector3.Lerp (transform.position, poi.transform.position, 0.1f);
			if (poi.transform.position.x > transform.position.x) { //if the shot is ahead of the camera, move the camera
				
				destination.z = camZ;
				
				if(destination.x > maxX){ destination.x = maxX; } //if the camera is within bounds, move the camera
				if(destination.y < originalPos.y){ destination.y = originalPos.y; } //if the camera is within bounds, move the camera
				transform.position = destination; //move the camera

			}

			if(poi.rigidbody != null) { if(poi.rigidbody.IsSleeping()) { poi = null; return; }}

		}



		if (transform.position.y > originalPos.y) {

			this.camera.orthographicSize = transform.position.y - originalPos.y + cameraHeight;

		} 

	}

	void SetToOrigin(){

		transform.position = originalPos;
		this.camera.orthographicSize = cameraHeight;

	}
}
