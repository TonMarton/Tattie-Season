using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyPlayerDetection : MonoBehaviour
{
	private float timer = 0;
	private bool canBeHurt = true;
	private float cooldownTime = 0.75f;
	private void Update()
	{
		timer += Time.deltaTime;

		if (timer > cooldownTime) {
			canBeHurt = true;
			timer = 0;
		}

	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		var player = col.GetComponent<PlayerStats>();
		if (player) {
			if (canBeHurt == false) return;
			canBeHurt = false;
			player.TakeDamage(1);
		}
	}
}
