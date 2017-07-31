using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toy : MonoBehaviour, IHittable 
{
	public Camera activeCamera;
	public CharacterController controller;
	public float walkSpeed = 1;
	public GameObject modelObject;
	public Animator modelAnimator;
	public Knob knob;
	public float moveDistancePerTurn = 10;
	public float windUpSpeed = 1;
	public GameObject selectionObject;
	public GameObject healthBarPrefab;
	public Transform healthBarOrigin;
	public NPCToyController npcController;


	public Transform controlledCameraTargetParent;
	public Transform controlledCameraTarget;
	public Transform controlledLookTarget;
	public Transform selectedCameraTarget;
	public Transform selectedLookTarget;
	public Transform enemyFollowTarget;

	// non player character options
	public bool isNPC;
	public float npcHorizontal;
	public float npcVertical;
	public bool npcAttacking;

	public DeathSequence deathSequence;

	public State state;

	public bool isPaused = false;

	protected bool isAttacking = false;

	protected float minVerticalSpeed = -20;
	protected float verticalSpeed = 0;

	protected HealthBar healthBar;
	protected bool isDead = false;

	public enum State
	{
		Waiting,
		Selected,
		Controlled,
		OtherIsControlled
	}

	void Start () { CustomStart(); }
	void Update () { CustomUpdate(); }

	protected virtual void CustomStart()
	{
		knob.power = 1.0f;
		GameObject instance = Instantiate( healthBarPrefab );
		instance.transform.parent = healthBarOrigin;
		instance.transform.localPosition = Vector3.zero;
		instance.transform.localEulerAngles = Vector3.zero;

		healthBar = instance.GetComponent<HealthBar>();
	}

	protected virtual void CustomUpdate()
	{
		if ( healthBar.damage >= healthBar.hitPoints )
		{
			if ( !isDead )
			{
				deathSequence.BeginDeathSequence();
				isDead = true;
			}
		}

		float horizontal = Input.GetAxis( "Horizontal" );
		float vertical = Input.GetAxis( "Vertical" );

		Vector2 smoothedInput = new Vector2( horizontal, vertical );
		Vector2 rawInput = new Vector2( Input.GetAxisRaw( "Horizontal" ), Input.GetAxisRaw( "Vertical" ) );

		Vector3 cameraForward;
		Vector3 cameraRight;
		if ( !isNPC )
		{
			cameraForward = activeCamera.transform.forward;
			cameraRight = activeCamera.transform.right;
		}
		cameraForward.y = 0;
		cameraForward.Normalize();

		cameraRight.y = 0;
		cameraRight.Normalize();

		Vector3 moveDirection = cameraForward * vertical + cameraRight * horizontal;

		verticalSpeed -= Time.deltaTime * 10;
		if ( verticalSpeed < minVerticalSpeed )
		{
			verticalSpeed = minVerticalSpeed;
		}

		if ( controller.isGrounded )
		{
			verticalSpeed = 0;
		}

		if ( isNPC )
		{
			moveDirection = new Vector3( npcHorizontal, 0, npcVertical );
		}

		if ( state == State.Controlled && !isPaused && !isDead )
		{
			isAttacking = Input.GetButton( "Attack" );

			if ( isNPC )
			{
				isAttacking = npcAttacking;
			}

			if ( isAttacking )
			{
				modelAnimator.SetBool( "isAttacking", true );

				AttackUpdate();
			}
			else
			{
				modelAnimator.SetBool( "isAttacking", false );

				if ( knob.power > 0.0f )
				{
					Vector3 initialPosition = transform.position;
					initialPosition.y = 0;

					Vector3 moveAmount = moveDirection;
					moveAmount.y = verticalSpeed;
					moveAmount = moveAmount * Time.deltaTime * walkSpeed;

					controller.Move( moveAmount );

					Vector3 newPosition = transform.position;
					newPosition.y = 0;

					float distanceMoved = Vector3.Distance( initialPosition, newPosition );

					// always drain some amount even if the enemy gets stuck
					if ( isNPC && !npcAttacking )
					{
						//Debug.Log( distanceMoved / Time.deltaTime );

						float slowestDrainSpeed = 3f;
						if ( distanceMoved / Time.deltaTime < slowestDrainSpeed )
						{
							//Debug.Log( "Slow Drain" );
							distanceMoved = slowestDrainSpeed * Time.deltaTime;
						}
					}

					knob.power -= distanceMoved / moveDistancePerTurn;



					if ( !isNPC )
					{
						EnemyToyManager.availableWindingPower += distanceMoved / moveDistancePerTurn;
					}

					float animationSpeed = Mathf.Clamp( smoothedInput.magnitude, 0, 1 );

					if ( isNPC )
					{
						animationSpeed = Mathf.Clamp( moveDirection.magnitude, 0, 1 );
					}

					modelAnimator.SetFloat( "speed", animationSpeed );
				}
				else
				{
					modelAnimator.SetFloat( "speed", 0 );

				}

				if ( isNPC )
				{
					if ( moveDirection.magnitude > 0.1f )
					{
						modelObject.transform.rotation = Quaternion.LookRotation( moveDirection );
					}
				}
				else if ( rawInput.magnitude > 0.1f )
				{
					modelObject.transform.rotation = Quaternion.LookRotation( moveDirection );
				}
			}

			// update the camera target based on mouse movement
			if ( !isNPC )
			{
				float mouseHorizontal = Input.GetAxis( "Mouse X" );

				float angle = controlledCameraTargetParent.localEulerAngles.y;
				angle += mouseHorizontal * Time.deltaTime * 50;

				controlledCameraTargetParent.localEulerAngles = new Vector3( 0, angle, 0 );
			}
		}
		else 
		{
			modelAnimator.SetFloat( "speed", 0 );
			modelAnimator.SetBool( "isAttacking", false );
		}
	}

	public void ResetControlledCameraTarget()
	{
		controlledCameraTargetParent.localEulerAngles = new Vector3( 0, modelObject.transform.localEulerAngles.y, 0 );
	}

	protected virtual void AttackUpdate()
	{

	}

	public void WindUp()
	{
		knob.power += windUpSpeed * Time.deltaTime;


	}

	public virtual void SetAsEnemy()
	{
		isNPC = true;
		selectionObject.SetActive( false );
		windUpSpeed = 0.1f;
	}

	public void Hit( HitInfo theHit )
	{
		healthBar.damage += theHit.damage;

		Vector3 difference = transform.position - theHit.origin;
		difference.y = 0;
		Vector3 moveDirection = difference.normalized;

		controller.Move( moveDirection * Time.deltaTime );

	}

	public void RemoveFromGame()
	{
		if ( isNPC )
		{
			EnemyToyManager.instance.toys.Remove( this );
		}
		else
		{
			ToyManager.sharedInstance.toys.Remove( this );
			ToyManager.sharedInstance.ResetSelection();
		}
	}

}
