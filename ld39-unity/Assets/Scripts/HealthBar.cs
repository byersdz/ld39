using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour 
{
	public float hitPoints = 10;
	public float damage;

	public GameObject innerObject;

	void LateUpdate()
	{
		float clampedDamage = Mathf.Clamp( damage, 0, hitPoints );
		clampedDamage = hitPoints - clampedDamage;

		float healthPercentage = clampedDamage / hitPoints;

		innerObject.transform.localScale = new Vector3( 1, 1, healthPercentage );
	}

}
