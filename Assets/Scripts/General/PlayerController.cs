using System;
using UnityEngine;
using Utils;
using static Utils.PlayerUtils;
using static Utils.MobileControl;

namespace General {
    public class PlayerController : MonoBehaviour {

        public static Action OnObstaclePassed;
        public static event Action<HorizontalPos> OnPlayerMovedH;

        [SerializeField] public GameObject dieEffect;

        public PlayerState state;

        private string _obsPassed = "";
        private Timer _timerDown;
        private Timer _timerUp;

        protected Animator Anim;

        protected void Start() {
            Setup();
        }

        protected void Update() {
            SwipeCheck();
            PCControl();

            UpdateUpAndDownTimers();
        }

        protected void OnEnable() {
            _timerDown = new Timer(1f);
            _timerUp = new Timer(1f);
            OnSwipe += Move;
            _timerDown.OnTimerFinished += GoUp;
            _timerUp.OnTimerFinished += GoDown;
        }

        private void OnDisable() {
            OnSwipe -= Move;
            _timerDown.OnTimerFinished -= GoUp;
            _timerUp.OnTimerFinished -= GoDown;
        }

        protected void OnCollisionEnter(Collision other) {
            if (other.gameObject.CompareTag("Obstacle")) {
                HandlePlayerDeath();
            }
        }

        public void OnTriggerEnter(Collider other) {
            if (other.gameObject.CompareTag("Obstacle")) {
                _timerDown.ResetTimer();
                _timerUp.ResetTimer();
            }
        }

        public void OnTriggerExit(Collider other) {
            if (!other.gameObject.CompareTag("Obstacle")) {
                return;
            }

            if (other.gameObject.name != _obsPassed) //in order not to get 2 points for passing complex obs
            {
                OnObstaclePassed?.Invoke();
            }

            switch (state.vPos) {

                case VerticalPos.Down:
                    GoUp();
                    break;
                case VerticalPos.Up:
                    GoDown();
                    break;
            }

            _obsPassed = other.gameObject.name;
        }

        protected void Setup() {
            Anim = GetComponent<Animator>();
            state = PlayerState.GetDefaultClassicState();
        }

        protected void HandlePlayerDeath() {
            Vector3 diePos = new(transform.position.x, 0.5f, transform.position.z);
            Instantiate(dieEffect, diePos, transform.rotation);
            gameObject.SetActive(false);
            GameManager.Instance.LevelEnded();
        }

        private void PCControl() {
            if (GameManager.Instance.IsGamePaused) {
                return;
            }

            if (state.vPos != VerticalPos.Up) {
                if (Input.GetKeyDown(KeyCode.LeftArrow)) {
                    GoLeft();
                } else if (Input.GetKeyDown(KeyCode.RightArrow)) {
                    GoRight();
                }
            }

            if (Input.GetKeyDown(KeyCode.UpArrow)) {
                GoUp();
            } else if (Input.GetKeyDown(KeyCode.DownArrow)) {
                GoDown();
            }
        }

        private void Move(Vector2 startPos, Vector2 endPos) {
            var length = endPos - startPos;
            float xLength = Mathf.Abs(length.x);
            float yLength = Mathf.Abs(length.y);

            if (GameManager.Instance.IsGamePaused) {
                return;
            }

            if (xLength > yLength) {
                if (length.x > SwipeDistance
                    && state.vPos != VerticalPos.Up) {
                    GoRight();
                } else if (length.x < -SwipeDistance && state.vPos != VerticalPos.Up) {
                    GoLeft();
                }

            } else if (xLength < yLength) {
                if (length.y > SwipeDistance) {
                    GoUp();
                } else if (length.y < -SwipeDistance && state.vPos != VerticalPos.Down) {
                    GoDown();
                }
            }
        }

        private void GoDown() {
            _timerUp.ResetTimer();

            state.vPos = state.vPos switch
            {
                VerticalPos.Up => VerticalPos.Middle,
                VerticalPos.Middle => VerticalPos.Down,
                _ => state.vPos
            };

            if (Anim.GetFloat(ANIM_VPOS) > -1f) {
                Anim.SetFloat(ANIM_VPOS, Anim.GetFloat(ANIM_VPOS) - 1f);
            }
        }

        private void GoUp() {
            _timerDown.ResetTimer();

            state.vPos = state.vPos switch
            {
                VerticalPos.Middle => VerticalPos.Up,
                VerticalPos.Down => VerticalPos.Middle,
                _ => state.vPos
            };
            
            if (Anim.GetFloat(ANIM_VPOS) < 1f) {
                Anim.SetFloat(ANIM_VPOS, Anim.GetFloat(ANIM_VPOS) + 1f);
            }
        }

        private void GoLeft() {
            if (Anim.GetFloat(ANIM_HPOS) > -1f) {
                Anim.SetFloat(ANIM_HPOS, Anim.GetFloat(ANIM_HPOS) - 1f);
            }

            UpdateHPos();
        }

        private void GoRight() {
            if (Anim.GetFloat(ANIM_HPOS) < 1f) {
                Anim.SetFloat(ANIM_HPOS, Anim.GetFloat(ANIM_HPOS) + 1f);
            }

            UpdateHPos();
        }

        private void UpdateHPos() {
            state.hPos = Anim.GetFloat(ANIM_HPOS) switch
            {
                -1f => HorizontalPos.Left,
                0f => HorizontalPos.Middle,
                1f => HorizontalPos.Right,
                _ => state.hPos
            };

            OnPlayerMovedH?.Invoke(state.hPos);
        }

        private void UpdateUpAndDownTimers() {

            switch (state.vPos) {
                case VerticalPos.Down:
                    _timerDown.ExecuteTimer();
                    break;
                case VerticalPos.Up:
                    _timerUp.ExecuteTimer();
                    break;
            }
        }
    }
}