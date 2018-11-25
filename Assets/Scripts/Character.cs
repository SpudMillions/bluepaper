using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Reporting;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public abstract class Character : MonoBehaviour {

	[SerializeField] private float _speed;
	protected Vector2 Direction;
	private Rigidbody2D _myRigidBody;
	protected Animator Animator;
	[SerializeField] protected Stat Health;

	public Stat MyHealth
	{
		get { return Health; }
	}
	[SerializeField] float _initialHealth;

	protected bool IsAttacking;
	protected Coroutine AttackRoutine;

	[SerializeField] protected Transform hitBox;

	protected bool IsMoving
	{
		get { return Direction.x != 0 || Direction.y != 0; }
	}

	protected virtual void Start ()
	{
		Health.Initialize(_initialHealth,_initialHealth);
		Animator = GetComponent<Animator>();
		_myRigidBody = GetComponent<Rigidbody2D>();
	}
	
	protected virtual void Update ()
	{
		HandleLayers();
	}

	private void FixedUpdate()
	{
		Move();
	}

	public void Move()
	{
		_myRigidBody.velocity = Direction.normalized * _speed;
	}

	private void HandleLayers()
	{
		if (IsMoving)
		{
			ActivateLayer("WalkLayer");
			
			//set animation params so player faces correct direction
			Animator.SetFloat("horizontal", Direction.x);
			Animator.SetFloat("vertical", Direction.y);
			
			StopAttack();
		}
		else if (IsAttacking)
		{
			ActivateLayer("AttackLayer");
		}
		else
		{
			ActivateLayer("IdleLayer");
		}
	}


	public void ActivateLayer(string layerNameToActivate)
	{
		for (var i = 0; i < Animator.layerCount; i++)
		{
			Animator.SetLayerWeight(i,0);
		}
		
		Animator.SetLayerWeight(Animator.GetLayerIndex(layerNameToActivate),1);
	}

	public virtual void StopAttack()
	{
		if (AttackRoutine != null)
		{
			StopCoroutine(AttackRoutine);
			IsAttacking = false;
			Animator.SetBool("attack", IsAttacking);
		}
	}

	public virtual void TakeDamage(float damage)
	{
		Health.CurrentValue -= damage;

		if (Health.CurrentValue <= 0)
		{
			Animator.SetTrigger("Die");
		}
	}
}
