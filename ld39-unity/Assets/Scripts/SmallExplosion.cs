using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallExplosion : MonoBehaviour 
{
	public Renderer myRenderer;
	// Use this for initialization
	void Start () {

		if ( Random.value < 0.5f )
		{
			myRenderer.material.color = Color.black;
		}

		float randomScale = 1 + 2 * Random.value;
		transform.localScale = new Vector3( randomScale, randomScale, randomScale );

		Destroy( gameObject, 0.1f );
			
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
