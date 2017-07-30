using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathSequence : MonoBehaviour 
{
	public Transform cameraTransform;
	public Transform lookTransform;

	public virtual void BeginDeathSequence()
	{
		ToyCamera.deathPosition = cameraTransform.position;
		ToyCamera.deathLookPosition = lookTransform.position;

		transform.parent = null;
		ToyManager.sharedInstance.BeginDeathAnimation();
	}

	protected virtual void DeathSequenceFinished()
	{
		ToyManager.sharedInstance.EndDeathAnimation();
	}


}
