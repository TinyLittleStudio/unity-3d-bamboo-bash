using System;
using UnityEngine;

namespace TinyLittleStudio.BambooBash.Content
{
    public sealed class Controls : MonoBehaviour
    {
        public static readonly string[] DISABLED_TAGS = {
            Configuration.Tags.PLAYER,
        };

        public static readonly float VELOCITY = 40.5f;

        private GameObject target;

        private void Awake()
        {
            Transform camera = Camera.main.transform;

            if (camera != null)
            {
                transform.SetParent(camera);
                transform.rotation = camera.rotation;
                transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane));
            }
            else
            {
                throw new Exception("Could Not Find Main Camera!");
            }
            Destination = Vector3.zero;
        }

        private void Update()
        {
            RaycastHit raycastHit;

            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out raycastHit, Mathf.Infinity))
            {
                if (Array.IndexOf(Controls.DISABLED_TAGS, raycastHit.transform.tag) >= 0)
                {
                    return;
                }

                if (target == null && Manager.DefaultInstance.Template != null)
                {
                    Character character = Instantiate(Manager.DefaultInstance.Template.gameObject, Destination, Quaternion.identity).GetComponent<Character>();

                    if (character != null)
                    {
                        character.Profile = Manager.DefaultInstance.CurrentProfile;
                        character.OnProfileChange();

                        target = character.gameObject;
                    }
                }
                Destination = raycastHit.point;
            }
            else
            {
                Destination = Vector3.zero;
            }

            if (Destination != Vector3.zero && target != null)
            {
                target.transform.position = Vector3.MoveTowards(target.transform.position, Destination, Controls.VELOCITY * Time.deltaTime);
            }
        }

        public Vector3 Destination { get; private set; }
    }
}
