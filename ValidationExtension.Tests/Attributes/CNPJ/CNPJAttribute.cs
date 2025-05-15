using System.ComponentModel.DataAnnotations;
using Microsoft.VisualBasic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Validation.ViewModel.Test;

[TestClass]
public class CNPJAttribute
{
    private bool ValidateUser(UserFakeEntity user, out List<ValidationResult> results)
    {
        results = new List<ValidationResult>();
        var context = new ValidationContext(user);
        return Validator.TryValidateObject(user, context, results, true);
    }

    [TestMethod]
    [TestCategory("Attribute")]
    public void CNPJ_Regex_Match_With_Pontuation_Attribute()
    {
        var user = new UserFakeEntity { CNPJ = "75.204.860/0001-46" };
        Assert.IsTrue(ValidateUser(user, out var results));
        Assert.AreEqual(0, results.Count);
    }

    [TestMethod]
    [TestCategory("Attribute")]
    public void CNPJ_Regex_Not_Match_With_Pontuation_Attribute()
    {
        var user = new UserFakeEntity { CNPJ = "75.204.860/0001-4" };
        Assert.IsFalse(ValidateUser(user, out var results));
        Assert.IsTrue(results.Count > 0);
        Assert.AreEqual($"The CNPJ '{user.CNPJ}' is in an invalid format or does not have exactly 14 digits. It can optionally include dots (.), slash (/) and hyphen (-). Please follow the pattern: XX.XXX.XXX/XXXX-XX or XXXXXXXXXXXXXX.", results[0].ErrorMessage);
    }

    [TestMethod]
    [TestCategory("Attribute")]
    public void CNPJ_Regex_Match_Without_Pontuation_Attribute()
    {
        var user = new UserFakeEntity { CNPJ = "75204860000146" };
        Assert.IsTrue(ValidateUser(user, out var results));
        Assert.AreEqual(0, results.Count);
    }

    [TestMethod]
    [TestCategory("Attribute")]
    public void CPF_Regex_Not_Match_Without_Pontuation_Attribute()
    {
        var user = new UserFakeEntity { CNPJ = "7520486000014" };
        Assert.IsFalse(ValidateUser(user, out var results));
        Assert.IsTrue(results.Count > 0);
        Assert.AreEqual($"The CNPJ '{user.CNPJ}' is in an invalid format or does not have exactly 14 digits. It can optionally include dots (.), slash (/) and hyphen (-). Please follow the pattern: XX.XXX.XXX/XXXX-XX or XXXXXXXXXXXXXX.", results[0].ErrorMessage);
    }

    [TestMethod]
    [TestCategory("Attribute")]
    public void CNPJ_With_Extra_Punctuation_Attribute()
    {
        var user = new UserFakeEntity { CNPJ = "+ */75204*/*-86000014-=-=" };
        Assert.IsFalse(ValidateUser(user, out var results));
        Assert.AreEqual($"The CNPJ '{user.CNPJ}' is in an invalid format or does not have exactly 14 digits. It can optionally include dots (.), slash (/) and hyphen (-). Please follow the pattern: XX.XXX.XXX/XXXX-XX or XXXXXXXXXXXXXX.", results[0].ErrorMessage);
    }

    [TestMethod]
    [TestCategory("Attribute")]
    public void CNPJ_Can_Be_Null_Attribute()
    {
        var user = new UserFakeEntity { CNPJ = null };
        Assert.IsTrue(ValidateUser(user, out var results));
        Assert.AreEqual(0, results.Count);
    }

    [TestMethod]
    [TestCategory("Attribute")]
    public void CNPJ_Can_Be_Empty_Attribute()
    {
        var user = new UserFakeEntity { CNPJ = "" };
        Assert.IsTrue(ValidateUser(user, out var results));
        Assert.AreEqual(0, results.Count);
    }

    [TestMethod]
    [TestCategory("Attribute")]
    public void CNPJ_Is_Valid_Digit_Verify_Attribute()
    {
        var user = new UserFakeEntity { CNPJ = "75204860000146" };
        Assert.IsTrue(ValidateUser(user, out var results));
        Assert.AreEqual(0, results.Count);
    }

    [TestMethod]
    [TestCategory("Attribute")]
    public void CNPJ_Is_Invalid_Digit_Verify_Attribute()
    {
        var user = new UserFakeEntity { CNPJ = "75204860000145" };
        Assert.IsFalse(ValidateUser(user, out var results));
        Assert.AreEqual($"Invalid CNPJ: '{user.CNPJ}', the verification digits are incorrect", results[0].ErrorMessage);
    }
}