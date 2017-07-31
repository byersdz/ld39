using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialParent : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
		
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		float aspectRatio = (float)Screen.width / (float)Screen.height;
		transform.localPosition = new Vector3( aspectRatio * 10, 0 ,0 );
		
	}
}
