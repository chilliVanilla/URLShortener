using System.Security.Cryptography;
using URLShortener.Interfaces;

namespace URLShortener.Services;

public class CodeGenerator : ICodeGenerator
{
    private const string Alphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    private const int CodeLength = 7;
    public string Generate()
    {
        var code = RandomNumberGenerator.GetString(Alphabet, CodeLength);
        
        return code;
    }
}