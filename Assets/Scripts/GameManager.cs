using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{

	[SerializeField] private Player _player;

	private NPC currentTarget;
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
				if (currentTarget != null)
				{
					currentTarget.DeSelect();
				}

				currentTarget = hit.collider.GetComponent<NPC>();

				_player.MyTarget = currentTarget.Select();
				UIManager.Instance.ShowTargetFrame(currentTarget);
			}
			else
			{
				UIManager.Instance.HideTargetFrame();
				if (currentTarget != null)
				{
					currentTarget.DeSelect();
					
				}

				currentTarget = null;
				_player.MyTarget = null;
			}
		}	
	}
}
