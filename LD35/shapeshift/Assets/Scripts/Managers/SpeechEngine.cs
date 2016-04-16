using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpeechEngine : MonoBehaviour {

	public string[] TriangleToCircle;
	public string[] TriangleToSquare;
	public string[] TriangleToTriangle;

	public string[] SquareToCircle;
	public string[] SquareToSquare;
	public string SquareToTriangle;

	public string[] CircleToCircle;
	public string[] CircleToSquare;
	public string[] CircleToTriangle;

	public string[] GenericSpeech;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public string GetSpeech(string playerType, string npcType){
		if(npcType == "Triangle"){
			switch(playerType){
			case("Square"):
				return RandomText(GenericSpeech,TriangleToSquare);
			case("Triangle"):
				return RandomText(GenericSpeech,TriangleToTriangle);
			case("Circle"):
				return RandomText(GenericSpeech,TriangleToCircle);
			default:
				return RandomText(GenericSpeech, new string[] {});
			}
		}
		else if(npcType == "Square"){
			switch(playerType){
			case("Square"):
				return RandomText(GenericSpeech,SquareToSquare);
			case("Triangle"):
				return SquareToTriangle;
			case("Circle"):
				return RandomText(GenericSpeech,SquareToCircle);
			default:
				return RandomText(GenericSpeech, new string[] {});
			}
		}
		else if(npcType == "Circle"){
			switch(playerType){
			case("Square"):
				return RandomText(GenericSpeech,CircleToSquare);
			case("Triangle"):
				return RandomText(GenericSpeech,CircleToTriangle);
			case("Circle"):
				return RandomText(GenericSpeech,CircleToCircle);
			default:
				return RandomText(GenericSpeech, new string[] {});
			}
		}
		else{
			return RandomText(GenericSpeech, new string[] {});
		}
	}

	private string RandomText(string[] generic, string[] specific){
		var speechList = new List<string>();
		speechList.AddRange(generic);
		speechList.AddRange(specific);

		return speechList[Random.Range(0,speechList.Count)];
	}
}
