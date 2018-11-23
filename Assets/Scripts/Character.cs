using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Reporting;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class Character : MonoBehaviour {

	[SerializeField] private float _speed;
	protected Vector2 Direction;
	private Rigidbody2D _myRigidBody;
	private Animator _animator;

	public bool IsMoving
	{
		get { return Direction.x != 0 || Direction.y != 0; }
	}

	protected virtual void Start ()
	{
		_animator = GetComponent<Animator>();
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
			_animator.SetFloat("horizontal", Direction.x);
			_animator.SetFloat("vertical", Direction.y);
		}
		else
		{
			ActivateLayer("IdleLayer");
		}
	}


	public void ActivateLayer(string layerNameToActivate)
	{
		for (int i = 0; i < _animator.layerCount; i++)
		{
			_animator.SetLayerWeight(i,0);
		}
		
		_animator.SetLayerWeight(_animator.GetLayerIndex(layerNameToActivate),1);
	}
}
