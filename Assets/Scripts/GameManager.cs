using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.EventSystems;

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
		if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject() ) //left mouse button
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
					_player.MyTarget = hit.transform.GetChild(0);
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
