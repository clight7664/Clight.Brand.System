namespace Clight.LogoStudio.Wasm.Services;

/// <summary>
/// Service interface for multi-language (i18n) localization across the Clight Logo Studio.
/// Supports Chinese (zh), English (en), Japanese (ja), and Korean (ko).
/// </summary>
public interface ILocalizationService
{
    string CurrentLanguage { get; }
    
    IReadOnlyList<(string Code, string NativeName, string EnglishName, string Flag)> SupportedLanguages { get; }

    string this[string key] { get; }

    string T(string key, params object[] args);

    Task InitializeAsync();

    Task SetLanguageAsync(string languageCode);

    event Action? OnLanguageChanged;
}
