using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
	public float scrollSpeed;
	private float tileSizeY;

	private Vector3 startPosition;

	void Start()
	{
		startPosition = transform.position;
		tileSizeY = transform.localScale.x/10;
	}

	void Update()
	{
		float newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileSizeY);
		transform.position = startPosition - Vector3.up * newPosition;
	}
}
