using Xunit;

public class CaesarCipherTests
{
    [Theory]
    [InlineData("abc", 3, "def")]
    [InlineData("XYZ", 3, "ABC")]
    [InlineData("Hello, World!", 3, "Khoor, Zruog!")]
    [InlineData("def", -3, "abc")]
    public void Transform_ShiftsLetters(string input, int shift, string expected)
    {
        var result = CaesarCipher.Transform(input, shift);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void Transform_LeavesNonLettersUntouched()
    {
        var result = CaesarCipher.Transform("123-!?", 5);

        Assert.Equal("123-!?", result);
    }

    [Fact]
    public void Decrypt_ReversesEncrypt()
    {
        var original = "Hej varlden!";
        var encrypted = CaesarCipher.Transform(original, CaesarCipher.DefaultShift);
        var decrypted = CaesarCipher.Transform(encrypted, -CaesarCipher.DefaultShift);

        Assert.Equal(original, decrypted);
    }
}
