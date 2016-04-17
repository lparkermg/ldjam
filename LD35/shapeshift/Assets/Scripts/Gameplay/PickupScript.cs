using UnityEngine;
using System.Collections;

public class PickupScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D objCol){
		if(objCol.gameObject.tag == "Player"){
			GameManager.Instance.UpdateCollectables(1,false);
			GameManager.Instance.UpdateScore(100, false);
			Destroy(gameObject);
		}
	}
}
