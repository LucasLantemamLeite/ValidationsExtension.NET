using System.ComponentModel.DataAnnotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Validation.ViewModel.Test;

[TestClass]
public class EmailAttribute
{
    private bool ValidateUser(UserFakeEntity user, out List<ValidationResult> results)
    {
        results = new List<ValidationResult>();
        var context = new ValidationContext(user);
        return Validator.TryValidateObject(user, context, results, true);
    }

    [TestMethod]
    [TestCategory("Attribute")]
    public void Email_Regex_Match_Attribute()
    {
        var user = new UserFakeEntity { Email = "emaildeteste@gmail.com.br" };
        Assert.IsTrue(ValidateUser(user, out var results));
        Assert.AreEqual(0, results.Count);
    }

    [TestMethod]
    [TestCategory("Attribute")]
    public void Email_Regex_Not_Match_Attribute()
    {
        var user = new UserFakeEntity { Email = "emaildetestegmail.com.br" };
        Assert.IsFalse(ValidateUser(user, out var results));
        Assert.IsTrue(results.Count > 0);
        Assert.AreEqual($"'{user.Email}' is an invalid email format", results[0].ErrorMessage);
    }

    [TestMethod]
    [TestCategory("Attribute")]
    public void Email_Regex_NotMatch_Without_Domain_Attribute()
    {
        var user = new UserFakeEntity { Email = "emaildeteste@" };
        Assert.IsFalse(ValidateUser(user, out var results));
        Assert.IsTrue(results.Count > 0);
        Assert.AreEqual($"'{user.Email}' is an invalid email format", results[0].ErrorMessage);

    }

    [TestMethod]
    [TestCategory("Attribute")]
    public void Email_Regex_NotMatch_Without_LocalPart_Attribute()
    {
        var user = new UserFakeEntity { Email = "@gmail.com.br" };
        Assert.IsFalse(ValidateUser(user, out var results));
        Assert.IsTrue(results.Count > 0);
        Assert.AreEqual($"'{user.Email}' is an invalid email format", results[0].ErrorMessage);
    }

    [TestMethod]
    [TestCategory("Attribute")]
    public void Email_Regex_Match_WithPontuation_LocalPart_Attribute()
    {
        var user = new UserFakeEntity { Email = "ema-il-detes.te@gmail.com.br" };
        Assert.IsTrue(ValidateUser(user, out var results));
        Assert.AreEqual(0, results.Count);
    }

    [TestMethod]
    [TestCategory("Attribute")]
    public void Email_Can_Be_Empty_Exception()
    {
        var user = new UserFakeEntity { Email = "" };
        Assert.IsTrue(ValidateUser(user, out var results));
        Assert.AreEqual(0, results.Count);
    }

    [TestMethod]
    [TestCategory("Attribute")]
    public void Email_Can_Be_Null_Exception()
    {
        var user = new UserFakeEntity { Email = null };
        Assert.IsTrue(ValidateUser(user, out var results));
        Assert.AreEqual(0, results.Count);
    }
}