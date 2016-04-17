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
	private float _jumpMultiplier = 100.0f;
	public float JumpAmount = 1.0f;

	private bool _canDoubleJump = false;
	private int _jumpCount = 0;
	const int JUMPS_MAX = 2;
	private bool _jumpPressed = false;

	//Shape Shifting Stuff
	public string CurrentShape;

	public bool CanShift = false;

	//Ingame  interation stuff (UI)
	public CanvasGroup SpeechBubble;
	public RectTransform SpeechBubbleRect;
	public Text SpeechBubbleText;

	private bool _needsDismissing = false;
	private float _dismissTime = 2.0f;
	private float _currentDismissTime = 0.0f;

	private bool _finishedAnim = true;
	private bool _goingBack = false;

	public GameObject CharacterImage;

	public SpriteRenderer CharacterImageSprite;
	public Sprite SquareImage;
	public Sprite CircleImage;
	public Sprite TriangleImage;

	// Use this for initialization
	void Start () {
		_rigidBody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		InputCheck(GameManager.Instance.IsBusted);
		DismissCheck();
	}

	void OnCollisionEnter2D(Collision2D collision){
		if(collision.transform.position.y < transform.position.y){
			_jumpCount = 0;
		}
	}

	void InputCheck(bool isBusted){
		if(!isBusted){
			Move(Input.GetAxis("Horizontal"));
			if(Input.GetAxis("Vertical") > 0.1f && !_jumpPressed){
				Jump();
			}
			else if(Input.GetAxis("Vertical") == 0.0f && _jumpPressed){
				_jumpPressed = false;
			}

			if(CanShift){
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
		if(axisAmount > 0.0f){
			CharacterImageSprite.flipX = false;
		}
		else if(axisAmount < 0.0f){
			CharacterImageSprite.flipX = true;
		}
		if(axisAmount == 0.0f){
			_rigidBody.velocity = new Vector2(0.0f,_rigidBody.velocity.y);
		}

		Animate(axisAmount);

	}

	void Jump(){
		if(_jumpCount == 0){
			_rigidBody.AddForce(transform.up * (_jumpMultiplier * JumpAmount ),ForceMode2D.Impulse);
			_jumpCount++;
			_jumpPressed = true;
		}
		else if(_canDoubleJump){
			if(JUMPS_MAX < _jumpCount){
				_rigidBody.AddForce(transform.up * (_jumpMultiplier * JumpAmount ),ForceMode2D.Impulse);
				_jumpCount++;
				_jumpPressed = true;
			}
		}
	}

	private void Animate(float axisAmount){
		if(_finishedAnim){
			if(_goingBack){
				switch(CurrentShape){
				case("Square"):
					_finishedAnim = false;
					LeanTween.rotateZ(CharacterImage,-4.0f,0.25f).setOnComplete(()=>{
						_goingBack = false;
						_finishedAnim = true;
					});
					break;
				case("Circle"):
					break;
				case("Triangle"):
					_finishedAnim = false;
					LeanTween.rotateZ(CharacterImage,-8.0f,0.25f).setOnComplete(()=>{
						_goingBack = false;
						_finishedAnim = true;
					});
					break;
				}

			}
			else{
				switch(CurrentShape){
				case("Square"):
					_finishedAnim = false;
					LeanTween.rotateZ(CharacterImage,4.0f,0.25f).setOnComplete(()=>{
						_goingBack = true;
						_finishedAnim = true;
					});
					break;
				case("Circle"):
					break;
				case("Triangle"):
					_finishedAnim = false;
					LeanTween.rotateZ(CharacterImage,8.0f,0.25f).setOnComplete(()=>{
						_goingBack = true;
						_finishedAnim = true;
					});
					break;
				}
			}

			if(axisAmount == 0.0f){
				LeanTween.rotateZ(CharacterImage,0.0f,0.25f);
			}
		}
	}

	public void ShapeShift(string toWhat){
		CurrentShape = toWhat;
		switch(toWhat){
		case("Square"):
			Speed = 1.0f;
			JumpAmount = 1.0f;
			_canDoubleJump = false;
			CharacterImageSprite.sprite = SquareImage;
			//TODO: Reset rotation and other rigidbody stuff.
			break;
		case("Circle"):
			Speed = 2.0f;
			JumpAmount = 0.1f;
			_canDoubleJump = false;
			CharacterImageSprite.sprite = CircleImage;
			//TODO:Set Rigidbody to circle stuff.
			break;
		case("Triangle"):
			Speed = 0.75f;
			JumpAmount = 1.5f;
			_canDoubleJump = true;
			CharacterImageSprite.sprite = TriangleImage;
			//TODO: Reset Rotation and other rigidbody stuff.
			break;
		default:
			CurrentShape = "Square";
			Speed = 1.0f;
			JumpAmount = 1.0f;
			_canDoubleJump = false;
			Debug.Log("Defaulted to Square.");
			break;
		}
	}

	#region Speech Bubble Stuff
	public void ShowSpeechBubble(string speech,float dismissAfter, float height, float width){
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

	#endregion
}
