using System;
using TinyLittleStudio.BambooBash.Utils;
using UnityEngine;

namespace TinyLittleStudio.BambooBash.Content
{
    public class Character : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private GameObject target;
        [SerializeField] private GameObject modelTarget;

        private bool isInitialized = false;

        private void Awake()
        {
            if (target == null || modelTarget == null)
            {
                throw new Exception("Missing Elements On Character-Script!");
            }
        }

        private void Update()
        {
            if (TouchUtils.Gestures.IsLeft)
            {
                GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
                gameObject.AddComponent<Rigidbody>();
                gameObject.AddComponent<BoxCollider>();

                gameObject.transform.SetParent(Manager.DefaultInstance.GeneralScene.transform);
                gameObject.transform.position = new Vector3(target.transform.position.x, target.transform.position.y + 6.0f, target.transform.position.z);
                gameObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

                gameObject.GetComponent<Renderer>().material.color = UnityEngine.Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
            }

            if (TouchUtils.Gestures.IsRight)
            {
                GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                gameObject.AddComponent<Rigidbody>();
                gameObject.AddComponent<SphereCollider>();

                gameObject.transform.SetParent(Manager.DefaultInstance.GeneralScene.transform);
                gameObject.transform.position = new Vector3(target.transform.position.x, target.transform.position.y + 6.0f, target.transform.position.z);
                gameObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

                gameObject.GetComponent<Renderer>().material.color = UnityEngine.Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
            }

            if (TouchUtils.Gestures.IsDown)
            {
                Manager.DefaultInstance.Reset();
            }
        }

        public void OnProfileChange()
        {
            if (Profile != null && !isInitialized)
            {
                GameObject model = Instantiate(Profile.Prefab, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
                model.transform.SetParent(modelTarget.transform);
                model.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            }
        }

        public Profile Profile { get; set; }
    }
}
