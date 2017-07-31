﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoToy : Toy 
{
	public Transform mouthOrigin;

	public ParticleSystem breathParticles;

	public Material enemyMaterial;
	public Renderer modelRenderer;

	public FlameTrigger flameTrigger;

	private bool isBreathing = false;
	private bool lastIsBreathing = false;

	protected override void CustomStart()
	{
		base.CustomStart();

		breathParticles.Stop();
	}

	protected override void AttackUpdate()
	{
		if ( knob.power > 0 )
		{
			float rayDistance = 4;
			Vector3 endPosition = mouthOrigin.forward * rayDistance;
			Debug.DrawRay( mouthOrigin.position, endPosition );
			isBreathing = true;

			foreach ( GameObject enclosedObject in flameTrigger.enclosedObjects )
			{
				if ( enclosedObject != null )
				{
					IHittable hittable = enclosedObject.GetComponent<IHittable>();

					if ( hittable != null )
					{
						HitInfo hitInfo = new HitInfo();
						hitInfo.damage = 1.5f * Time.deltaTime;
						hitInfo.origin = transform.position;

						hittable.Hit( hitInfo );
					}
				}
			}

			/*
			Ray ray = new Ray( mouthOrigin.position, endPosition );
			RaycastHit hit;

			if ( Physics.Raycast( ray, out hit, rayDistance ) )
			{
				IHittable hittable = hit.collider.GetComponent<IHittable>();

				if ( hittable != null )
				{
					HitInfo hitInfo = new HitInfo();
					hitInfo.damage = 1.5f * Time.deltaTime;

					hittable.Hit( hitInfo );
				}
			}

*/

			knob.power -= 0.25f * Time.deltaTime;

			if ( !isNPC )
			{
				EnemyToyManager.availableWindingPower += 0.25f * Time.deltaTime;
			}
		}

		if ( isNPC )
		{
			Vector3 moveDirection = new Vector3( npcHorizontal, 0, npcVertical );

			if ( moveDirection.magnitude > 0.1f )
			{
				modelObject.transform.rotation = Quaternion.LookRotation( moveDirection );
			}

		}


	}

	void LateUpdate()
	{
		if ( isBreathing && !lastIsBreathing )
		{
			// turn on breath effects
			breathParticles.Play();
		}
		else if ( !isBreathing && lastIsBreathing )
		{
			// turn off breath effects
			breathParticles.Stop();
		}

		lastIsBreathing = isBreathing;
		isBreathing = false;
	}

	public override void SetAsEnemy()
	{
		base.SetAsEnemy();
		modelRenderer.material = enemyMaterial;
	}

}
