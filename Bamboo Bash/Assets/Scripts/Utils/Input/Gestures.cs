using UnityEngine;

namespace TinyLittleStudio.BambooBash.Utils
{
    public sealed class Gestures : MonoBehaviour
    {
        private bool isDoubleTap = false, isTap, isLeft, isRight, isUp, isDown, isDrag = false;

        private int taps;

        private Vector2 origin, delta;

        private float time;

        private void Awake()
        {
            Magnitude = 60;

            DontDestroyOnLoad(this);
        }

        private void Update()
        {
            isTap = isDoubleTap = isLeft = isRight = isUp = isDown = false;

            #region Standalone Inputs
            if (Input.GetMouseButtonDown(0))
            {
                isTap = true;
                isDrag = true;
                origin = Input.mousePosition;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                isDrag = false;
                origin = delta = Vector2.zero;
            }
            #endregion

            #region Mobile Inputs
            if (Input.touches.Length > 0)
            {
                if (Input.touches[0].phase == TouchPhase.Began)
                {
                    isDrag = true;
                    isTap = true;
                    origin = Input.touches[0].position;
                }
                else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
                {
                    isDrag = false;
                    origin = delta = Vector2.zero;
                }
            }
            #endregion

            delta = Vector2.zero;

            if (isDrag)
            {
                if (Input.touches.Length > 0)
                {
                    delta = Input.touches[0].position - origin;
                }
                else if (Input.GetMouseButton(0))
                {
                    delta = (Vector2)Input.mousePosition - origin;
                }
            }

            if (delta.magnitude > Magnitude)
            {
                float x = delta.x;
                float y = delta.y;

                if (Mathf.Abs(x) > Mathf.Abs(y))
                {
                    if (x < 0)
                    {
                        isLeft = true;
                    }
                    else
                    {
                        isRight = true;
                    }
                }
                else
                {
                    if (y < 0)
                    {
                        isDown = true;
                    }
                    else
                    {
                        isUp = true;
                    }
                }
                origin = delta = Vector2.zero;
                isDrag = false;
            }

            if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                taps++;
            }

            if (Application.isEditor)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    taps++;
                }
            }

            if (taps > 0)
            {
                time += Time.deltaTime;

                if (taps >= 2)
                {
                    isDoubleTap = true;

                    taps = 0;
                    time = 0.0f;
                }
            }

            if (time > 0.5f)
            {
                time = 0f;
                taps = 0;
            }
        }

        public int Magnitude { get; set; }

        public bool IsLeft => isLeft;

        public bool IsRight => isRight;

        public bool IsUp => isUp;

        public bool IsDown => isDown;

        public bool IsTap => isTap;

        public bool IsDoubleTap => isDoubleTap;

        public bool IsDrag => isDrag;
    }
}
