using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : MonoBehaviour, IHittable 
{
	public HealthBar healthBar;
	public DeathSequence deathSequence;

	private bool isDead = false;

	public void Hit( HitInfo theHit )
	{
		healthBar.damage += theHit.damage;
	}

	void Update()
	{
		if ( healthBar.damage >= healthBar.hitPoints )
		{
			if ( !isDead )
			{
				deathSequence.BeginDeathSequence();
				isDead = true;
			}
		}
	}
}
