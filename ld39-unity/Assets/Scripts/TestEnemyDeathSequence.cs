using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemyDeathSequence : DeathSequence 
{
	public GameObject parentObject;

	public override void BeginDeathSequence()
	{
		base.BeginDeathSequence();

		StartCoroutine( DeathRoutine() );
	}

	private IEnumerator DeathRoutine()
	{
		yield return new WaitForSeconds( 2 );

		DeathSequenceFinished();

		Destroy( parentObject );
		Destroy( gameObject );
	}

}
