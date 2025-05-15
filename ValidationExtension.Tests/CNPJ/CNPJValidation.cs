using Microsoft.VisualStudio.TestTools.UnitTesting;
using Validation.ExceptionExtension;

namespace ValidationExtension.Tests;

[TestClass]
public class CNPJValidation
{

    [TestMethod]
    [TestCategory("Exception")]
    public void CNPJ_Regex_Match_With_Pontuation_Exception()
    {
        var CNPJ = "75.204.860/0001-46";
        CNPJValidationException.ValidationThrow(CNPJ);
    }

    [TestMethod]
    [TestCategory("Exception")]
    public void CNPJ_Regex_NotMatch_With_Pontuation_Exception()
    {
        var CNPJ = "75.204.860/0001-4";
        var ex = Assert.ThrowsException<CNPJValidationException>(() =>
        {
            CNPJValidationException.ValidationThrow(CNPJ);
        });
        Assert.AreEqual($"The CNPJ '{CNPJ}' is in an invalid format or does not have exactly 14 digits. It can optionally include dots (.), slash (/) and hyphen (-). Please follow the pattern: XX.XXX.XXX/XXXX-XX or XXXXXXXXXXXXXX.", ex.Message);
    }

    [TestMethod]
    [TestCategory("Exception")]
    public void CNPJ_Regex_Match_Without_Pontuation_Exception()
    {
        var CNPJ = "75204860000146";
        CNPJValidationException.ValidationThrow(CNPJ);
    }

    [TestMethod]
    [TestCategory("Exception")]
    public void CNPJ_Regex_NotMatch_Without_Pontuation_Exception()
    {
        var CNPJ = "7520486000014";
        var ex = Assert.ThrowsException<CNPJValidationException>(() =>
        {
            CNPJValidationException.ValidationThrow(CNPJ);
        });
        Assert.AreEqual($"The CNPJ '{CNPJ}' is in an invalid format or does not have exactly 14 digits. It can optionally include dots (.), slash (/) and hyphen (-). Please follow the pattern: XX.XXX.XXX/XXXX-XX or XXXXXXXXXXXXXX.", ex.Message);
    }

    [TestMethod]
    [TestCategory("Exception")]
    public void CNPJ_With_Extra_Punctuation_Should_Exception()
    {
        var CNPJ = "+-/7520486000014)*//*-";
        var ex = Assert.ThrowsException<CNPJValidationException>(() =>
        {
            CNPJValidationException.ValidationThrow(CNPJ);
        });
        Assert.AreEqual($"The CNPJ '{CNPJ}' is in an invalid format or does not have exactly 14 digits. It can optionally include dots (.), slash (/) and hyphen (-). Please follow the pattern: XX.XXX.XXX/XXXX-XX or XXXXXXXXXXXXXX.", ex.Message);
    }

    [TestMethod]
    [TestCategory("Exception")]
    public void CNPJ_CanBeEmpty_Exception()
    {
        var CNPJ = "";
        CNPJValidationException.ValidationThrow(CNPJ);
    }

    [TestMethod]
    public void CNPJ_CanBeNull_Exception()
    {
        string? CNPJ = null;
        CNPJValidationException.ValidationThrow(CNPJ);
    }

    [TestMethod]
    [TestCategory("Exception")]
    public void CNPJ_Isvalid_Digit_Verify_Exception()
    {
        var CNPJ = "75204860000146";
        CNPJValidationException.ValidationThrow(CNPJ);
    }

    [TestMethod]
    [TestCategory("Exception")]
    public void CNPJ_IsInvalid_Digit_Verify_Exception()
    {
        var CNPJ = "75204860000149";
        var ex = Assert.ThrowsException<CNPJValidationException>(() =>
        {
            CNPJValidationException.ValidationThrow(CNPJ);
        });
        Assert.AreEqual($"Invalid CNPJ: '{CNPJ}', the verification digits are incorrect", ex.Message);

    }
}