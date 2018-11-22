using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Reporting;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class Character : MonoBehaviour {

	[SerializeField] private float _speed;
	protected Vector2 Direction;

	private Animator _animator;

	private void Start ()
	{
		_animator = GetComponent<Animator>();
	}
	
	protected virtual void Update ()
	{
		Move();
	}
	
	public void Move()
	{
		transform.Translate(Direction * _speed * Time.deltaTime);
		
		if (Direction.x != 0 || Direction.y != 0) //player is moving
		{
			AnimateMovement(Direction);
		}
		else
		{
			_animator.SetLayerWeight(1,0);
		}
		
		
	}

	public void AnimateMovement(Vector2 direction)
	{
		_animator.SetLayerWeight(1,1);
		
		//set animation params so player faces correct direction
		_animator.SetFloat("horizontal", direction.x);
		_animator.SetFloat("vertical", direction.y);
	}
}
