using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Preload : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
		Cursor.lockState = CursorLockMode.Locked;
		SceneManager.LoadScene( "title" );
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
