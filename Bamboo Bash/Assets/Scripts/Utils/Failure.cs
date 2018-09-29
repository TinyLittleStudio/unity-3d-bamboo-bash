using UnityEngine;

namespace TinyLittleStudio.BambooBash.Utils
{
    public sealed class Failure : MonoBehaviour
    {
        private class Temporary
        {
            public string Scene { get; set; }

            public string Message { get; set; }
        }

        private static readonly Temporary temporary = new Temporary();

        private void Start()
        {
            string message = temporary.Message;

            if (message != null)
            {
                Notification.Notify(message, Notification.Level.ERROR);
            }
        }

        public void ReTry()
        {
            SceneUtils.Load(temporary.Scene);
        }

        public static void SetNextReTry(string message, string scene)
        {
            if (SceneUtils.Exists(scene))
            {
                temporary.Message = message;
                temporary.Scene = scene;

                SceneUtils.Load(Configuration.Scenes.FAILURE);
            }
        }
    }
}
