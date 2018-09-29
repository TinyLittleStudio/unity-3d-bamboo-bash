using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

namespace TinyLittleStudio.BambooBash.Utils
{
    namespace TinyLittleStudio.PocketJourney.Utils
    {
        public sealed class Loading : MonoBehaviour
        {
            private const float TIMEOUT_TIME = 5.0f;

            [Header("Settings")]
            [SerializeField]
            private float delay;

            [Header("UI")]
            [SerializeField]
            private Slider loadingBar;
            [SerializeField]
            private TextMeshProUGUI hint;

            [Space(10)]
            [SerializeField]
            private string[] hints;

            private Ping ping;
            private float pingTime;
            private bool isPinging;

            private bool hasInternetConnection = false;

            private void Awake()
            {
                if (hint != null && hints.Length > 0)
                {
                    hint.text = hints[Random.Range(0, hints.Length)];
                }
            }

            private void Start()
            {
                if (!hasInternetConnection && (Application.internetReachability != NetworkReachability.NotReachable))
                {
                    ping = new Ping(Configuration.URLs.GOOGLE_PUBLIC_DNS);
                    pingTime = Time.time;
                    isPinging = true;
                }
            }

            private void Update()
            {
                if (isPinging)
                {
                    if (Time.time - pingTime >= Loading.TIMEOUT_TIME)
                    {
                        hasInternetConnection = false;
                        isPinging = false;
                    }
                    else
                    {
                        if (ping.isDone)
                        {
                            hasInternetConnection = true;
                            isPinging = false;
                        }
                    }
                }
                else
                {
                    delay -= Time.deltaTime;

                    if (delay <= 0)
                    {
                        if (!hasInternetConnection)
                        {
                            Failure.SetNextReTry("No Internet Connection available!", Configuration.Scenes.LOADING);
                        }
                        else
                        {
                            StartCoroutine(LoadAsynchronously());
                        }
                    }
                }
            }

            private IEnumerator LoadAsynchronously()
            {
                Scene scene = SceneManager.GetSceneByName(GetNextScene());

                AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(GetNextScene());

                while (!asyncOperation.isDone)
                {
                    float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);

                    loadingBar.value = progress;

                    yield return null;
                }
                Notification.CancelAll();

                yield return null;
            }

            public string GetNextScene()
            {
                return Configuration.Scenes.GAME;
            }
        }
    }
}
