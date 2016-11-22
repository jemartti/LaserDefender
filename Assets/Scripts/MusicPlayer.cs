using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MusicPlayer : MonoBehaviour
{
	static MusicPlayer instance = null;
	public AudioClip startClip;
	public AudioClip gameClip;
	public AudioClip endClip;
	private AudioSource music;

	void Awake ()
	{
		if (instance != null && instance != this) {
			Destroy (gameObject);
			print ("Duplicate music player self-destructing!");
		} else {
			instance = this;
			GameObject.DontDestroyOnLoad (gameObject);
			music = GetComponent<AudioSource> ();
		}
	}

	void OnEnable ()
	{
		SceneManager.sceneLoaded += OnLevelFinishedLoading;
	}

	void OnDisable ()
	{
		SceneManager.sceneLoaded -= OnLevelFinishedLoading;
	}

	void OnLevelFinishedLoading (Scene scene, LoadSceneMode mode)
	{
		music.Stop ();
		if (scene.buildIndex == 0) {
			music.clip = startClip;
		} else if (scene.buildIndex == 1) {
			music.clip = gameClip;
		} else if (scene.buildIndex == 2) {
			music.clip = endClip;
		}
		music.loop = true;
		music.Play ();
	}
}
