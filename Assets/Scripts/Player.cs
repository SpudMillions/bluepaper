using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : Character
{
	[SerializeField] private Stat _health;
	[SerializeField] private Stat _mana;
	private float _initialMana = 100;
	private float _initialHealth = 100;

	[SerializeField] GameObject[] spellPrefab;
	[SerializeField] Transform[] exitPoints;
	private int exitIndex = 2;
	[SerializeField]private Block[] blocks;
	public Transform MyTarget { get; set; }
	
	protected override void Start()
	{
		
		_health.Initialize(_initialHealth,_initialHealth);
		_mana.Initialize(_initialMana, _initialMana);
		base.Start();
	}

	protected override void Update ()
	{
		GetInput();
		base.Update();
		
	}
	
	
	private void GetInput()
	{
		Direction = Vector2.zero;
		
		if (Input.GetKey(KeyCode.W))
		{
			exitIndex = 0;
			Direction += Vector2.up;
		}
		if (Input.GetKey(KeyCode.A))
		{
			exitIndex = 3;
			Direction += Vector2.left;
		}
		if (Input.GetKey(KeyCode.S))
		{
			exitIndex = 2;
			Direction += Vector2.down;
		}
		if (Input.GetKey(KeyCode.D))
		{
			exitIndex = 1;
			Direction += Vector2.right;
		}

		if (Input.GetKeyDown(KeyCode.Space))
		{
			BlockLineOfSight();
			if (MyTarget != null && !IsAttacking && !IsMoving && InLineOfSight())
			{
				AttackRoutine = StartCoroutine(Attack());
			}
		}
	}

	private IEnumerator Attack()
	{
			IsAttacking = true;
			Animator.SetBool("attack",IsAttacking);

			yield return new WaitForSeconds(3); //hard coded cat time, change later	
			
			CastSpell();
		
			StopAttack();	
	}

	public void CastSpell()
	{
		Instantiate(spellPrefab[0], exitPoints[exitIndex].position, Quaternion.identity);
	}

	private bool InLineOfSight()
	{
		Vector3 targetDirection = (MyTarget.position - transform.position).normalized;
		RaycastHit2D hit = Physics2D.Raycast(transform.position,targetDirection,Vector2.Distance(transform.position,MyTarget.transform.position), LayerMask.GetMask("Block"));
		Debug.DrawRay(transform.position,MyTarget.transform.position, Color.red);
		if (hit.collider == null)
		{
			return true;
		}
		return false;
	}

	private void BlockLineOfSight()
	{
		foreach (var block in blocks)
		{
			block.Deactivate();
		}
		
		blocks[exitIndex].Activate();
	}
}
