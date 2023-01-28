using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CompleteLevelTrigger : MonoBehaviour
{
	public UnityEvent OnLevelComplete;


	private void OnTriggerEnter2D(Collider2D col)
	{
		if (col.GetComponent<PlayerMovement>()) {
			Debug.Log("completeed the level");
			OnLevelComplete?.Invoke();
		}
	}
}
