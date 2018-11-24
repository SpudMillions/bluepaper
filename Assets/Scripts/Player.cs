using System;
using System.Collections;
using System.Collections.Generic;
using Boo.Lang.Environments;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : Character
{
	[SerializeField] private Stat _health;
	[SerializeField] private Stat _mana;
	private float _initialMana = 100;
	private float _initialHealth = 100;

	[SerializeField] Transform[] exitPoints;
	private int exitIndex = 2;
	[SerializeField]private Block[] blocks;
	public Transform MyTarget { get; set; }
	private SpellBook spellbook;
	
	protected override void Start()
	{
		
		_health.Initialize(_initialHealth,_initialHealth);
		_mana.Initialize(_initialMana, _initialMana);
		spellbook = GetComponent<SpellBook>();
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
	}

	private IEnumerator Attack(int spellIndex)
	{
			Transform currentTarget = MyTarget;
			Spell spell = spellbook.CastSpell(spellIndex);
			IsAttacking = true;
			Animator.SetBool("attack",IsAttacking);

			yield return new WaitForSeconds(spell.CastTime);

			if (currentTarget != null && InLineOfSight())
			{
				SpellScript spellScript = Instantiate(spell.SpellPrefab, exitPoints[exitIndex].position, Quaternion.identity).GetComponent<SpellScript>();
				spellScript.MyTarget = currentTarget;
			}
			
			StopAttack();	
	}

	public void CastSpell(int spellIndex)
	{
		BlockLineOfSight();
		if (MyTarget != null && !IsAttacking && !IsMoving && InLineOfSight())
		{
			AttackRoutine = StartCoroutine(Attack(spellIndex));
		}
		
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

	public override void StopAttack()
	{
		spellbook.StopCasting();
		
		base.StopAttack();
	}
}
