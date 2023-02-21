using UnityEngine;

internal sealed class CustomLogger
{
    private readonly string _sourceName;

    public CustomLogger(string sourceName) => _sourceName = sourceName;

    public void Log(string message) =>
        Debug.Log(WrapMessage(message));

    public void Error(string message) =>
       Debug.LogError(WrapMessage(message));

    public void Warning(string message) =>
       Debug.LogWarning(WrapMessage(message));

    private string WrapMessage(string message) =>
        $"[{GetType().Name}] {message}";
}
