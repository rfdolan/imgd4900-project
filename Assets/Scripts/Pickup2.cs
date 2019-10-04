using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Pickup2 : MonoBehaviour {
	private Vector3 objectPos;
	private float distance;
	public float holdDistance = 0.25F;
	public Transform tempParent;
	public bool isHolding = false;
	private Rigidbody itemRB;
	private NavMeshAgent agent;
	void Start() {
		itemRB = GetComponent<Rigidbody>();
	}
	void Update() {
		float moveStep = 15 * Time.deltaTime;
		float rotationStep = 120 * Time.deltaTime;
		distance = Vector3.Distance(transform.position, tempParent.transform.position);
		if (distance >= holdDistance) {
			isHolding = false;
		}
		if (Input.GetKeyDown(KeyCode.E)) HoldUnHold();
		if (isHolding) {
			itemRB.velocity = Vector3.zero;
			itemRB.angularVelocity = Vector3.zero;
			transform.SetParent(tempParent.transform);
			Vector3 tempForward = (tempParent.transform.parent.position - tempParent.transform.position).normalized;
			transform.localPosition = Vector3.MoveTowards(transform.localPosition, Vector3.zero, moveStep);
			transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.identity, rotationStep);
		}
		else {
			objectPos = transform.position;
			transform.SetParent(null);
			itemRB.useGravity = true;
			transform.position = objectPos;
		}
	}
	public void HoldUnHold() {
		if (isHolding) unHold();
		else Hold();
	}
	public void Hold() {
		if (distance <= holdDistance ) {
			isHolding = true;
			itemRB.useGravity = false;
			itemRB.detectCollisions = true;
		}
	}
	public void unHold() {
		isHolding = false;
	}
}