using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	public float speed = 15f;
	public float padding = 1f;
	public GameObject projectile;
	public float projectileSpeed;
	public float firingRate = 0.2f;
	public float health = 1000f;
	public AudioClip fireSound;
	private float xMin;
	private float xMax;

	void Start ()
	{
		// Calculate playspace boundaries
		float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftBoundary = Camera.main.ViewportToWorldPoint (new Vector3 (0f, 0f, distanceToCamera));
		Vector3 rightBoundary = Camera.main.ViewportToWorldPoint (new Vector3 (1f, 0f, distanceToCamera));
		xMin = leftBoundary.x + padding;
		xMax = rightBoundary.x - padding;
	}

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Space)) {
			InvokeRepeating ("Fire", 0.000001f, firingRate);
		}
		if (Input.GetKeyUp (KeyCode.Space)) {
			CancelInvoke ("Fire");
		}

		if (Input.GetKey (KeyCode.LeftArrow)) {
			transform.position += Vector3.left * speed * Time.deltaTime;
		} else if (Input.GetKey (KeyCode.RightArrow)) {
			transform.position += Vector3.right * speed * Time.deltaTime;
		}
		// Restrict player to the playspace
		transform.position = new Vector3 (
			Mathf.Clamp (transform.position.x, xMin, xMax),
			transform.position.y,
			transform.position.z
		);
	}

	void Fire ()
	{
		GameObject beam = Instantiate (projectile, transform.position, Quaternion.identity) as GameObject;
		beam.rigidbody2D.velocity = new Vector3 (0f, projectileSpeed, 0f);
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
		LevelManager levelManager = GameObject.Find ("LevelManager").GetComponent<LevelManager> ();
		levelManager.LoadLevel ("Win Screen");
		Destroy (gameObject);
	}
}
