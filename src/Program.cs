using Microsoft.Toolkit.Uwp.Notifications;
using Newtonsoft.Json;

namespace NotificationManager;

internal sealed class Program
{
    private static readonly string DataPath = @".\Data\";

    private static readonly int CheckInterval = 20 * 1000;
    private static readonly Random rng = new();

    internal static List<Notification> Notifications = new();

    public static async Task Main(string[] args)
    {
        await LoadJSON();

        await StartUpCheck();

        await CycleAsync();
        
        await Task.Delay(-1);
    }
    
    private static async Task StartUpCheck()
    {
        if (!Directory.Exists(DataPath))
        {
            Directory.CreateDirectory(DataPath);
        }
        
        foreach (var notification in Notifications)
        {
            if (!notification.Days.Contains(DateTime.Now.DayOfWeek)) { continue; }
            
            foreach (var sendOut in notification.SendOutTime)
            {
                TimeOnly SendOutTime = GetTimeFromArray(sendOut.TimeOnlyArray);
                
                //Check if sendOut has already happened, and if then change its .Sent to true
                if (DateTime.Now.TimeOfDay > SendOutTime.ToTimeSpan())
                {
                    sendOut.Sent = true;

                    await UpdateNotificationFileAsync(notification);
                }
            }
        }
    }

    private static async Task CycleAsync()
    {
        while (true)
        {
            await TryNotificationSendAsync();

            await ResetSentAsync();

            await Task.Delay(CheckInterval);
        }
    }

    private static async Task TryNotificationSendAsync()
    {
        foreach (var notification in Notifications)
        {
            if (!notification.Days.Contains(DateTime.Now.DayOfWeek)) { continue; }

            foreach (var sendOut in notification.SendOutTime)
            {
                TimeOnly SendOutTime = GetTimeFromArray(sendOut.TimeOnlyArray);

                if ((SendOutTime.ToTimeSpan() <= DateTime.Now.TimeOfDay) && !sendOut.Sent)
                {
                    NewToast(notification).Show();
                    sendOut.Sent = true;

                    //Update The Notification Properties. Mostly For notification.Sent
                    await UpdateNotificationFileAsync(notification);
                }
            }
        }
    }

    private static async Task ResetSentAsync()
    {
        //Reset notification.Sent to get it ready for the next day
        if (DateTime.Now.Hour != 0 && DateTime.Now.Minute != 0) { return; }

        foreach (var notification in Notifications)
        {
            if (!notification.ResetAtMidnight) { continue; }

            foreach (var sendout in notification.SendOutTime)
            {
                sendout.Sent = false;
            }

            await UpdateNotificationFileAsync(notification);
        }
    }

    private static TimeOnly GetTimeFromArray(int[] times) => new(times[0], times[1], times[2], times[3]);

    private static async Task UpdateNotificationFileAsync(Notification notification) 
         => await File.WriteAllTextAsync(
            $"{DataPath}{notification.FileName}.json", 
            JsonConvert.SerializeObject(notification, Formatting.Indented));

    private static async Task LoadJSON()
    {
        foreach (var file in Directory.GetFiles(DataPath))
        {
            var content = await File.ReadAllTextAsync(file);

            var notification = JsonConvert.DeserializeObject<Notification>(content) ?? throw new Exception($"Could Not Load: {file}");

            Notifications.Add(notification);
        }
    }

    private static ToastContentBuilder NewToast(Notification notification)
    {
        var Toast = new ToastContentBuilder();

        Toast.AddText(notification.Title);
        
        int index = rng.Next(0,notification.Texts.Length);
        Toast.AddText(notification.Texts[index]);
        
        if (notification.AttributionText is not null)
        {
            Toast.AddAttributionText(notification.AttributionText);
        }

        if (notification.InlineImage is not null)
        {
            Toast.AddInlineImage(notification.InlineImage);
        }

        if (notification.CustomTimestamp is not null)
        {
            Toast.AddCustomTimeStamp(notification.CustomTimestamp.Value);
        }

        Toast.SetToastScenario(ToastScenario.Reminder);

        return Toast;
    }
}
