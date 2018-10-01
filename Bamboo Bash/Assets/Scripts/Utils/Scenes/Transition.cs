using UnityEngine;
using UnityEngine.SceneManagement;

namespace TinyLittleStudio.BambooBash.Utils
{
    public sealed class Transition
    {
        private readonly int sceneIndex = -1;
        private readonly string sceneName = null;

        private bool isLoaded = false;

        public Transition(TransitionType transitionType)
        {
            TransitionType = transitionType;
        }

        public Transition(TransitionType transitionType, int scene)
        {
            TransitionType = transitionType;

            this.sceneIndex = scene;
        }

        public Transition(TransitionType transitionType, string scene)
        {
            TransitionType = transitionType;

            this.sceneName = scene;
        }

        public void LoadNextScene()
        {
            if (!isLoaded)
            {
                if (SceneUtils.Load(sceneIndex) || SceneUtils.Load(sceneName))
                {
                    isLoaded = true;
                }
            }
        }

        public TransitionType TransitionType { get; private set; }

        public override string ToString()
        {
            return TransitionType.ToString();
        }
    }
}
