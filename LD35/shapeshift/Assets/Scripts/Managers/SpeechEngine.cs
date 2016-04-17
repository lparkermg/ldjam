using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpeechEngine : MonoBehaviour {
	//AI to Player
	public string[] TriangleToCircle = new string[] {"Sup?", "Oh hey.", "Yo!"};
	public string[] TriangleToSquare = new string[] {"HALT!", "Stop right there, criminal scum!", "Gotcha now!", "Hold it, you're under arrest"};
	public string[] TriangleToTriangle = new string[] {"What up bruh?", "You're lookin' buff bruh.","I HATE SQUARES BRO!!!", "Where's that square?", "Yo, Bro!"};

	public string[] SquareToCircle = new string[] {"Hi there!", "Howdy!"};
	public string[] SquareToSquare = new string[] {"Hey buddy!", "Be square or be there, amirite?", "Don't square off with me!", "Those triangles are scary!", "Hip to be square..."};
	public string[] SquareToTriangle = new string[] {"EEK!!", "Please don't hurt me!!", "I'm sorry!",  "Sorry!"};

	public string[] CircleToCircle = new string[] {"You look cool.", "Wow, you're in great shape...", "I'll see you... a round..."};
	public string[] CircleToSquare = new string[] {"You'll see me, a round... right?","I'm not a platform, I'm a circle!\nJerk...", "Whatever...", "You're pretty cool... for a square.", "You look like a square."};
	public string[] CircleToTriangle = new string[] {"Hey big guy..."};

	public string[] GenericSpeech = new string[] {"Hey.", "Nice, day it is today."};

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
				return RandomText(new string[] {},TriangleToSquare);
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
				return RandomText(GenericSpeech,SquareToTriangle);
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
