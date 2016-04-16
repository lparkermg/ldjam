using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayerScript : MonoBehaviour {

	private Rigidbody2D _rigidBody;
	//Speed Stuff
	private float _speedMultiplier = 100.0f;
	public float Speed = 1.0f;

	//Jump Stuff
	private float _jumpMultiplier = 500.0f;
	public float JumpAmount = 1.0f;

	private bool _canDoubleJump = false;
	private int _jumpCount = 0;
	const int JUMPS_MAX = 2;

	//Shape Shifting Stuff
	public string CurrentShape;

	public bool CanCircle = false;
	public bool CanTriangle = false;

	//Ingame  interation stuff (UI)
	public CanvasGroup SpeechBubble;
	public RectTransform SpeechBubbleRect;
	public Text SpeechBubbleText;

	private bool _needsDismissing = false;
	private float _dismissTime = 2.0f;
	private float _currentDismissTime = 0.0f;

	// Use this for initialization
	void Start () {
		_rigidBody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		InputCheck();
		DismissCheck();
	}

	void OnCollisionEnter2D(Collision2D collision){
		if(collision.transform.position.y < transform.position.y){
			_jumpCount = 0;
		}
	}

	void InputCheck(){
		Move(Input.GetAxis("Horizontal"));
		if(Input.GetAxis("Vertical") > 0.0f){
			Jump();
		}

		//TODO: Consider changing to mappable thing...
		if(Input.GetKeyUp(KeyCode.Alpha1)){
			ShapeShift("Square");
		}
		else if(Input.GetKeyUp(KeyCode.Alpha2)){
			ShapeShift("Circle");
		}
		else if(Input.GetKeyUp(KeyCode.Alpha3)){
			ShapeShift("Triangle");
		}

	}

	void DismissCheck(){
		if(_needsDismissing){
			if(_dismissTime < _currentDismissTime){
				DismissSpeechBubble();
			}
			else{
				_currentDismissTime += Time.deltaTime;
			}
		}
	}

	void Move(float axisAmount){
		_rigidBody.AddForce(transform.right * (_speedMultiplier * Speed) * axisAmount,ForceMode2D.Force);

		//TODO: Add Animation part to this.
	}

	void Jump(){
		if(_jumpCount == 0){
			_rigidBody.AddForce(transform.up * (_jumpMultiplier * JumpAmount ),ForceMode2D.Impulse);
			_jumpCount++;
		}
		else if(_canDoubleJump){
			if(JUMPS_MAX < _jumpCount){
				_rigidBody.AddForce(transform.up * (_jumpMultiplier * JumpAmount ),ForceMode2D.Impulse);
				_jumpCount++;
			}
		}
	}

	public void ShapeShift(string toWhat){
		CurrentShape = toWhat;
		switch(toWhat){
		case("Square"):
			Speed = 1.0f;
			Jump = 1.0f;
			_canDoubleJump = false;
			break;
		case("Circle"):
			Speed = 2.0f;
			Jump = 0.2f;
			_canDoubleJump = false;
			break;
		case("Trianlgle"):
			Speed = 0.75f;
			Jump = 1.2f;
			_canDoubleJump = true;
			break;
		default:
			CurrentShape = "Square";
			Speed = 1.0f;
			Jump = 1.0f;
			_canDoubleJump = false;
			Debug.Log("Defaulted to Square.");
			break;
		}
	}

	#region Speech Bubble Stuff
	public void ShowSpeechBubble(string speech,float dismissAfter, float height, float width){
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
