using UnityEngine;
using TMPro;
using System.Collections;
using TinyLittleStudio.BambooBash.Utils;
using Vuforia;
using TinyLittleStudio.BambooBash.Content;
using System;
using UnityEngine.SceneManagement;

namespace TinyLittleStudio
{
    public class Manager : MonoBehaviour
    {
        private static Manager defaultInstance;

        [Header("Collections")]
        [SerializeField] private Profile[] data;

        [Header("Profiles UI")]
        [SerializeField] private TextMeshProUGUI nameLabel;
        [SerializeField] private TextMeshProUGUI descriptionLabel;
        [SerializeField] private Transform preview;

        [Header("Screens")]
        [SerializeField] private GameObject startScreen;
        [SerializeField] private GameObject endScreen;
        [SerializeField] private GameObject contentScene;

        [SerializeField] private Character template;

        private GameObject previewGameObject;

        private int index = 0;

        private void Awake()
        {
            if (defaultInstance != null)
            {
                throw new Exception("Multiple Manager-Scripts Found!");
            }
            defaultInstance = this;

            DisableAR();

            StartScreen.SetActive(true);
            EndScreen.SetActive(false);

            Change(0);

            StartCoroutine(Late());

            SceneUtils.RequestTransition(new Transition(TransitionType.FADE_IN));
        }

        private void Update()
        {
            if (previewGameObject != null)
            {
                previewGameObject.transform.Rotate(new Vector3(0.2f, 1.0f, 0.2f), 0.5f);
            }
        }

        private IEnumerator Late()
        {
            yield return new WaitForSeconds(0.5f);

            OnProfileChange();
        }

        private void OnProfileChange()
        {
            CurrentProfile = data[index];

            if (nameLabel != null)
            {
                nameLabel.text = CurrentProfile.Name;
            }

            if (descriptionLabel != null)
            {
                descriptionLabel.text = CurrentProfile.Description;
            }

            if (preview != null)
            {
                Clear();

                previewGameObject = Instantiate(CurrentProfile.Prefab, preview);

                if (previewGameObject != null)
                {
                    previewGameObject.transform.SetParent(preview);
                    previewGameObject.transform.localScale = new Vector3(1, 1, 1);
                    previewGameObject.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 5.0f;
                }
            }
        }

        public void Clear()
        {
            if (previewGameObject != null)
            {
                Destroy(previewGameObject);
            }
        }

        public void Reset()
        {
            Application.LoadLevel(Application.loadedLevel);
        }

        public void Next()
        {
            index++;

            if (index > data.Length - 1)
            {
                index = 0;
            }
            OnProfileChange();
        }

        public void Prev()
        {
            index--;

            if (index < 0)
            {
                index = data.Length - 1;
            }
            OnProfileChange();
        }

        public void Change(int index)
        {
            if (index > -1 && index < data.Length - 1)
            {
                this.index = index;
            }
            OnProfileChange();
        }

        public void DisableAR()
        {
            VuforiaBehaviour.Instance.enabled = false;
        }

        public void EnableAR()
        {
            VuforiaBehaviour.Instance.enabled = true;
        }

        public Profile CurrentProfile { get; private set; }

        public string Username { get; set; }

        public GameObject StartScreen => startScreen;

        public GameObject EndScreen => endScreen;

        public GameObject GeneralScene => contentScene;

        public Character Template => template;

        public static Manager DefaultInstance => Manager.defaultInstance;
    }
}
