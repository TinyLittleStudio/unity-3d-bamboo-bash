using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TinyLittleStudio.BambooBash.Utils
{
    public sealed class Notification : MonoBehaviour
    {
        public enum Level
        {
            INFO = 0x969696,

            SUCCESS = 0x5ECA47,

            WARNING = 0xBF8A39,

            ERROR = 0xD94949,
        }

        private class NotificationRequest
        {
            public NotificationRequest(string message, Level level)
            {
                this.Message = message;
                this.Level = level;
            }

            public string Message { get; }

            public Level Level { get; }
        }

        private static Notification singleton;

        public const int LIMIT = 10;

        [Header("Notification Settings")]

        [SerializeField] private float duration = 3.25f;
        [SerializeField] private float delay = 1.5f;

        [Header("Notification Elements")]

        [SerializeField] private RectTransform container;
        [SerializeField] private TextMeshProUGUI message;
        [SerializeField] private Image background;
        [SerializeField] private new Animation animation;

        private Queue<NotificationRequest> requests = new Queue<NotificationRequest>();
        private float time;

        private NotificationRequest last;

        private bool isReady, isVisible;

        private void Awake()
        {
            if (singleton == null)
            {
                singleton = this;
            }
            else
            {
                Destroy(this);

                if (Application.isEditor)
                {
                    Debug.LogError("Multiple 'Notification' Scripts Found!");
                }
                return;
            }
            Initialize();
        }

        private void Initialize()
        {
            if (container == null || message == null || background == null || animation == null)
            {
                Debug.LogError("Notifcation: Missing Components!");
            }
            else
            {
                isReady = true;
            }
        }

        private void Update()
        {
            if ((requests.Count > 0 || isVisible) && !animation.isPlaying)
            {
                time -= Time.deltaTime;

                if (time <= 0)
                {
                    if (!isVisible)
                    {
                        Show();
                    }
                    else
                    {
                        Hide();
                    }
                }
            }
        }

        private void Show()
        {
            NotificationRequest notificationRequest = requests.Dequeue();

            last = notificationRequest;

            message.text = notificationRequest.Message;
            int color = (int)notificationRequest.Level;

            background.color = new Color32
            {
                b = (byte)((color) & 0xFF),
                g = (byte)((color >> 8) & 0xFF),
                r = (byte)((color >> 16) & 0xFF),
                a = 255
            };

            animation.Play("Notification_Show");
            isVisible = true;
            time = duration;
        }

        private void Hide()
        {
            last = null;
            animation.Play("Notification_Hide");
            isVisible = false;
            time = delay;
        }

        public void Cancel()
        {
            requests.Clear();
            time = 0;
        }

        public void LogInfo(string message)
        {
            Log(message, Level.INFO);
        }

        public void LogError(string message)
        {
            Log(message, Level.ERROR);
        }

        public void LogWarning(string message)
        {
            Log(message, Level.WARNING);
        }

        public void LogSuccess(string message)
        {
            Log(message, Level.SUCCESS);
        }

        public void Log(string message, Level level)
        {
            if (last != null && last.Message == message)
            {
                return;
            }
            requests.Enqueue(new NotificationRequest(message, level));

            if (requests.Count > Notification.LIMIT)
            {
                requests.Dequeue();
            }
        }

        public static void Notify(string message)
        {
            Notify(message, Level.INFO);
        }

        public static void Notify(string message, Level level)
        {
            Notification notification = singleton;

            if (notification != null && notification.isReady)
            {
                notification.Log(message, level);
            }
        }

        public static void CancelAll()
        {
            Notification notification = singleton;

            if (notification != null)
            {
                notification.Cancel();
            }
        }
    }
}