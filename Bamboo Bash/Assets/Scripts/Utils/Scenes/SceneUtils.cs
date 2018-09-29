using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TinyLittleStudio.BambooBash.Utils
{
    public sealed class SceneUtils : MonoBehaviour
    {
        private static SceneUtils singleton;

        private Queue<Transition> transitions = new Queue<Transition>();
        private bool isPlaying = false;

        private new Animation animation;

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
                    Debug.LogError("Multiple 'SceneUtils' Scripts Found!");
                }
                return;
            }
            Initialize();
        }

        private void Initialize()
        {
            GameObject gameObject = GameObject.FindGameObjectWithTag("Overlay");

            if (gameObject != null)
            {
                animation = (animation ?? (animation = gameObject.GetComponent<Animation>()));
            }
        }

        private void Update()
        {
            if (transitions.Count > 0)
            {
                if (!isPlaying)
                {
                    Transition transition = transitions.Dequeue();
                    Transition(transition);
                }
            }
        }

        private void Transition(Transition transition)
        {
            StartCoroutine(TransitionCoroutine(transition));
        }

        private IEnumerator TransitionCoroutine(Transition transition)
        {
            if (transition != null && animation != null)
            {
                if (!isPlaying)
                {
                    string key = transition.TransitionType.AnimationKey;

                    if (animation.GetClip(key) != null)
                    {
                        animation.Play(key);
                    }
                    isPlaying = true;
                }

                do
                {
                    yield return null;
                }
                while (animation.isPlaying);

                transition.LoadNextScene();

                isPlaying = false;
            }
        }

        public static void RequestTransition(Transition transition)
        {
            if (transition != null)
            {
                SceneUtils scenes = Instantiate();

                if (scenes != null)
                {
                    scenes.transitions.Enqueue(transition);
                }
            }
        }

        public static bool Load(int index)
        {
            if (Exists(index))
            {
                SceneManager.LoadScene(index);
                return true;
            }
            return false;
        }

        public static bool Load(string name)
        {
            if (Exists(name))
            {
                SceneManager.LoadScene(name);
                return true;
            }
            return false;
        }

        public static bool Exists(string name)
        {
            return Application.CanStreamedLevelBeLoaded(name);
        }

        public static bool Exists(int index)
        {
            return index >= 0 && SceneManager.sceneCountInBuildSettings > index;
        }

        private static SceneUtils Instantiate()
        {
            if (singleton == null)
            {
                GameObject gameObject = new GameObject()
                {
                    name = "[Scene Controls] Scene Utils"
                };
                gameObject.AddComponent<SceneUtils>();
            }
            return singleton;
        }
    }
}
