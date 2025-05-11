using UnityEngine;
using System;

#if UNITY_ANDROID
using Unity.Notifications.Android;
using UnityEngine.Android;
#endif

public class NotificationSystem : MonoBehaviour
{
#if UNITY_ANDROID
    private const string DefaultChannelId = "default_channel";

    private void Start()
    {
        RequestNotificationPermission();
        CreateNotificationChannel(DefaultChannelId, "Default Channel");
    }

    public void RequestNotificationPermission()
    {
        if (!Permission.HasUserAuthorizedPermission("android.permission.POST_NOTIFICATIONS"))
        {
            Permission.RequestUserPermission("android.permission.POST_NOTIFICATIONS");
        }
    }

    public void CreateNotificationChannel(string channelId, string channelName)
    {
        AndroidNotificationChannel channel = new AndroidNotificationChannel
        {
            Id = channelId,
            Name = channelName,
            Importance = Importance.Default,
            Description = "Canal para notificaciones predeterminadas"
        };

        AndroidNotificationCenter.RegisterNotificationChannel(channel);
    }

    public void SendNotification(string title, string message, int fireTimeInSeconds = 0)
    {
        AndroidNotification notification = new AndroidNotification
        {
            Title = title,
            Text = message,
            FireTime = DateTime.Now.AddSeconds(fireTimeInSeconds),
            SmallIcon = "icon_0",
            LargeIcon = "icon_1"
        };

        AndroidNotificationCenter.SendNotification(notification, DefaultChannelId);
    }
#endif
}
