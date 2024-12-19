namespace bexio.net.Models.Projects.Timesheet.Tracking;

/// <summary>
/// Base class for the tracking entity.
/// Possible values for "type" are:
///     "duration"
///     "range"
///     "stopwatch"
/// </summary>
public abstract record TrackingBase
{
    public virtual string Type => "?";
}