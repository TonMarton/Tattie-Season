using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Music Layerr system, the music gets increased with methods 
/// </summary>
public class MusicLayerSystem : MonoBehaviour
{
	public List<AudioSource> musicSources;
	public List<AudioClip> musicLayers;
	[SerializeField] [Range(1f, 10f)] private float transitionInTime = 5f;
	[SerializeField] [Range(1f, 10f)] private float transitionOutTime = 5f;

	[SerializeField] private int currentLayer = 0;//the "first" music layer is layer 0, just for consistency in code 

	private float musicVolume = 1;

	public bool Active { get; private set; }

	private void Awake()
	{
		Active = false;
		CheckIfSourcesAreNull();
		AssignClipsToSources();
	}

	void Update()
	{
		//testing 




		if (Input.GetKeyDown(KeyCode.G)) {
			IncreaseMusicLayer();
		}

		if (Input.GetKeyDown(KeyCode.H)) {
			DecreaseMusicLayer();
		}
		if (Input.GetKeyDown(KeyCode.J)) {
			Debug.Log("pressed");
			StartMusicSystem();
		}
		if (Input.GetKeyDown(KeyCode.K)) {
			StopMusicSystem();
		}

	}

	private IEnumerator FadeInMusicLayer(AudioSource activeSource, float transitionTime)
	{
		activeSource.volume = 0.0f;
		activeSource.mute = false;

		// Fade in
		for (float t = 0.0f; t <= transitionTime; t += Time.deltaTime) {
			activeSource.volume = (t / transitionTime) * musicVolume;
			yield return null;
		}

		// Make sure we don't end up with a weird float value
		activeSource.volume = musicVolume;
	}

	private IEnumerator FadeOutMusicLayer(AudioSource activeSource, float transitionTime)
	{

		float t = 0.0f;

		// Fade out
		for (t = 0.0f; t <= transitionTime; t += Time.deltaTime) {
			activeSource.volume = (musicVolume - ((t / transitionTime) * musicVolume));
			yield return null;
		}

		activeSource.mute = true;


		// Make sure we don't end up with a weird float value
		activeSource.volume = musicVolume;
	}

	public void StartMusicSystem()
	{
		if (Active) return;
		Active = true;

		//assumption is that the first layer will always start, while the rest will be muted
		musicSources[0].Play();
		StartCoroutine(FadeInMusicLayer(musicSources[0], transitionInTime));
		for (int i = 1; i < musicSources.Count; i++) {
			musicSources[i].Play();
			musicSources[i].mute = true;
		}
	}

	public void StopMusicSystem()
	{
		if (!Active) return;
		Active = false;

		StartCoroutine(StopAllMusic());
		currentLayer = 0;
	}

	private IEnumerator StopAllMusic()
	{
		foreach (var source in musicSources) {
			StartCoroutine(FadeOutMusicLayer(source, transitionOutTime));
		}

		//arbitrary small value added to garantee all music has faded out
		yield return new WaitForSeconds(transitionOutTime + 0.05f);
		foreach (var source in musicSources) {
			source.Stop();
		}

	}

	public void IncreaseMusicLayer()
	{
		if (!Active) return;

		if (currentLayer >= musicSources.Count - 1) {
			Debug.Log("Already at maximum music layer");
			return;
		}

		currentLayer++;
		StartCoroutine(FadeInMusicLayer(musicSources[currentLayer], transitionInTime));

	}

	public void DecreaseMusicLayer()
	{
		if (!Active) return;

		if (currentLayer <= 0) {
			Debug.Log("Already at min music layer");
			return;
		}


		StartCoroutine(FadeOutMusicLayer(musicSources[currentLayer], transitionOutTime));
		currentLayer--;
	}

	private void CheckIfSourcesAreNull()
	{
		foreach (var audioSource in musicSources) {
			if (audioSource == null)
				Debug.LogWarning("One of the referenced audio sources is null!");
		}

		foreach (var clip in musicLayers) {
			if (clip == null)
				Debug.LogWarning("One of the referenced audio clips is null!");
		}
	}


	private void AssignClipsToSources()
	{
		for (int i = 0; i < musicSources.Count; i++) {
			musicSources[i].clip = musicLayers[i];
		}
	}

}
