using System.ComponentModel.DataAnnotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Validation.ViewModel.Test;

[TestClass]
public class CPFAttribute
{
    private bool ValidateUser(UserFakeEntity user, out List<ValidationResult> results)
    {
        results = new List<ValidationResult>();
        var context = new ValidationContext(user);
        return Validator.TryValidateObject(user, context, results, true);
    }

    [TestMethod]
    [TestCategory("Attribute")]
    public void CPF_Regex_Match_With_Pontuation_Attribute()
    {
        var user = new UserFakeEntity { CPF = "867.253.580-47" };
        Assert.IsTrue(ValidateUser(user, out var results));
        Assert.AreEqual(0, results.Count);
    }

    [TestMethod]
    [TestCategory("Attribute")]
    public void CPF_Regex_NotMatch_With_Pontuation_Attribute()
    {
        var user = new UserFakeEntity { CPF = "867.253.580-4" };
        Assert.IsFalse(ValidateUser(user, out var results));
        Assert.IsTrue(results.Count > 0);
        Assert.AreEqual($"The CPF '{user.CPF}' is in an invalid format or does not have exactly 11 digits. It can optionally include dots (.) and hyphen (-). Please follow the pattern: XXX.XXX.XXX-XX or XXXXXXXXXXX.", results[0].ErrorMessage);
    }

    [TestMethod]
    [TestCategory("Attribute")]
    public void CPF_Regex_Match_Without_Pontuation_Attribute()
    {
        var user = new UserFakeEntity { CPF = "86725358047" };
        Assert.IsTrue(ValidateUser(user, out var results));
        Assert.AreEqual(0, results.Count);
    }

    [TestMethod]
    [TestCategory("Attribute")]
    public void CPF_Regex_NotMatch_Without_Pontuation_Attribute()
    {
        var user = new UserFakeEntity { CPF = "8672535804" };
        Assert.IsFalse(ValidateUser(user, out var results));
        Assert.IsTrue(results.Count > 0);
        Assert.AreEqual($"The CPF '{user.CPF}' is in an invalid format or does not have exactly 11 digits. It can optionally include dots (.) and hyphen (-). Please follow the pattern: XXX.XXX.XXX-XX or XXXXXXXXXXX.", results[0].ErrorMessage);
    }

    [TestMethod]
    [TestCategory("Attribute")]
    public void CPF_With_Extra_Punctuation_Attribute()
    {
        var user = new UserFakeEntity { CPF = "-=8672/-53580-0<47=-=" };
        Assert.IsFalse(ValidateUser(user, out var results));
        Assert.AreEqual($"The CPF '{user.CPF}' is in an invalid format or does not have exactly 11 digits. It can optionally include dots (.) and hyphen (-). Please follow the pattern: XXX.XXX.XXX-XX or XXXXXXXXXXX.", results[0].ErrorMessage);
    }

    [TestMethod]
    [TestCategory("Attribute")]
    public void CPF_Can_Be_Null_Attribute()
    {
        var user = new UserFakeEntity { CPF = null };
        Assert.IsTrue(ValidateUser(user, out var results));
        Assert.AreEqual(0, results.Count);
    }

    [TestMethod]
    [TestCategory("Attribute")]
    public void CPF_Can_Be_Empty_Attribute()
    {
        var user = new UserFakeEntity { CPF = "" };
        Assert.IsTrue(ValidateUser(user, out var results));
        Assert.AreEqual(0, results.Count);
    }

    [TestMethod]
    [TestCategory("Attribute")]
    public void CPF_Is_Valid_Digit_Verify_Attribute()
    {
        var user = new UserFakeEntity { CPF = "86725358047" };
        Assert.IsTrue(ValidateUser(user, out var results));
        Assert.AreEqual(0, results.Count);
    }

    [TestMethod]
    [TestCategory("Attribute")]
    public void CPF_Is_Invalid_Digit_Verify_Attribute()
    {
        var user = new UserFakeEntity { CPF = "86725358046" };
        Assert.IsFalse(ValidateUser(user, out var results));
        Assert.AreEqual($"Invalid CPF: '{user.CPF}', the verification digits are incorrect", results[0].ErrorMessage);
    }
}