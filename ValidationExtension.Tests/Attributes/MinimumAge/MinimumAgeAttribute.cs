using System.ComponentModel.DataAnnotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Validation.ViewModel.Test;

[TestClass]
public class MinimumAgeAttribute
{
    private bool ValidateUser(UserFakeEntity user, out List<ValidationResult> results)
    {
        results = new List<ValidationResult>();
        var context = new ValidationContext(user);
        return Validator.TryValidateObject(user, context, results, true);
    }



    [TestMethod]
    [TestCategory("Attribute")]
    public void MinimumAge_Is_A_Valid_Date_Attribute()
    {
        var user = new UserFakeEntity { BirthDate = DateTime.Now.AddYears(-18) };
        Assert.IsTrue(ValidateUser(user, out var results));
        Assert.AreEqual(0, results.Count);
    }

    [TestMethod]
    [TestCategory("Attribute")]
    public void MinimumAge_Invalid_Age_Attribute()
    {
        var user = new UserFakeEntity { BirthDate = DateTime.Now.AddYears(-17) };
        Assert.IsFalse(ValidateUser(user, out var results));
        Assert.IsTrue(results.Count > 0);
        Assert.AreEqual($"User must be at least '18' years old.", results[0].ErrorMessage);
    }

    [TestMethod]
    [TestCategory("Attribute")]
    public void MinimumAge_Can_Be_Null_Attribute()
    {
        var user = new UserFakeEntity { BirthDate = null };
        Assert.IsTrue(ValidateUser(user, out var results));
        Assert.AreEqual(0, results.Count);
    }

    [TestMethod]
    [TestCategory("Attribute")]
    public void MinimumAge_Cannot_Be_A_Future_Date_Attribute()
    {
        var user = new UserFakeEntity { BirthDate = DateTime.Now.AddDays(1) };
        Assert.IsFalse(ValidateUser(user, out var results));
        Assert.IsTrue(results.Count > 0);
        Assert.AreEqual($"User must be at least '18' years old.", results[0].ErrorMessage);
    }

    [TestMethod]
    [TestCategory("Attribute")]
    public void MinimumAge_Default_Minimum_Age_Attribute()
    {
        var user = new UserFakeEntity { BirthDateDefaultMinimumAge = DateTime.Now };
        Assert.IsTrue(ValidateUser(user, out var results));
        Assert.AreEqual(0, results.Count);
    }

    [TestMethod]
    [TestCategory("Attribute")]
    public void MinimumAge_Cannot_Be_Negative_Age_Attribute()
    {
        var user = new UserFakeEntity { BirthDateNegative = DateTime.Now };
        Assert.IsFalse(ValidateUser(user, out var results));
        Assert.IsTrue(results.Count > 0);
        Assert.AreEqual($"The MinimumAge cannot be less than zero. Actual value is: '-1'.", results[0].ErrorMessage);
    }
}