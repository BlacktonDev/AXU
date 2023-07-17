using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class GameStateData
{
	public Vector3 playerPosition;
	public int vidaMaxima;
	public int vida;
	public bool PUvida;
	//public bool PUDobleSalto;
	public bool PUDobleDanio;
}
public class GameManager : MonoBehaviour
{
	private string filePath;

	private void Awake()
	{
		filePath = Application.persistentDataPath + "/gameStateData.json";
	}

	public void GameSave(GameStateData gameStateData)
	{
		string jsonData = JsonUtility.ToJson(gameStateData);
		File.WriteAllText(filePath, jsonData);
	}

	public GameStateData GameLoad()
	{
		if (File.Exists(filePath))
		{
			string jsonData = File.ReadAllText(filePath);
			GameStateData gameStateData = JsonUtility.FromJson<GameStateData>(jsonData);
			return gameStateData;
		}
		else
		{
			Debug.LogError("No se encontró el archivo de guardado.");
			return null;
		}
	}
}

