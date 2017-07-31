using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knob : MonoBehaviour 
{
	public GameObject modelObject;
	public AudioSource windAudio;

	private float _power;
	private float lastPower;

	public float power
	{
		get
		{
			return _power;
		}
		set
		{
			if ( value < 0 )
			{
				_power = 0;
			}
			else if ( value > 1 )
			{
				_power = 1;
			}
			else
			{
				_power = value;
			}
		}
	}


	void LateUpdate()
	{
		float maxAngle = 315;
		float currentAngle = maxAngle * _power;
		modelObject.transform.localEulerAngles = new Vector3( 0, currentAngle, 0 );

	}
}
