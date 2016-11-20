using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
	public GameObject enemyPrefab;
	public float width = 5f;
	public float height = 5f;
	public float speed = 25f;
	private float xMin;
	private float xMax;

	void Start ()
	{
		// Calculate playspace boundaries
		float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftBoundary = Camera.main.ViewportToWorldPoint (new Vector3 (0f, 0f, distanceToCamera));
		Vector3 rightBoundary = Camera.main.ViewportToWorldPoint (new Vector3 (1f, 0f, distanceToCamera));
		xMin = leftBoundary.x + (0.5f * width);
		xMax = rightBoundary.x - (0.5f * width);

		// Spawn enemies in position
		foreach (Transform child in transform) {
			GameObject enemy = Instantiate (enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = child;
		}
	}

	void OnDrawGizmos ()
	{
		Gizmos.DrawWireCube (transform.position, new Vector3 (width, height, 0f));
	}

	void Update ()
	{
		// Restrict formation to the playspace
		transform.position = new Vector3 (
			Mathf.Clamp (transform.position.x + (speed * Time.deltaTime), xMin, xMax),
			transform.position.y,
			transform.position.z
		);

		// Switch directions when we hit the edges
		if (transform.position.x <= xMin || transform.position.x >= xMax) {
			speed *= -1;
		}
	}
}
