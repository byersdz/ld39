using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargeExplosion : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
		StartCoroutine( ExplodeRoutine() );
	}

	IEnumerator ExplodeRoutine()
	{
		float explodeTime = 0.5f;

		transform.localScale = Vector3.zero;

		iTween.ScaleTo( gameObject, iTween.Hash( "scale", Vector3.one, "time", explodeTime ) );

		yield return new WaitForSeconds( explodeTime + 0.5f );

		Destroy( gameObject );
	}
	
}
