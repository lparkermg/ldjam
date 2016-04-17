using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	private float _second = 1.0f;
	private float _current = 0.0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		SecondCheck();
	}

	void SecondCheck(){
		if(_current >= _second){
			GameManager.Instance.AddSeconds(1);
			_current = 0.0f;
		}
		else{
			_current += Time.deltaTime;
		}
	}
}
