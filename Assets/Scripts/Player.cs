using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
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
