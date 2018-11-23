using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{

	[SerializeField] private Player _player;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		ClickTarget();
	}

	private void ClickTarget()
	{
		if (Input.GetMouseButtonDown(0)) //left mouse button
		{
			RaycastHit2D hit = Physics2D.Raycast
				(
					Camera.main.ScreenToWorldPoint(Input.mousePosition), 
					Vector2.zero,
					Mathf.Infinity,
					LayerMask.GetMask("Clickable")
				);

			if (hit.collider != null)
			{
				if (hit.collider.tag == "Enemy")
				{
					_player.MyTarget = hit.transform;
				}
			}
			else
			{
				//detarget
				_player.MyTarget = null;
			}
		}	
	}
}
