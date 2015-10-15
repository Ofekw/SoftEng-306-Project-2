using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PowerupController : MonoBehaviour
{
	public Image agilitySprite;
	public Image atkSprite;

	private bool _agilBoost;
	private bool _dexBoost;
	private bool _attackBoost;
	private const float _powerupDuration = 15f;

	public bool isAgilityBoost()
	{
		return _agilBoost;
	}

	public bool isDexBoost()
	{
		return _dexBoost;
	}

	public bool isAttackBoost()
	{
		return _attackBoost;
	}

	public void setAgilBoost(bool boost)
	{
		_agilBoost = boost;
	}

	public void setDexBoost(bool boost)
	{
		_dexBoost = boost;
	}

	public void setAttackBoost(bool boost)
	{
		_attackBoost = boost;
	}

	void Start()
	{
		setAgilBoost (false);
		setDexBoost (false);
		setAttackBoost (false);

		atkSprite.enabled = false;
		agilitySprite.enabled = false;
	}

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

	public void activateHealthBoost()
	{
		Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		player.currentHealth++;
	}

	public void activateSpecialBoost()
	{
		GameManager.instance.specialCharge += GameManager.SPECIAL_CHARGE_TARGET / 2;
		if (GameManager.instance.specialCharge > GameManager.SPECIAL_CHARGE_TARGET)
			GameManager.instance.specialCharge = GameManager.SPECIAL_CHARGE_TARGET;
	}

}

