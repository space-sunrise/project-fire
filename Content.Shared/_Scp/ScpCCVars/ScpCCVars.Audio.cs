using Robust.Shared.Configuration;

namespace Content.Shared._Scp.ScpCCVars;

public sealed partial class ScpCCVars
{
    #region Audio

    /// <summary>
    ///     Whether to render sounds with echo when they are in 'large' open, rooved areas.
    /// </summary>
    /// <seealso cref="AreaEchoSystem"/>
    public static readonly CVarDef<bool> EchoEnabled =
        CVarDef.Create("scp.audio.area_echo.enabled", true, CVar.ARCHIVE | CVar.CLIENTONLY);

    /// <summary>
    ///     If false, area echos calculate with 4 directions (NSEW).
    ///         Otherwise, area echos calculate with all 8 directions.
    /// </summary>
    /// <seealso cref="AreaEchoSystem"/>
    public static readonly CVarDef<bool> EchoHighResolution =
        CVarDef.Create("scp.audio.area_echo.alldirections", false, CVar.ARCHIVE | CVar.CLIENTONLY);


    /// <summary>
    ///     How many times a ray can bounce off a surface for an echo calculation.
    /// </summary>
    /// <seealso cref="AreaEchoSystem"/>
    public static readonly CVarDef<int> EchoReflectionCount =
        CVarDef.Create("scp.audio.area_echo.max_reflections", 1, CVar.ARCHIVE | CVar.CLIENTONLY);

    /// <summary>
    ///     Distantial interval, in tiles, in the rays used to calculate the roofs of an open area for echos,
    ///         or the ray's distance to space, at which the tile at that point of the ray is processed.
    ///
    ///     The lower this is, the more 'predictable' and computationally heavy the echoes are.
    /// </summary>
    /// <seealso cref="AreaEchoSystem"/>
    public static readonly CVarDef<float> EchoStepFidelity =
        CVarDef.Create("scp.audio.area_echo.step_fidelity", 5f, CVar.CLIENTONLY);

    /// <summary>
    ///     Interval between updates for every audio entity.
    /// </summary>
    /// <seealso cref="AreaEchoSystem"/>
    public static readonly CVarDef<int> EchoRecalculationInterval =
        CVarDef.Create("scp.audio.area_echo.recalculation_interval", 1, CVar.ARCHIVE | CVar.CLIENTONLY);

    #endregion

    #region Muffle

    /// <summary>
    /// Будет ли подавление звуков в зависимости от видимости работать?
    /// </summary>
    public static readonly CVarDef<bool> AudioMufflingEnabled =
        CVarDef.Create("scp.audio_muffling_enabled", true, CVar.CLIENTONLY | CVar.ARCHIVE);

    /// <summary>
    /// Будет ли использована частая проверка параметров для подавления звуков?
    /// </summary>
    public static readonly CVarDef<bool> AudioMufflingHighFrequencyUpdate =
        CVarDef.Create("scp.audio_muffling_use_high_frequency_update", false, CVar.CLIENTONLY | CVar.ARCHIVE);

    #endregion
}
