using UnityEngine;

namespace TinyLittleStudio.BambooBash.Utils
{
    public sealed class TouchUtils
    {
        private static readonly TouchUtils TOUCH_UTILS = new TouchUtils();

        private Gestures gestures;

        private TouchUtils()
        {
            this.gestures = new GameObject("[TouchUtils] Gestures").AddComponent<Gestures>();
        }

        public static Gestures Gestures => TouchUtils.TOUCH_UTILS.gestures;
    }
}
