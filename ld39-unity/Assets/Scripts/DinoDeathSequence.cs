using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoDeathSequence : DeathSequence 
{
	public GameObject parentObject;

	public Toy toy;

	public override void BeginDeathSequence()
	{
		base.BeginDeathSequence();

		StartCoroutine( DeathRoutine() );
	}

	private IEnumerator DeathRoutine()
	{
		yield return new WaitForSeconds( 2 );

		toy.RemoveFromGame();

		DeathSequenceFinished();

		Destroy( parentObject );
		Destroy( gameObject );
	}

}
