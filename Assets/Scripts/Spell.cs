using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{

	private Rigidbody2D _myRigidBody;

	[SerializeField] private float speed;

	private Transform _target;
	// Use this for initialization
	void Start ()
	{
		_myRigidBody = GetComponent<Rigidbody2D>();
		
		//just to test
		_target = GameObject.Find("Target").transform;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void FixedUpdate()
	{
		Vector2 direction = _target.position - transform.position;

		_myRigidBody.velocity = direction.normalized * speed;

		var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
	}
}
