using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InGame_UI_Manager : MonoBehaviour {

	public CanvasGroup BustedScreen;
	public CanvasGroup HUD;

	private bool _bustedShown = false;
	private float _waitTime =2.5f;
	private float _currentTime = 0.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(_bustedShown){
			if(_currentTime >= _waitTime){
				StartAgain();
			}
			else{
				_currentTime += Time.deltaTime;
			}
		}
	}

	public void ShowBusted(){
		GameManager.Instance.UpdateIsBusted(true);
		DisableHUD();
		LeanTween.alphaCanvas(BustedScreen, 1.0f,1.0f).setOnComplete(() => {
			BustedScreen.blocksRaycasts = true;
			BustedScreen.interactable = true;
			_bustedShown = true;
		});
	}

	private void DisableHUD(){
		HUD.blocksRaycasts = false;
		HUD.interactable = false;
	}

	public void EnableHUD(){
		HUD.blocksRaycasts = true;
		HUD.interactable = true;
	}

	private void StartAgain(){
		GameManager.Instance.LoadLevel(1);
	}
}
