using System.Collections.Generic;

namespace TinyLittleStudio.BambooBash.Utils
{
    public class TransitionType
    {
        public static readonly TransitionType FADE_IN = new TransitionType("Scene_Fade_In");

        public static readonly TransitionType FADE_OUT = new TransitionType("Scene_Fade_Out");

        public static IEnumerator<TransitionType> Values
        {
            get
            {
                yield return FADE_IN;

                yield return FADE_OUT;
            }
        }

        private TransitionType(string animationKey)
        {
            AnimationKey = animationKey;
        }

        public string AnimationKey { get; private set; }

        public override string ToString()
        {
            return AnimationKey;
        }
    }
}
