using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPanel : MonoBehaviour 
{
	public bool isOn = false;

	public bool isGameOverPanel;

	public static bool showGameOverPanel = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
		
		Vector3 targetPosition = Vector3.zero;

		if ( !isOn || ( showGameOverPanel && isGameOverPanel ) )
		{
			targetPosition = new Vector3( 10, 0, 0 );
		}

		transform.localPosition = Vector3.Lerp( transform.localPosition, targetPosition, 0.2f );
	}
}
