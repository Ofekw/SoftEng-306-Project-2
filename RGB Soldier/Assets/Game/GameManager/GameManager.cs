//using UnityEngine;
//using System.Collections;
//
//public class GameManager : MonoBehaviour
//	where T : Component
//{
//	private static T _instance;
//	public static T Instance {
//		get {
//			if (_instance == null) {
//				_instance = FindObjectOfType<GameManager> ();
//				if (_instance == null) {
//					GameObject obj = new GameObject ();
//					obj.hideFlags = HideFlags.HideAndDontSave;
//					_instance = obj.AddComponent<T> ();
//				}
//			}
//			return _instance;
//		}
//	}
//
//	public virtual void Awake() {
//		DontDestroyOnLoad (this.gameObject);
//		if (_instance == null) {
//			_instance = this as T;
//		} else {
//			Destroy(gameObject);
//		}
//	}
//
//	// Use this for initialization
//	void Start () {
//	
//	}
//	
//	// Update is called once per frame
//	void Update () {
//	
//	}
//}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {
	public static GameManager instance;
	public static int orbsCollected = 0;
	public static int specialAttackCounter = 0;
	public static int enemiesOnScreen = 0;
	public static int stage;

	public const int ORB_COUNT_TARGET = 20;

    public Text orbCountDisp;

	void Awake() {
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy(gameObject); //this enforces Singleton pattern
		}
		DontDestroyOnLoad (gameObject);
		InitGame ();
	}

	void InitGame() {
		orbsCollected = 0;
		specialAttackCounter = 0;
		enemiesOnScreen = 0;
        orbCountDisp.text = "0 / " + ORB_COUNT_TARGET.ToString();
	}
	void Update() {
		Player player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player>();
		if (player.currentHealth <= 0) {
			gameOver();
		}
		if (orbsCollected >= ORB_COUNT_TARGET) {
			levelCleared();
		}
        
        //test orb display by random increment
        if (Random.value < 0.01)
        {
            incrementOrbCounter();
            orbCountDisp.text = orbsCollected.ToString() + " / " + ORB_COUNT_TARGET.ToString();
        }
	}

	public static void incrementOrbCounter() {
		orbsCollected++;
	}

	public static void incrementSpecialAtkCounter() {
		specialAttackCounter++;
	}

	public static void resetSpecialAtkCounter() {
		specialAttackCounter = 0;
	}

	void levelCleared ()
	{
		print ("Level has been cleared!");
	}

	void gameOver()
	{
		print ("You died lol");
	}
}