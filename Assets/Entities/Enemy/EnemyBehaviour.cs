using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour
{
	public GameObject projectile;
	public float projectileSpeed;
	public float shotsPerSecond = 0.5f;
	public float health = 150f;

	void Update ()
	{
		float probability = shotsPerSecond * Time.deltaTime;
		if (Random.value < probability) {
			Fire ();
		}
	}

	void Fire ()
	{
		GameObject missile = Instantiate (projectile, transform.position, Quaternion.identity) as GameObject;
		missile.rigidbody2D.velocity = new Vector3 (0f, projectileSpeed * -1f, 0f);
	}

	void OnTriggerEnter2D (Collider2D collider)
	{
		Projectile missile = collider.gameObject.GetComponent<Projectile> ();
		if (missile) {
			health -= missile.GetDamage ();
			missile.Hit ();
			if (health <= 0) {
				Destroy (gameObject);
			}
		}
	}
}
