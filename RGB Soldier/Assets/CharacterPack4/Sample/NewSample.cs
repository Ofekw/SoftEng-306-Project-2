using UnityEngine;
using UnityEngine.UI;

public class NewSample : MonoBehaviour {

	[SerializeField]string[] animationNames;
	[SerializeField]Text animationNameText;

	int animationNumber = 0;
	int playingAnimationNumber = 0;

	void Start(){
		SetText();
		PlayAnimation();
	}

	void SetText(){
		animationNameText.text = animationNames[animationNumber];
	}

	void PlayAnimation(){
		Animator[] animators = GetComponentsInChildren<Animator>();
		foreach( Animator anim in animators ){
			anim.SetBool(animationNames[playingAnimationNumber],false);
			anim.SetBool(animationNames[animationNumber],true);
		}
		playingAnimationNumber = animationNumber;
	}

	public void OnButtonNext(){
		animationNumber++;
		if( animationNumber > animationNames.Length - 1 ) animationNumber = 0;
		SetText();
		PlayAnimation();
	}

	public void OnButtonBack(){
		animationNumber--;
		if( animationNumber < 0 ) animationNumber = animationNames.Length - 1;
		SetText();
		PlayAnimation();
	}

}
