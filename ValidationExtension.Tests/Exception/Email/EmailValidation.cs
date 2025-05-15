using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Validation.ExceptionExtension.Test;

[TestClass]
public class EmailValidation
{
    [TestMethod]
    [TestCategory("Exception")]
    public void Email_Regex_Match_Exception()
    {
        var email = "emaildeteste@gmail.com.br";
        EmailAddressDomainException.ValidationThrow(email);
    }

    [TestMethod]
    [TestCategory("Exception")]
    public void Email_Regex_NotMatch_Exception()
    {
        var email = "emaildetestegmail.com.br"; // Email without "@"
        var ex = Assert.ThrowsException<EmailAddressDomainException>(() =>
        {
            EmailAddressDomainException.ValidationThrow(email);
        });
        Assert.AreEqual($"'{email} is not a valid email", ex.Message);
    }

    [TestMethod]
    [TestCategory("Exception")]
    public void Email_Regex_NotMatch_Without_Domain_Exception()
    {
        var email = "emaildetestegmail@"; // Email without Domain
        var ex = Assert.ThrowsException<EmailAddressDomainException>(() =>
        {
            EmailAddressDomainException.ValidationThrow(email);
        });
        Assert.AreEqual($"'{email} is not a valid email", ex.Message);
    }

    [TestMethod]
    [TestCategory("Exception")]
    public void Email_Regex_NotMatch_Without_LocalPart_Exception()
    {
        var email = "@gmail.com"; // Email without local part
        var ex = Assert.ThrowsException<EmailAddressDomainException>(() =>
        {
            EmailAddressDomainException.ValidationThrow(email);
        });
        Assert.AreEqual($"'{email} is not a valid email", ex.Message);
    }

    [TestMethod]
    [TestCategory("Exception")]
    public void Email_Regex_Match_WithPontuation_LocalPart_Exception()
    {
        var email = "ema-il-detes.te@gmail.com.br";
        EmailAddressDomainException.ValidationThrow(email);
    }

    [TestMethod]
    [TestCategory("Exception")]
    public void Email_Can_Be_Empty_Exception()
    {
        var email = "";
        EmailAddressDomainException.ValidationThrow(email);
    }

    [TestMethod]
    [TestCategory("Exception")]
    public void Email_Can_Be_Null_Exception()
    {
        string? email = null;
        EmailAddressDomainException.ValidationThrow(email);
    }
}