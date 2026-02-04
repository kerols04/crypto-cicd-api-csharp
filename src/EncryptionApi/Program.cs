using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

var app = builder.Build();

app.MapGet("/", () => Results.Ok(new { message = "Encryption API is running" }));

// Encrypts text using a Caesar cipher with the provided shift (default = 3).
app.MapPost("/encrypt", (CipherRequest request) =>
{
    if (string.IsNullOrWhiteSpace(request.Text))
    {
        return Results.BadRequest(new { error = "Text is required." });
    }

    var shift = request.Shift ?? CaesarCipher.DefaultShift;
    var result = CaesarCipher.Transform(request.Text, shift);

    return Results.Ok(new CipherResponse(result));
});

// Decrypts text by reversing the Caesar cipher shift.
app.MapPost("/decrypt", (CipherRequest request) =>
{
    if (string.IsNullOrWhiteSpace(request.Text))
    {
        return Results.BadRequest(new { error = "Text is required." });
    }

    var shift = request.Shift ?? CaesarCipher.DefaultShift;
    var result = CaesarCipher.Transform(request.Text, -shift);

    return Results.Ok(new CipherResponse(result));
});

app.Run();

public record CipherRequest(string Text, int? Shift);
public record CipherResponse(string Result);

public static class CaesarCipher
{
    public const int DefaultShift = 3;

    // Transforms the input by shifting alphabetic characters while keeping casing and punctuation.
    public static string Transform(string input, int shift)
    {
        if (string.IsNullOrEmpty(input))
        {
            return input;
        }

        shift = shift % 26;

        var chars = input.Select(character => TransformChar(character, shift)).ToArray();
        return new string(chars);
    }

    // Keeps non-letters unchanged so punctuation survives round-trips.
    private static char TransformChar(char character, int shift)
    {
        if (!char.IsLetter(character))
        {
            return character;
        }

        var offset = char.IsUpper(character) ? 'A' : 'a';
        var normalized = character - offset;
        var shifted = (normalized + shift + 26) % 26;

        return (char)(shifted + offset);
    }
}
