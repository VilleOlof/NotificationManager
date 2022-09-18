# NotificationManager
A Small Backgrounds Application To Notify And Send Simple Reminders via .JSON Files.  
*Works Only On Windows 10 & Windows 11.*  

---
`FileName` Needs to Be The Exact Same As The Actual File Name.  
`Title` Is The Title Of The Notification.  
`Texts` Is A Collection Of One *(Or Multiple)* Texts Which Will Be Randomized During Send Out.  
`AttributionText` Is A Smaller Text Displayed At The Bottom Of The Notification *(Always Visible)*.  
`InlineImage` Displays An Image(Uri) Alongside The Rest.  
`CustomTimestamp` Shows A Custom DateTime In The Notification.  
`Days` Determines What Days During The Week It Should Be Sent Out On *(1,2,3,4,5,6,0)*.  
`ResetAtMidnight` Determines If All `Sent` Properties Should Be Reset To `false` At Midnight.  

`SendOutTime` Is An Array Of `SendOut` Which Has `TimeOnlyArray` And `Sent` Has Properties.  
`TimeOnlyArray` Is A `TimeOnly` Type But Filled In With Integers In JSON *(15,0,0,0 == 15:00)*.  
`Sent` Shouldn't Be Touched And Left `false` By Default, The Program Will Update This When Needed.

`AttributionText`, `InlineImage`, `CustomTimestamp` Can All Be Left `Null` To Leave Them Empty.  

## Example JSON File
```json
{
  "FileName": "water",
  "Title": "Hydration Time!",
  "Texts": [
    "Take One Sip Of Water!",
    "Take Two Sips Of Water!",
    "Take Three Sips Of Water!"
  ],
  "AttributionText": "Refill Your Bottle If You Haven't!",
  "InlineImage": null,
  "CustomTimestamp": null,
  "Days": [ 1, 2, 4, 6, 0 ],
  "SendOutTime": [
    {
      "TimeOnlyArray": [ 15, 0, 0, 0 ],
      "Sent": false
    },
    {
      "TimeOnlyArray": [ 17, 0, 0, 0 ],
      "Sent": false
    }
  ],
  "ResetAtMidnight": true
}
```
