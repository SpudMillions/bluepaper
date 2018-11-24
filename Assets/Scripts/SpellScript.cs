using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellScript : MonoBehaviour
{

	private Rigidbody2D _myRigidBody;

	[SerializeField] private float speed;

	public Transform MyTarget;

	// Use this for initialization
	void Start ()
	{
		_myRigidBody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void FixedUpdate()
	{
		if (MyTarget != null)
		{
			Vector2 direction = MyTarget.position - transform.position;

			_myRigidBody.velocity = direction.normalized * speed;

			var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		
			transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		}
		
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "HitBox" && collision.transform == MyTarget)
		{
			GetComponent<Animator>().SetTrigger("Impact");
			_myRigidBody.velocity = Vector2.zero;
			MyTarget = null;
		}
	}
}
