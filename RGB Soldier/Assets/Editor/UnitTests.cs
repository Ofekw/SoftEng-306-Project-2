using System;
using NUnit.Framework;
using UnityEngine;

[TestFixture]
public class UnitTests
{
    [Test]
    public void jumpPressed()
    {
        Player sub = NSubstitute.Substitute.For<Player>();
        sub.jumpPressed();


        Assert.True(sub.isJumping);
    }
}
