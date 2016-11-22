using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour
{
	public GameObject projectile;
	public float projectileSpeed;
	public float shotsPerSecond = 0.5f;
	public float health = 150f;
	public int score = 150;
	public AudioClip fireSound;
	public AudioClip deathSound;
	private ScoreKeeper scoreKeeper;

	void Start ()
	{
		scoreKeeper = GameObject.Find ("Score").GetComponent<ScoreKeeper> ();
	}

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
		missile.GetComponent<Rigidbody2D> ().velocity = new Vector3 (0f, -projectileSpeed, 0f);
		AudioSource.PlayClipAtPoint (fireSound, transform.position);
	}

	void OnTriggerEnter2D (Collider2D collider)
	{
		Projectile missile = collider.gameObject.GetComponent<Projectile> ();
		if (missile) {
			health -= missile.GetDamage ();
			missile.Hit ();
			if (health <= 0) {
				Die ();
			}
		}
	}

	void Die ()
	{
		AudioSource.PlayClipAtPoint (deathSound, transform.position);
		Destroy (gameObject);
		scoreKeeper.Score (score);
	}
}
