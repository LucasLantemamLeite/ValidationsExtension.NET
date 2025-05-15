using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Validation.ExceptionExtension.Test;

[TestClass]
public class MiniumAgeValidation
{
    [TestMethod]
    [TestCategory("Exception")]
    public void MinimumAge_Is_A_Valid_Date_Exception()
    {
        var data = DateTime.Now;
        MinimumAgeException.ValidationThrow(data);
    }

    [TestMethod]
    [TestCategory("Exception")]
    public void MinimumAge_Invalid_Age_Exception()
    {
        var data = DateTime.Now.AddYears(-1);
        var minimumAge = 10; // MinimumAge = 10
        var ex = Assert.ThrowsException<MinimumAgeException>(() =>
        {
            MinimumAgeException.ValidationThrow(data, minimumAge);
        });
        Assert.AreEqual($"User must be at least '{minimumAge}' years old.", ex.Message);
    }

    [TestMethod]
    [TestCategory("Exception")]
    public void MinimumAge_Can_Be_Null_Exception()
    {
        DateTime? data = null;
        MinimumAgeException.ValidationThrow(data);
    }

    [TestMethod]
    [TestCategory("Exception")]
    public void MinimumAge_Cannot_Be_A_Future_Date_Exception()
    {
        var data = DateTime.Now.AddDays(1);
        var ex = Assert.ThrowsException<MinimumAgeException>(() =>
        {
            MinimumAgeException.ValidationThrow(data);
        });
        Assert.AreEqual($"Birth date cannot be in the future. Provided birth date: '{data.ToShortDateString()}'", ex.Message);
    }

    [TestMethod]
    [TestCategory("Exception")]
    public void MinimumAge_Cannot_Be_Negative_Age_Exception()
    {
        var data = DateTime.Now;
        var minimumAge = -10; // MinimumAge = -10
        var ex = Assert.ThrowsException<MinimumAgeException>(() =>
        {
            MinimumAgeException.ValidationThrow(data, minimumAge);
        });
        Assert.AreEqual($"Minimum age cannot be negative. Actualy MinimunAge is: {minimumAge}", ex.Message);
    }
}