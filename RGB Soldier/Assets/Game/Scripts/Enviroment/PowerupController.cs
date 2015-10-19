using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PowerupController : MonoBehaviour
{
	public Image agilitySprite;
	public Image atkSprite;

	public GameObject agilityPowerup;
	public GameObject healthPowerup;
	public GameObject attackPowerup;
	public GameObject specialPowerup;
	
	private bool _agilBoost;
	private bool _attackBoost;
	private const float _powerupDuration = 15f;

	public bool isAgilityBoost()
	{
		return _agilBoost;
	}

	public bool isAttackBoost()
	{
		return _attackBoost;
	}

	public void setAgilBoost(bool boost)
	{
		_agilBoost = boost;
	}

	public void setAttackBoost(bool boost)
	{
		_attackBoost = boost;
	}

	void Start()
	{
		setAgilBoost (false);
		setAttackBoost (false);

		atkSprite.enabled = false;
		agilitySprite.enabled = false;
	}

    /// <summary>
    /// Calls the appropriate method to activate the inputted powerup
    /// </summary>
    /// <param name="powerup">Input powerup to be activated</param>
	public void activatePowerup(Powerup powerup)
	{
		switch (powerup.getType ()) 
		{
			case PowerupType.ATTACK:
				activateAttackBoost();
				break;
			case PowerupType.AGILITY:
				activateAgilityBoost();
				break;
			case PowerupType.HEALTH: 
				activateHealthBoost();
				break;
			case PowerupType.SPECIAL: 
				activateSpecialBoost();
				break;
		}
	}

    /// <summary>
    /// Activates agility boost for 15 seconds
    /// </summary>
	public void activateAgilityBoost() {
		if (isAgilityBoost())
			return;
		setAgilBoost(true);
		StartCoroutine (StartAgilityBoost ());
	}
	
	IEnumerator StartAgilityBoost() {
		agilitySprite.enabled = true;
		// go through countdown timer
		for (int i = (int)_powerupDuration; i > 0; i--)
		{
			yield return new WaitForSeconds(1f);
		}
		setAgilBoost (false);
		agilitySprite.enabled = false;
	}

    /// <summary>
    /// Activates attack boost for 15 seconds
    /// </summary>
	public void activateAttackBoost()
	{
		if (isAttackBoost ())
			return;
		setAttackBoost (true);
		StartCoroutine (StartAtkBoost ());
	}

	IEnumerator StartAtkBoost() {
		atkSprite.enabled = true;
		// go through countdown timer
		for (int i = (int)_powerupDuration; i > 0; i--)
		{
			yield return new WaitForSeconds(1f);
		}
		setAttackBoost (false);
		atkSprite.enabled = false;
	}

    /// <summary>
    /// Increments player's current health by 1
    /// </summary>
	public void activateHealthBoost()
	{
		Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		player.currentHealth++;
	}

    /// <summary>
    /// Adds 50% onto the special attack charger bar
    /// </summary>
	public void activateSpecialBoost()
	{
		GameManager.instance.specialCharge += GameManager.SPECIAL_CHARGE_TARGET / 2;
		if (GameManager.instance.specialCharge > GameManager.SPECIAL_CHARGE_TARGET)
			GameManager.instance.specialCharge = GameManager.SPECIAL_CHARGE_TARGET;
	}

    /// <summary>
    /// Spawns a random powerup at the given position and rotation.
    /// Spawned powerup can be one of the following:
    /// Agility, Attack, Health, Special.
    /// </summary>
    /// <param name="position">Vector position of spawned powerup. Used to instantiate powerup</param>
    /// <param name="rotation">Rotation of spawned powerup. Used to instantiate powerup</param>
	public void spawnRandomPowerup(Vector3 position, Quaternion rotation)
	{
		switch (Random.Range (0, 3))
		{
		case 0: 
			Instantiate(agilityPowerup, position, rotation);
			break;
		case 1: 
			Instantiate(attackPowerup, position, rotation);
			break;
		case 2: 
			Instantiate(healthPowerup, position, rotation);
			break;
		case 3: 
			Instantiate(specialPowerup, position, rotation);
			break;
		}
	}

}

