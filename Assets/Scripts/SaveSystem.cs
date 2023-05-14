using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

using System;

[Serializable]
public struct SaveData
{
	public string playerName;
	public int score;
	public Vector3 position;
}


public static class SaveSystem
{
	public static void SaveGame(SaveData data)
	{
		BinaryFormatter formatter = new BinaryFormatter();
		string savePath = Application.persistentDataPath + "/save.dat";

		FileStream stream = new FileStream(savePath, FileMode.Create);

		formatter.Serialize(stream, data);
		stream.Close();
	}

	public static SaveData LoadGame()
	{
		string savePath = Application.persistentDataPath + "/save.dat";

		if (File.Exists(savePath))
		{
			BinaryFormatter formatter = new BinaryFormatter();

			FileStream stream = new FileStream(savePath, FileMode.Open);

			SaveData data = (SaveData)formatter.Deserialize(stream);
			stream.Close();

			return data;
		}
		else
		{
			Debug.LogWarning("No save file found.");
			return new SaveData();
		}
	}
	
	
	/*
	void OnApplicationQuit()
	{
	SaveData data = new SaveData();
	data.playerName = "John";
	data.score = 100;
	data.position = transform.position;

	SaveSystem.SaveGame(data);
	}
	*/
}
