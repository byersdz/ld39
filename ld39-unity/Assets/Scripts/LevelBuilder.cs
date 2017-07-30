using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour 
{
	public GameObject basicTile;

	public static int width = 8;
	public static int height = 8;

	// Use this for initialization
	void Awake () 
	{
		for ( int x = 0; x < width; x++ )
		{
			for ( int y = 0; y < height; y++ )
			{
				GameObject instance = Instantiate( basicTile );
				instance.transform.position = new Vector3( x * 8, 0, y * 8 );
			}
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
