using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoToy : Toy 
{
	public Transform mouthOrigin;

	public ParticleSystem breathParticles;

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

			knob.power -= 0.25f * Time.deltaTime;
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

}
