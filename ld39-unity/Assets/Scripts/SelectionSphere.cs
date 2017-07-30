using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionSphere : MonoBehaviour 
{
	public Renderer myRenderer;
	// Use this for initialization
	void OnEnable () 
	{
		transform.localScale = new Vector3( 1.2f, 1.2f, 1.2f );
		StartCoroutine( ScaleRoutine() );
		StartCoroutine( RandomColorRoutine() );

	}

	void OnDisable()
	{
		StopAllCoroutines();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	IEnumerator ScaleRoutine()
	{
		float scaleTime = 1;

		iTween.ScaleTo( gameObject, iTween.Hash( "scale", new Vector3( 1.5f, 1.5f, 1.5f ), "time", scaleTime, "easetype", iTween.EaseType.easeInOutQuad ) );

		yield return new WaitForSeconds( 1.01f );

		iTween.ScaleTo( gameObject, iTween.Hash( "scale", new Vector3( 1.2f, 1.2f, 1.2f ), "time", scaleTime, "easetype", iTween.EaseType.easeInOutQuad ) );

		yield return new WaitForSeconds( 1.01f );

		StartCoroutine( ScaleRoutine() );

	}

	IEnumerator RandomColorRoutine()
	{
		Color randomColor = new Color( Random.value, Random.value, Random.value );
		//myRenderer.material.color = randomColor;

		iTween.ColorTo( gameObject, iTween.Hash( "color", randomColor, "time", 0.25f, "easetype", iTween.EaseType.linear ) );

		yield return new WaitForSeconds( 0.25f );

		StartCoroutine( RandomColorRoutine() );
	}
}
