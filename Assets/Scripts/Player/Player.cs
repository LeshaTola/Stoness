using System;
using UnityEngine;

public class Player : Creature {

	[SerializeField] private GameInput gameInput;

	private void Start() {
		Init();

		gameInput.OnTurnLeft += GameInput_TurnLeft;
		gameInput.OnTurnRight += GameInput_TurnRight;
		gameInput.OnAttack += GameInput_Attack;
	}

	private void OnDisable() {
		gameInput.OnTurnLeft -= GameInput_TurnLeft;
		gameInput.OnTurnRight -= GameInput_TurnRight;
		gameInput.OnAttack -= GameInput_Attack;
	}

	private void GameInput_Attack(object sender, System.EventArgs e) {
		if (attackTimer <= 0) {
			Attack();
		}
	}

	private void Attack() {
		//Анимация
		Debug.Log("Attack");
		attackTimer = attackCooldown;
	}

	private void Update() {
		attackTimer -= Time.time;
		Vector2Int moveDirection = Vector2Int.zero;
		if (gameInput.OnMoveForward()) {
			moveDirection = GetVector2IntPosition(transform.position + transform.forward);
		}
		else if (gameInput.OnMoveBack()) {
			moveDirection = GetVector2IntPosition(transform.position - transform.forward);
		}
		else if (gameInput.OnMoveLeft()) {
			moveDirection = GetVector2IntPosition(transform.position - transform.right);
		}
		else if (gameInput.OnMoveRight()) {
			moveDirection = GetVector2IntPosition(transform.position + transform.right);
		}

		if (moveDirection!= Vector2Int.zero) {
			//movement.SetTargetTile(World.GetTileFromPosition(moveDirection));
			movement.SetTargetPosition(moveDirection);
		}
	}

	private void GameInput_TurnRight(object sender, System.EventArgs e) {
		movement.SetTargetRotation(90f);
	}

	private void GameInput_TurnLeft(object sender, System.EventArgs e) {
		movement.SetTargetRotation(-90f);
	}
}