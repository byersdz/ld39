﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyManager : MonoBehaviour 
{
	public List<Toy> toys;

	public ToyCamera toyCamera;

	public State state;

	private int selectedToyIndex = 0;

	private bool left = false;
	private bool lastLeft = false;

	private bool right = false;
	private bool lastRight = false;

	private State cachedStateFromDeath;
	private ToyCamera.State cachedCameraStateFromDeath;

	public static ToyManager sharedInstance;

	public enum State
	{
		SelectingToy,
		ControllingToy,
		ZoomedOut,
		DeathAnimation
	}

	void Awake()
	{
		sharedInstance = null;
		sharedInstance = this;
	}

	// Use this for initialization
	void Start () 
	{
		foreach ( Toy toy in toys )
		{
			toy.state = Toy.State.Waiting;
		}

		toys[selectedToyIndex].state = Toy.State.Selected;
		toyCamera.state = ToyCamera.State.ToyIsSelected;
		toyCamera.currentToyTarget = toys[selectedToyIndex];

		state = State.SelectingToy;

		SelectToy( 0 );
	}

	public void BeginDeathAnimation()
	{
		cachedStateFromDeath = state;
		cachedCameraStateFromDeath = toyCamera.state;

		state = State.DeathAnimation;
		toyCamera.state = ToyCamera.State.DeathAnimation;

		foreach( Toy toy in toys )
		{
			toy.isPaused = true;
		}
	}

	public void EndDeathAnimation()
	{
		state = cachedStateFromDeath;
		toyCamera.state = cachedCameraStateFromDeath;

		foreach( Toy toy in toys )
		{
			toy.isPaused = false;
		}

	}
	
	// Update is called once per frame
	void Update () 
	{
		lastLeft = left;
		lastRight = right;

		if ( Input.GetAxisRaw( "Horizontal" ) < -0.5f )
		{
			left = true;
			right = false;
		}
		else if ( Input.GetAxisRaw( "Horizontal" ) > 0.5f )
		{
			left = false;
			right = true;
		}
		else
		{
			left = false;
			right = false;
		}

		if ( state == State.ZoomedOut )
		{
			if ( Input.GetButtonDown( "Select" ) )
			{
				state = State.SelectingToy;

				toyCamera.state = ToyCamera.State.ToyIsSelected;
			}
			else if ( Input.GetButton( "Attack" ) )
			{
				toys[selectedToyIndex].WindUp();
			}
			else
			{
				if ( left && !lastLeft )
				{
					SelectToy( selectedToyIndex - 1 );
				}
				else if ( right && !lastRight )
				{
					SelectToy( selectedToyIndex + 1 );
				}
			}


		}
		else if ( state == State.SelectingToy )
		{
			if ( Input.GetButtonDown( "Select" ) )
			{
				state = State.ControllingToy;

				foreach( Toy toy in toys )
				{
					toy.state = Toy.State.OtherIsControlled;
				}

				toys[selectedToyIndex].state = Toy.State.Controlled;
				toys[selectedToyIndex].selectionObject.SetActive( false );

				toyCamera.state = ToyCamera.State.ToyIsControlled;
			}
			else if ( Input.GetButtonDown( "Back" ) )
			{
				state = State.ZoomedOut;
				toyCamera.state = ToyCamera.State.ZoomedOut;
			}
			else if ( Input.GetButton( "Attack" ) )
			{
				toys[selectedToyIndex].WindUp();
			}
			else
			{
				if ( left && !lastLeft )
				{
					SelectToy( selectedToyIndex - 1 );
				}
				else if ( right && !lastRight )
				{
					SelectToy( selectedToyIndex + 1 );
				}
			}
		}
		else if ( state == State.ControllingToy )
		{
			if ( Input.GetButtonDown( "Back" ) )
			{
				state = State.SelectingToy;

				foreach( Toy toy in toys )
				{
					toy.state = Toy.State.Waiting;
				}

				toys[selectedToyIndex].state = Toy.State.Selected;
				toys[selectedToyIndex].selectionObject.SetActive( true );

				toyCamera.state = ToyCamera.State.ToyIsSelected;


			}
		}
			
	}

	private void SelectToy( int theIndex )
	{
		selectedToyIndex = theIndex;

		if ( selectedToyIndex < 0 )
		{
			selectedToyIndex = toys.Count - 1;
		}
		else if ( selectedToyIndex >= toys.Count )
		{
			selectedToyIndex = 0;
		}

		toyCamera.currentToyTarget = toys[selectedToyIndex];

		foreach( Toy toy in toys )
		{
			toy.state = Toy.State.Waiting;
			toy.selectionObject.SetActive( false );
		}

		toys[selectedToyIndex].state = Toy.State.Selected;
		toys[selectedToyIndex].selectionObject.SetActive( true );

	}
}