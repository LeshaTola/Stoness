using UnityEngine;

public abstract class Creature : MonoBehaviour {
	[Header("��������� ��������")]
	[SerializeField] protected float moveSpeed;
	[SerializeField] protected float rotationSpeed;
	[SerializeField] protected float health;
	[SerializeField] protected float damageAttack;
	[SerializeField] protected float timeBetweenMoves;
	[SerializeField] protected float timeBetweenAttacks;


	protected Tile targetTile;
	protected Tile currentTile;
	protected Vector3 targetRotation;

	protected float moveTimer;
	protected float attackTimer;

	protected void Move() {

		if (targetRotation.y < 0) targetRotation.y = 270f;
		if (targetRotation.y >= 360) targetRotation.y = 0;

		transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetTile.Position.x, transform.position.y, targetTile.Position.y), Time.deltaTime * moveSpeed);
		transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(targetRotation),
		Time.deltaTime * rotationSpeed);
	}

	/*	protected bool IsPositionEmpty(Vector2Int position) {
			return World.GetTileFromPosition(position).isWalkable();
		}*/

	protected bool IsMoving() {
		if (targetTile != null) {
			return new Vector3(targetTile.Position.x, transform.position.y, targetTile.Position.y) != transform.position;
		}
		return false;
	}

	protected bool IsRotating() {
		return targetRotation != transform.eulerAngles;
	}

	protected virtual void SetTargetTile(Tile targetTile) {
		if (!IsMoving() && !IsRotating()) {
			if (targetTile.isWalkable()) {
				if (currentTile != null) {
					currentTile.Occupied = false;
				}

				this.targetTile = targetTile;
				currentTile = targetTile;
				currentTile.Occupied = true;
				moveTimer = timeBetweenMoves;
			}
		}
	}
	protected Vector2Int GetVector2IntPositionToMove(Vector3 direction) {
		return new Vector2Int(Mathf.RoundToInt(direction.x), Mathf.RoundToInt(direction.z));
	}
}
