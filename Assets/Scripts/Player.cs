using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : Character
{
	[SerializeField] private Stat _health;
	[SerializeField] private Stat _mana;
	private float _initialMana = 100;
	private float _initialHealth = 100;
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
		/// <summary>
		/// DEBUG ONLY
		/// </summary>
		if(Input.GetKeyDown(KeyCode.I))
		{
			_health.CurrentValue -= 10;
		}
		else if (Input.GetKeyDown(KeyCode.O))
		{
			_health.CurrentValue += 10;
		}
	}
	
	
	private void GetInput()
	{
		Direction = Vector2.zero;
		
		if (Input.GetKey(KeyCode.W))
		{
			Direction += Vector2.up;
		}
		if (Input.GetKey(KeyCode.A))
		{
			Direction += Vector2.left;
		}
		if (Input.GetKey(KeyCode.S))
		{
			Direction += Vector2.down;
		}
		if (Input.GetKey(KeyCode.D))
		{
			Direction += Vector2.right;
		}
	}
}
