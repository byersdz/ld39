using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DinoDeathSequence : DeathSequence 
{
	public GameObject parentObject;
	public GameObject smallExplosionPrefab;
	public GameObject largeExplosionPrefab;

	public Toy toy;

	private bool spawnSmallExplosions;
	private float smallExplosionTimer;

	public override void BeginDeathSequence()
	{
		base.BeginDeathSequence();

		StartCoroutine( DeathRoutine() );
	}

	private IEnumerator DeathRoutine()
	{
		float waitTime = 2;
		if ( ToyManager.sharedInstance.toys.Count <= 1 )
		{
			TutorialPanel.showGameOverPanel = true;
			waitTime = 4;
		}

		spawnSmallExplosions = true;

		yield return new WaitForSeconds( waitTime );

		if ( ToyManager.sharedInstance.toys.Count <= 1 )
		{
			// game over
			Round.currentRound = 0;
			SceneManager.LoadScene( "title" );
		}
		else if ( EnemyToyManager.instance.toys.Count <= 1 )
		{
			// we win, increment the round and reload the game
			Round.currentRound++;
			SceneManager.LoadScene( "test" );
		}
		else
		{

			toy.RemoveFromGame();

			spawnSmallExplosions = false;

			Vector3 largeExplosionPosition = parentObject.transform.position;
			largeExplosionPosition.y += 1;

			Instantiate( largeExplosionPrefab, largeExplosionPosition, Quaternion.identity );
		}
		yield return new WaitForSeconds( 1 );


		DeathSequenceFinished();

		Destroy( parentObject );
		Destroy( gameObject );
	}

	void Update()
	{
		if ( spawnSmallExplosions )
		{
			if ( smallExplosionTimer < 0 )
			{
				smallExplosionTimer = 0.1f;

				Vector3 explosionPosition = parentObject.transform.position;
				explosionPosition.y += 1.0F;
				explosionPosition.x += Random.value * 2 - 1;
				explosionPosition.y += Random.value * 4 - 2;
				explosionPosition.z += Random.value * 2 - 1;

				Instantiate( smallExplosionPrefab, explosionPosition, Quaternion.identity );
			}

			smallExplosionTimer -= Time.deltaTime;


		}
	}

}
