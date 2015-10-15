using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
    public bool flip;
    public BaseEnemy enemySpawned;

    //Spawn Enemy type at spawner position
	public BaseEnemy Spawn()
    {
        BaseEnemy clone = (BaseEnemy)Instantiate(enemySpawned, this.transform.position, this.transform.rotation);
        if (flip)
        {
            clone.gameObject.GetComponent<EntityMovement>().Flip();
            
        }
        ScaleEnemy(clone);
        return clone;
    }

    public void OnDeathSpawn()
    {
        if (Random.Range(0, 10) > 5)
        {
            Spawn();
        }
        else
        {
            BaseEnemy enemy = Spawn();
            enemy.gameObject.GetComponent<EntityMovement>().Flip();
        }
    }

    public void ScaleEnemy(BaseEnemy enemy)
    {
        int playerLevel = GameControl.control.playerLevel;
        Debug.Log("Scaled Enemy");
        enemy.maxHealth = enemy.maxHealth + (Mathf.CeilToInt(enemy.maxHealth / 4.0f) * Mathf.FloorToInt(playerLevel / 5.0f));
        enemy.currentHealth = enemy.currentHealth + (Mathf.CeilToInt(enemy.currentHealth/4.0f) * Mathf.FloorToInt(playerLevel/5.0f));
        enemy.damageGiven = enemy.damageGiven;
        enemy.experienceGiven = enemy.experienceGiven;
        enemy.entityMovement.maxSpeed = enemy.entityMovement.maxSpeed;
        enemy.entityMovement.maxMaxSpeed = enemy.entityMovement.maxMaxSpeed;
    }
}
