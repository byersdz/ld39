using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCToyController : MonoBehaviour 
{
	public Toy toy;

	public MoveState moveState;

	public Toy closestPlayerToy;
	public float distanceToClosestPlayerToy;

	public enum MoveState
	{
		Still,
		TowardsClosestPlayer,
		Attack
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if ( !toy.isNPC )
		{
			return;
		}

		toy.npcHorizontal = 0;
		toy.npcVertical = 0;
		toy.npcAttacking = false;

		distanceToClosestPlayerToy = 100000;

		foreach( Toy toy in ToyManager.sharedInstance.toys )
		{
			float currentDistance = ( toy.transform.position - transform.position ).magnitude;

			if ( currentDistance < distanceToClosestPlayerToy )
			{
				closestPlayerToy = toy;
				distanceToClosestPlayerToy = currentDistance;
			}
		}


		if ( moveState == MoveState.Still )
		{
			toy.state = Toy.State.OtherIsControlled;
		}
		else if ( moveState == MoveState.TowardsClosestPlayer || moveState == MoveState.Attack )
		{
			toy.state = Toy.State.Controlled;

			Vector3 moveDirection = closestPlayerToy.transform.position - transform.position;
			moveDirection.y = 0;
			moveDirection.Normalize();

			toy.npcHorizontal = moveDirection.x;
			toy.npcVertical = moveDirection.z;
		}

		if ( moveState == MoveState.Attack )
		{
			toy.npcAttacking = true;
		}
		else
		{
			toy.npcAttacking = false;
		}
	}


}
