using UnityEngine;
using System.Collections;

public class SpaceChange : MonoBehaviour {

	public int ToScene;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyUp(KeyCode.Space)){
			GameManager.Instance.LoadLevel(ToScene);
		}
	}
}
