using UnityEngine;
using System.Collections;
using System;

public class CameraFollow : MonoBehaviour 
{
	public float xMargin = 1f;		// Distance in the x axis the player can move before the camera follows.
	public float yMargin = 1f;		// Distance in the y axis the player can move before the camera follows.
	public float xSmooth = 8f;		// How smoothly the camera catches up with it's target movement in the x axis.
	public float ySmooth = 8f;		// How smoothly the camera catches up with it's target movement in the y axis.
	public Vector2 maxXAndY;		// The maximum x and y coordinates the camera can have.
	public Vector2 minXAndY;		// The minimum x and y coordinates the camera can have.


	public Transform player;		// Reference to the player's transform.


	void Awake ()
	{
		// Setting up the reference.
		//player = GameObject.FindGameObjectWithTag("Player").transform;
	}

	
	void LateUpdate()
    {
		TrackPlayer();
    }
	
	void TrackPlayer ()
	{
		// Get the current position of the camera 
		Vector3 cameraPosition = transform.position;


		// follow the payer 
		cameraPosition.x = Mathf.Max(cameraPosition.x, player.position.x);
		cameraPosition.y = Mathf.Max(cameraPosition.y, player.position.y);

		if (Mathf.Abs(cameraPosition.y - player.position.y) > yMargin)
		{
			cameraPosition.y = Mathf.MoveTowards(cameraPosition.y, 6.5f, ySmooth * Time.deltaTime);
		}




        // set the camera's position to the new position
        transform.position = cameraPosition;
	}
}
