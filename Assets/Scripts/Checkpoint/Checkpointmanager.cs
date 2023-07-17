using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
	public static Vector3 lastCheckpointPosition;
	public static GameStateData gameStateData;

	private void Start()
	{
		// Recuperar la posición del último Checkpoint de PlayerPrefs
		float lastCheckpointX = PlayerPrefs.GetFloat("LastCheckpointX", 0f);
		float lastCheckpointY = PlayerPrefs.GetFloat("LastCheckpointY", 0f);
		lastCheckpointPosition = new Vector3(lastCheckpointX, lastCheckpointY, 0f);
	}
}
