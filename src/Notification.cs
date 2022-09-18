namespace NotificationManager;

internal sealed class Notification
{
    public string FileName { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;

    public string[] Texts { get; set; } = Array.Empty<string>();
    public string? AttributionText { get; set; } = string.Empty;

    public Uri? InlineImage { get; set; } = default;
    public DateTime? CustomTimestamp { get; set; } = default;

    public DayOfWeek[] Days { get; set; } = Array.Empty<DayOfWeek>();
    public SendOut[] SendOutTime { get; set; } = default!;

    public bool ResetAtMidnight { get; set; } = false;

    public Notification(string fileName, string title, string[] texts, string? attributionText, Uri? inlineImage, DateTime? customTimestamp, DayOfWeek[] days, SendOut[] sendOutTime, bool resetAtMidnight)
    {
        FileName = fileName;
        Title = title;
        Texts = texts;
        AttributionText = attributionText;
        InlineImage = inlineImage;
        CustomTimestamp = customTimestamp;
        Days = days;
        SendOutTime = sendOutTime;
        ResetAtMidnight = resetAtMidnight;
    }
}

internal sealed class SendOut
{
    public int[] TimeOnlyArray { get; set; } = Array.Empty<int>();
    public bool Sent { get; set; } = false;

    public SendOut(int[] timeOnlyArray, bool sent)
    {
        TimeOnlyArray = timeOnlyArray;
        Sent = sent;
    }
}
