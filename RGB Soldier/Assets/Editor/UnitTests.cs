using System;
using NUnit.Framework;
using UnityEngine;

[TestFixture]
public class UnitTests
{
    [Test]
    public void jumpPressed()
    {
        //Create a player object and assert that isJumping equals true whenever a jump is performed.
        Player sub = new Player();

        Assert.True(!sub.isJumping);

        sub.setJumping();

        Assert.True(sub.isJumping);
    }

    [Test]
    public void takeDamage()
    {
        //Create a player and ensure the player takes damage whenever struck.
        Player player = new Player();
        player.currentHealth = 5;

        Assert.That(player.currentHealth == 5);

        player.calculateDamage(1);

        Assert.That(player.currentHealth == 4);
    }

    [Test]
    public void updateStats()
    {
        //Create a player and substitute in EntityMovement and PowerupController components.
        //Check that attributes are correctly updated.
        Player subPlayer = new Player();
        EntityMovement subEntityMovement = NSubstitute.Substitute.For<EntityMovement>();
        PowerupController subPowerController = NSubstitute.Substitute.For<PowerupController>();
        subPlayer.powerController = subPowerController;
        GameControl subGameControl = new GameControl();
        GameControl.control = subGameControl;

        subPlayer.entityMovement = subEntityMovement;

        subPlayer.vitality = 6;
        subPlayer.maxHealth = 3;

        Assert.That(subPlayer.maxHealth == 3);

        subPlayer.UpdateStats();

        Assert.That(subPlayer.maxHealth == 6);
    }

    [Test]
    public void levelUpCarryOver()
    {
        //Create the GameControl and assert that experience correctly carries over.
        GameControl subGameControl = new GameControl();
        subGameControl.playerLevel = 1;
        subGameControl.playerExp = 100;
        subGameControl.experienceRequired = 40;
        subGameControl.levelAndCarryOver();

        Assert.That(subGameControl.playerExp == 60);
        Assert.That(subGameControl.playerLevel == 2);
    }

    [Test]
    public void save()
    {
        //Create the GameControl and a Player
        //Check that saves are correctly done.
        GameControl subGameControl = new GameControl();
        PlayerData subPlayerData = new PlayerData();
        subGameControl.playerData = subPlayerData;

        subGameControl.playerStr = 8;
        subGameControl.playerAgl = 7;
        subGameControl.playerDex = 6;
        subGameControl.playerInt = 5;
        subGameControl.playerVit = 4;
        subGameControl.currentGameLevel = 3;
        subGameControl.abilityPoints = 2;

        subGameControl.Save();

        Assert.That(subPlayerData.playerStr == 8);
        Assert.That(subPlayerData.playerAgl == 7);
        Assert.That(subPlayerData.playerDex == 6);
        Assert.That(subPlayerData.playerInt == 5);
        Assert.That(subPlayerData.playerVit == 4);
        Assert.That(subPlayerData.currentGameLevel == 3);
        Assert.That(subPlayerData.abilityPoints == 2);
    }

    [Test]
    public void load()
    {
        //Create the GameControl and a Player.
        //Checks that loads are correctly done.
        GameControl subGameControl = new GameControl();
        PlayerData subPlayerData = new PlayerData();
        subGameControl.playerData = subPlayerData;

        subPlayerData.playerStr = 8;
        subPlayerData.playerAgl = 7;
        subPlayerData.playerDex = 6;
        subPlayerData.playerInt = 5;
        subPlayerData.playerVit = 4;
        subPlayerData.currentGameLevel = 3;
        subPlayerData.abilityPoints = 2;

        subGameControl.Load();

        Assert.That(subGameControl.playerStr == 8);
        Assert.That(subGameControl.playerAgl == 7);
        Assert.That(subGameControl.playerDex == 6);
        Assert.That(subGameControl.playerInt == 5);
        Assert.That(subGameControl.playerVit == 4);
        Assert.That(subGameControl.currentGameLevel == 3);
        Assert.That(subGameControl.abilityPoints == 2);
    }

    [Test]
    public void resetStat()
    {
        //Create the GameControl, IncreaseStat and a Player.
        //Check that attributes are correctly reset.
        IncreaseStat subIS = new IncreaseStat();
        GameControl subGameControl = new GameControl();
        GameControl.control = subGameControl;
        Player subPlayer = new Player();

        subPlayer.strength = 5;
        subPlayer.dexterity = 5;
        subPlayer.vitality = 5;
        subPlayer.intelligence = 5;
        subPlayer.agility = 5;
        subIS.player = subPlayer;

        subGameControl.playerStr = 5;
        subGameControl.playerDex = 5;
        subGameControl.playerVit = 5;
        subGameControl.playerInt = 5;
        subGameControl.playerAgl = 5;

        subIS.doReset();

        Assert.That(subPlayer.strength == 1);
        Assert.That(subPlayer.dexterity == 1);
        Assert.That(subPlayer.vitality == 1);
        Assert.That(subPlayer.intelligence == 1);
        Assert.That(subPlayer.agility == 1);
        Assert.That(subGameControl.playerStr == 1);
        Assert.That(subGameControl.playerDex == 1);
        Assert.That(subGameControl.playerVit == 1);
        Assert.That(subGameControl.playerInt == 1);
        Assert.That(subGameControl.playerAgl == 1);
    }
}
