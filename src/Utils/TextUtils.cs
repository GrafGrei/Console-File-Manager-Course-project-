namespace ConsoleFileManager.Utils;

public static class TextUtils
{
    public static string Trim(string text, int maxLenght)
    {
        if (string.IsNullOrEmpty(text) || text.Length <= maxLenght)
            return text;
        if (maxLenght <= 1)
            return "…";
        
        return text[..(maxLenght-1)] + "…";

    }
}