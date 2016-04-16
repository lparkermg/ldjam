using UnityEngine;
using System.Collections;

public class TimedChange : MonoBehaviour {

	public float MaxWaitTime = 2.0f;
	public int SceneNumber = 1;
	private float _currentTime = 0.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(_currentTime >= MaxWaitTime){
			GameManager.Instance.LoadLevel(SceneNumber);
		}
		else{
			_currentTime += Time.deltaTime;
		}
	}
}
