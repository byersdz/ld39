using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameTrigger : MonoBehaviour 
{
	public List<GameObject> enclosedObjects;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter( Collider other )
	{
		if ( !enclosedObjects.Contains( other.gameObject ) )
		{
			enclosedObjects.Add( other.gameObject );
		}
	}

	void OnTriggerExit( Collider other )
	{
		//Debug.Log( "Exit?" );

		if ( enclosedObjects.Contains( other.gameObject ) )
		{
			//Debug.Log( "TRIGGER EXIT" );
			enclosedObjects.Remove( other.gameObject );
		}

	}
}
