using System;
using NUnit.Framework;
using UnityEngine;

[TestFixture]
public class UnitTests
{
    [Test]
    public void jumpPressed()
    {
        Player sub = new Player();
        sub.jumpPressed();


        Assert.True(sub.isJumping);
    }

    [Test]
    public void updateStats()
    {
        Player subPlayer = new Player();
        EntityMovement subEntityMovement = NSubstitute.Substitute.For<EntityMovement>();
        subPlayer.entityMovement = subEntityMovement;

        subPlayer.vitality = 6;
        subPlayer.maxHealth = 3;

        subPlayer.UpdateStats();

        Assert.That(subPlayer.maxHealth == 6);
    }
}
