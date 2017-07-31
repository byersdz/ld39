using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundUI : MonoBehaviour 
{
	public TextMesh textMesh;

	// Use this for initialization
	void Start () {

		int round = Round.currentRound + 1;
		textMesh.text = "Round " + round.ToString();
		Destroy( gameObject, 3 );
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
