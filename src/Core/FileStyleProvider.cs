namespace ConsoleFileManager.Core;

using System;
using System.Collections.Generic;
using System.IO;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;



public class FileStyle
{
    public Char Icon { get; set; } = '';

    public string? Fg { get; set; }
    public string? Bg { get; set; }
    public string? Accent { get; set; }

    public ConsoleColor FgColor { get; set; }
    public ConsoleColor BgColor { get; set; }
    public ConsoleColor AccentColor { get; set; }

    public List<string>? Extensions { get; set; }
}

public class FileStyleProvider
{
    private readonly Dictionary<string, FileStyle> _extMap = new();
    private FileStyle _default = new();
    private FileStyle? _directory;
    private readonly string _configPath;

    public FileStyleProvider(string configPath)
    {
        _configPath = configPath;
        Load();
    }

    public void Reload()
    {
        _extMap.Clear();
        Load();
    }

    private void Load()
    {
        var yaml = File.ReadAllText(_configPath);

        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .Build();

        var config = deserializer.Deserialize<Dictionary<string, FileStyle>>(yaml);

        if (config == null || !config.ContainsKey("default"))
            throw new Exception("Config must contain 'default' section");

        _default = Prepare(config["default"]);

        if (config.ContainsKey("directories"))
            _directory = Prepare(config["directories"]);

        foreach (var kv in config)
        {
            var style = Prepare(kv.Value);

            if (style.Extensions == null) continue;

            foreach (var ext in style.Extensions)
            {
                _extMap[ext.ToLower()] = style;
            }
        }
    }

    public FileStyle GetStyle(string path)
    {
        if (_directory != null && Directory.Exists(path))
            return _directory;

        var ext = Path.GetExtension(path)
            .TrimStart('.')
            .ToLower();

        if (_extMap.TryGetValue(ext, out var style))
            return style;

        return _default;
    }

    private FileStyle Prepare(FileStyle style)
    {
        style.FgColor = ParseColor(style.Fg, ConsoleColor.Gray);
        style.BgColor = ParseColor(style.Bg, ConsoleColor.Black);
        style.AccentColor = ParseColor(style.Accent, ConsoleColor.DarkGray);
        return style;
    }

    private ConsoleColor ParseColor(string? value, ConsoleColor fallback)
    {
        if (string.IsNullOrWhiteSpace(value))
            return fallback;

        if (Enum.TryParse<ConsoleColor>(value, true, out var color))
            return color;

        return fallback;
    }
}