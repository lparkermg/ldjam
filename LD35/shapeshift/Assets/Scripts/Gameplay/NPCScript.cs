using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NPCScript : MonoBehaviour {

	//Ingame  interation stuff (UI)
	public CanvasGroup SpeechBubble;
	public RectTransform SpeechBubbleRect;
	public Text SpeechBubbleText;
	private SpeechEngine _speech;

	private bool _needsDismissing = false;
	private float _dismissTime = 2.0f;
	private float _currentDismissTime = 0.0f;

	private float _bubbleHeight = 0.5f;
	private float _bubbleWidth = 1.0f;

	public string AiShape = "Square";

	//Triangle to Square Specific Interaction
	private float _haltTime = 3.0f;
	private float _currentHaltTime = 0.0f;
	private bool _alerted = false;


	// Use this for initialization
	void Start () {
		_speech = GameObject.FindGameObjectWithTag("Managers").GetComponent<SpeechEngine>();
	}
	
	// Update is called once per frame
	void Update () {
		if(_needsDismissing){
			CheckDismiss();
		}
	}

	void OnCollisionEnter2D(Collision2D objCol){
		if(objCol.gameObject.tag == "Player"){
			var ps = objCol.gameObject.GetComponent<PlayerScript>();
			ShowSpeechBubble(_speech.GetSpeech(ps.CurrentShape,AiShape),3.0f,_bubbleHeight,_bubbleWidth);
		}
	}

	void OnTriggerEnter2D(Collider2D objCol){
		if(objCol.gameObject.tag == "Player"){
			var ps = objCol.gameObject.GetComponent<PlayerScript>();
			ShowSpeechBubble(_speech.GetSpeech(ps.CurrentShape,AiShape),3.0f,_bubbleHeight,_bubbleWidth);
			if(ps.CurrentShape == "Square" && AiShape == "Triangle"){
				_alerted = true;
			}
		}
	}

	void OnTriggerStay2D(Collider2D objCol){
		if(objCol.gameObject.tag == "Player"){
			var ps = objCol.gameObject.GetComponent<PlayerScript>();
			if(_alerted){
				if(_currentHaltTime > _haltTime){
					GameObject.FindGameObjectWithTag("Managers").GetComponent<InGame_UI_Manager>().ShowBusted();
				}
				else{
					_currentHaltTime += Time.deltaTime;
				}
			}
		}
	}

	void OnTriggerExit2D(Collider2D objCol){
		if(objCol.gameObject.tag == "Player"){
			var ps = objCol.gameObject.GetComponent<PlayerScript>();
			if(_alerted){
				_currentHaltTime = 0.0f;
				_alerted = false;
			}
		}
	}

	#region Speech Bubble Stuff
	public void ShowSpeechBubble(string speech,float dismissAfter,float height, float width){
		SpeechBubbleText.text = speech;
		SpeechBubbleRect.sizeDelta = new Vector2(width,height);
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

	private void CheckDismiss(){
		if(_currentDismissTime >= _dismissTime){
			DismissSpeechBubble();
		}
		else {
			_currentDismissTime += Time.deltaTime;
		}
	}

	#endregion

	private PlayerScript GetPlayerScript(GameObject objCol){
		var ps = objCol.GetComponent<PlayerScript>();
		return ps;
	}
		
}
