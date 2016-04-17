using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {

	private float _second = 1.0f;
	private float _current = 0.0f;

	public bool InCutscene = false;

	public List<GameObject> LevelSpawnLocations;

	private GameObject _player;

	// Use this for initialization
	void Start () {
		_player = GameObject.FindGameObjectWithTag("MainCharacter");
	}
	
	// Update is called once per frame
	void Update () {
		SecondCheck();
	}

	void SecondCheck(){
		if(!InCutscene){
			if(_current >= _second){
				GameManager.Instance.AddSeconds(1);
				_current = 0.0f;
			}
			else{
				_current += Time.deltaTime;
			}
		}
	}

	public void MovePlayerToSpawn(){
		_player.transform.position = LevelSpawnLocations[GameManager.Instance.CurrentLevel].transform.position;
	}
}
