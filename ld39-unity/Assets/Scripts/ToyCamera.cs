using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyCamera : MonoBehaviour 
{
	public State state;
	public Toy currentToyTarget;
	public Camera myCamera;

	public static Vector3 deathPosition;
	public static Vector3 deathLookPosition;

	public static Transform enemyFollowTransform;

	public enum State
	{
		ToyIsSelected,
		ToyIsControlled,
		ZoomedOut,
		DeathAnimation,
		FollowEnemies
	}


	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void LateUpdate () 
	{
		if ( state == State.ToyIsControlled )
		{
			float lerpAmount = 0.3f;

			Vector3 targetPosition = currentToyTarget.controlledCameraTarget.position;
			Vector3 newPosition = Vector3.Lerp( transform.position, targetPosition, lerpAmount );
			transform.position = newPosition;

			Vector3 relativePosition = currentToyTarget.controlledLookTarget.position - transform.position;
			Quaternion targetRotation = Quaternion.LookRotation( relativePosition );
			transform.rotation = Quaternion.Lerp( transform.rotation, targetRotation, 0.8f );

			//transform.LookAt( currentToyTarget.controlledLookTarget );
		}
		else if ( state == State.ToyIsSelected )
		{
			float lerpAmount = 0.2f;

			Vector3 targetPosition = currentToyTarget.selectedCameraTarget.position;
			Vector3 newPosition = Vector3.Lerp( transform.position, targetPosition, lerpAmount );
			transform.position = newPosition;

			Vector3 relativePosition = currentToyTarget.selectedLookTarget.position - transform.position;
			Quaternion targetRotation = Quaternion.LookRotation( relativePosition );
			transform.rotation = Quaternion.Lerp( transform.rotation, targetRotation, lerpAmount );
		}
		else if ( state == State.ZoomedOut )
		{
			float lerpAmount = 0.15f;

			Vector3 targetPosition = new Vector3( 32, 45, -5 );
			Vector3 lookingAtPosition = new Vector3( 32, 0, 24 );

			transform.position = Vector3.Lerp( transform.position, targetPosition, lerpAmount );

			Vector3 relativePosition = lookingAtPosition - transform.position;
			Quaternion targetRotation = Quaternion.LookRotation( relativePosition );
			transform.rotation = Quaternion.Lerp( transform.rotation, targetRotation, lerpAmount );
		}
		else if ( state == State.DeathAnimation )
		{
			transform.position = deathPosition;

			Vector3 relativePosition = deathLookPosition - deathPosition;
			transform.rotation = Quaternion.LookRotation( relativePosition );
		}
		else if ( state == State.FollowEnemies )
		{
			if ( enemyFollowTransform != null )
			{
				float lerpAmount = 0.2f;

				Vector3 targetPosition = enemyFollowTransform.position;
				Vector3 newPosition = Vector3.Lerp( transform.position, targetPosition, lerpAmount );
				transform.position = newPosition;

				transform.rotation = Quaternion.Lerp( transform.rotation, enemyFollowTransform.rotation, lerpAmount );
			}
		}
	}
}
