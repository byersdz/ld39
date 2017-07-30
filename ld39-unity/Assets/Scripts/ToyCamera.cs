using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyCamera : MonoBehaviour 
{
	public State state;
	public Toy currentToyTarget;

	public enum State
	{
		ToyIsSelected,
		ToyIsControlled,
		ZoomedOut
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
			Vector3 targetPosition = currentToyTarget.controlledCameraTarget.position;
			Vector3 newPosition = Vector3.Lerp( transform.position, targetPosition, 0.3f );
			transform.position = newPosition;


			transform.LookAt( currentToyTarget.controlledLookTarget );
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

			Vector3 targetPosition = new Vector3( 40, 65, 0 );
			Vector3 lookingAtPosition = new Vector3( 40, 0, 20 );

			transform.position = Vector3.Lerp( transform.position, targetPosition, lerpAmount );

			Vector3 relativePosition = lookingAtPosition - transform.position;
			Quaternion targetRotation = Quaternion.LookRotation( relativePosition );
			transform.rotation = Quaternion.Lerp( transform.rotation, targetRotation, lerpAmount );



		}
	}
}
