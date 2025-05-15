using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Validation.ExceptionExtension.Test;

[TestClass]
public class CPFValidation
{
    [TestMethod]
    [TestCategory("Exception")]
    public void CPF_Regex_Match_With_Pontuation_Exception()
    {
        var CPF = "867.253.580-47"; // Random CPF
        CPFValidationException.ValidationThrow(CPF);
    }

    [TestMethod]
    [TestCategory("Exception")]
    public void CPF_Regex_NotMatch_With_Pontuation_Exception()
    {
        var CPF = "867.253.580-4"; // Random CPF with 10 lenght
        var ex = Assert.ThrowsException<CPFValidationException>(() =>
        {
            CPFValidationException.ValidationThrow(CPF);
        });
        Assert.AreEqual($"The CPF '{CPF}' is in an invalid format or does not have exactly 11 digits. It can optionally include dots (.) and hyphen (-). Please follow the pattern: XXX.XXX.XXX-XX or XXXXXXXXXXX.", ex.Message);
    }

    [TestMethod]
    [TestCategory("Exception")]
    public void CPF_Regex_Match_Without_Pontuation_Exception()
    {
        var CPF = "86725358047"; // Random CPF without pontuation
        CPFValidationException.ValidationThrow(CPF);
    }

    [TestMethod]
    [TestCategory("Exception")]
    public void CPF_Regex_NotMatch_Without_Pontuation_Exception()
    {
        var CPF = "8672535804"; // Random CPF with 10 lenght
        var ex = Assert.ThrowsException<CPFValidationException>(() =>
        {
            CPFValidationException.ValidationThrow(CPF);
        });
        Assert.AreEqual($"The CPF '{CPF}' is in an invalid format or does not have exactly 11 digits. It can optionally include dots (.) and hyphen (-). Please follow the pattern: XXX.XXX.XXX-XX or XXXXXXXXXXX.", ex.Message);
    }

    [TestMethod]
    [TestCategory("Exception")]
    public void CPF_With_Extra_Punctuation_Exception()
    {
        var CPF = "*/867.253.580-47)*+*.";
        var ex = Assert.ThrowsException<CPFValidationException>(() =>
        {
            CPFValidationException.ValidationThrow(CPF);
        });
        Assert.AreEqual($"The CPF '{CPF}' is in an invalid format or does not have exactly 11 digits. It can optionally include dots (.) and hyphen (-). Please follow the pattern: XXX.XXX.XXX-XX or XXXXXXXXXXX.", ex.Message);
    }

    [TestMethod]
    [TestCategory("Exception")]
    public void CPF_Can_Be_Empty_Exception()
    {
        var CPF = "";
        CPFValidationException.ValidationThrow(CPF);
    }

    [TestMethod]
    [TestCategory("Exception")]
    public void CPF_Can_Be_Null_Exception()
    {
        string? CPF = null;
        CPFValidationException.ValidationThrow(CPF);
    }

    [TestMethod]
    [TestCategory("Exception")]
    public void CPF_Is_Valid_Digit_Verify_Exception()
    {
        var CPF = "86725358047";
        CPFValidationException.ValidationThrow(CPF);
    }

    [TestMethod]
    [TestCategory("Exception")]
    public void CPF_Is_Invalid_Digit_Verify_Exception()
    {
        var CPF = "86725358046";
        var ex = Assert.ThrowsException<CPFValidationException>(() =>
        {
            CPFValidationException.ValidationThrow(CPF);
        });
        Assert.AreEqual($"Invalid CPF: '{CPF}', the verification digits are incorrect", ex.Message);
    }
}