using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class GameManager : Singleton<GameManager> {
	protected GameManager(){}

	//Audio Options
	public float MusicVolume {get; private set;}
	private float _musicVolume; //May not need this...
	public float SfxVolume {get; private set;}
	private float _sfxVolume; //May not need this...

	public Vector3 LastCheckpoint {get; private set;}
	public int Lives {get; private set;}
	public int Score {get; private set;}
	public int Collectables {get; private set;}
	public int HiddenCollectables {get; private set;}

	#region World Setup
	public void SetupWorld(){

		//Probably should safe here...
		LoadLevel(3);
	}
	#endregion

	#region Update In Game
	public void UpdateCheckpoint(Vector3 newCheckpoint){
		LastCheckpoint = newCheckpoint;
	}

	public void UpdateLives(int amount, bool isTaking){
		if(!isTaking){
			Lives += amount;
		}
		else{
			Lives -= amount;
		}
	}

	public void UpdateScore(int amount, bool isTaking){
		if(!isTaking){
			Score += amount;
		}
		else{
			Score += amount;
		}
	}

	public void UpdateCollectables(int amount, bool isTaking){
		if(!isTaking){
			Collectables += amount;
		}
		else {
			Collectables -= amount;
		}
	}

	public void UpdateHiddenCollectables(int amount, bool isTaking){
		if(!isTaking){
			HiddenCollectables += amount;
		}
		else {
			HiddenCollectables -= amount;
		}
	}

	#endregion

	#region Update Audio
	//Audio Settings
	public void UpdateMusicVolume(float music){
		MusicVolume = music;
	}

	public void UpdateSfxVolume(float sfx){
		SfxVolume = sfx;
	}
	#endregion

	#region Audio Stuff
	public void MuteMusic(){
		if(MusicVolume == 0.0f){
			MusicVolume = _musicVolume;
			_musicVolume = 0.0f;
		}
		else{
			_musicVolume = MusicVolume;
			MusicVolume = 0.0f;
		}
	}

	public void MuteSound(){
		if(SfxVolume == 0.0f){
			SfxVolume = _sfxVolume;
			_sfxVolume = 0.0f;
		}
		else{
			_sfxVolume = MusicVolume;
			SfxVolume = 0.0f;
		}
	}
	#endregion

	#region Level Loading Stuff
	public void LoadLevel(int sceneNumber){
		//Start the coroutine.
		StartCoroutine(LoadNewScene(sceneNumber));
	}

	IEnumerator LoadNewScene(int sceneNumber){
		AsyncOperation loading = SceneManager.LoadSceneAsync(sceneNumber);
		loading.allowSceneActivation = true;
		while(!loading.isDone){
			yield return null;
		}
	}
	#endregion
}
