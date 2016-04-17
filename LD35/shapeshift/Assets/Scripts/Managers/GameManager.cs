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

	public TimeSpan TimeTaken {get; private set;}
	public int Score {get; private set;}
	public int Collectables {get; private set;}
	public int HiddenCollectables {get; private set;}

	public bool IsBusted {get; private set;}

	#region World Setup
	public void SetupWorld(){

		//Probably should safe here...
		LoadLevel(3);
	}
	#endregion

	#region Update In Game
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

	public void UpdateIsBusted(bool isBusted){
		IsBusted = isBusted;
	}

	public void AddSeconds(int amount){
		TimeTaken = TimeTaken.Add(new TimeSpan(0,0,amount));
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
