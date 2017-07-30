using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyToyManager : MonoBehaviour 
{
	public static EnemyToyManager instance;

	public ToyManager playerToyManager;

	public List<Toy> toys;

	private int selectedToyIndex = 0;

	public static float availableWindingPower = 0;

	void Awake()
	{
		instance = null;
		instance = this;

		availableWindingPower = 0;
	}

	void Start()
	{
		playerToyManager = ToyManager.sharedInstance;
	}

	void Update()
	{

		if ( availableWindingPower < 0 )
		{
			availableWindingPower = 0;
		}
			
		if ( toys == null || toys.Count <= 0 )
		{
			return;
		}

		if ( playerToyManager.toys == null || playerToyManager.toys.Count <= 0 )
		{
			return;
		}


		if ( playerToyManager.state == ToyManager.State.ZoomedOut )
		{
			foreach( Toy toy in toys )
			{
				toy.npcController.moveState = NPCToyController.MoveState.Still;
			}
		}
		if ( playerToyManager.state == ToyManager.State.ControllingToy )
		{
			// the player is controlling a toy, so we should wind up now

			// wind up the toy that is the closest to a player now
			Toy closestToPlayer = toys[0];

			float closestDistance = 1000;

			foreach( Toy toy in toys )
			{
				toy.npcController.moveState = NPCToyController.MoveState.Still;

				if ( toy.knob.power < 1 )
				{
					if ( toy.npcController.distanceToClosestPlayerToy < closestDistance )
					{
						closestToPlayer = toy;
						closestDistance = toy.npcController.distanceToClosestPlayerToy;
					}
				}
			}

			closestToPlayer.WindUp();

		}
		else if ( playerToyManager.state == ToyManager.State.SelectingToy )
		{
			// the player is winding up now, so we should be moving and attacking now

			// get the closest toy to a player that has power
			Toy closestToPlayer = toys[0];

			float closestDistance = 1000;

			foreach( Toy toy in toys )
			{
				toy.npcController.moveState = NPCToyController.MoveState.Still;

				if ( toy.knob.power > 0.0f )
				{
					if ( toy.npcController.distanceToClosestPlayerToy <= closestDistance )
					{
						closestToPlayer = toy;
						closestDistance = toy.npcController.distanceToClosestPlayerToy;
					}
				}
			}

			// if we are closer enough we should attack, otherwise move towards the closest player
			if ( closestToPlayer.npcController.distanceToClosestPlayerToy < 3 )
			{
				closestToPlayer.npcController.moveState = NPCToyController.MoveState.Attack;
			}
			else
			{
				closestToPlayer.npcController.moveState = NPCToyController.MoveState.TowardsClosestPlayer;
			}

		}
	}
}
