using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomColor : MonoBehaviour 
{
	public List<Color> colors;

	public Renderer myRenderer;

	// Use this for initialization
	void Start () 
	{
		Color randomColor = colors[Random.Range( 0, colors.Count )];

		myRenderer.material.color = randomColor;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
