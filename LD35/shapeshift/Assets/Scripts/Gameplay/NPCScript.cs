using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NPCScript : MonoBehaviour {

	//Ingame  interation stuff (UI)
	public CanvasGroup SpeechBubble;
	public RectTransform SpeechBubbleRect;
	public Text SpeechBubbleText;

	private bool _needsDismissing = false;
	private float _dismissTime = 2.0f;
	private float _currentDismissTime = 0.0f;

	public string AiShape = "Square";

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	#region Speech Bubble Stuff
	public void ShowSpeechBubble(string speech,float dismissAfter,float height, float width){
		SpeechBubbleText.text = speech;
		SpeechBubbleRect.rect.height = height;
		SpeechBubbleRect.rect.width = width;
		LeanTween.alphaCanvas(SpeechBubble,1.0f,0.25f).setOnComplete(() => {
			_dismissTime = dismissAfter;
			_currentDismissTime = 0.0f;
			_needsDismissing = true;
		});
	}

	public void DismissSpeechBubble(){
		LeanTween.alphaCanvas(SpeechBubble,0.0f,0.25f).setOnComplete(() => {
			_needsDismissing = false;
		});
	}

	#endregion
}
