using System;
using Unity1week202306.InGame.CheckPoints;
using Unity1week202306.InGame.Inputs;
using Unity1week202306.InGame.Umbrella;
using Unity1week202306.InGame.Wind;
using UnityEngine;

namespace Unity1week202306.InGame.Players
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody2D _rigidbody2D;

        [SerializeField]
        private float _moveSpeed = 1f;

        [SerializeField]
        private float _jumpPower = 8f;

        [Header("GroundCheck")]
        [SerializeField]
        private Transform _groundCheck;

        [SerializeField]
        private Vector2 _groundCheckSize = new Vector2(0.2f, 0.05f);

        [SerializeField]
        private UmbrellaController _umbrellaController;

        [SerializeField]
        private float _windResistanceRatio = 1f;

        [SerializeField]
        private float _windResistanceRatioWhenNotOnGround = 1f;

        [Header("Gravity")]
        [SerializeField]
        private float _baseGravityScale = 0.8f;

        [SerializeField]
        private float _minGravityScale = 0.4f;

        [Header("Animation")]
        [SerializeField]
        private PlayerAnimation _playerAnimation;

        [Header("CheckPoint")]
        [SerializeField]
        private CheckPointDetector _checkPointDetector;

        public UmbrellaController UmbrellaController => _umbrellaController;

        private InputData _inputData = InputData.Neutral;
        private WindSpeed _receivedWindSpeed = WindSpeed.Zero;
        private Vector2 umbrellaAdditionalForce = Vector2.zero;
        private bool _isGrounded;
        private bool _isActivate;
        private bool _isActiveGravity = true;

        public IObservable<CheckPointObject> OnCheckPointerAsObservable() => _checkPointDetector.OnCheckPointEnter;

        /// <summary>
        /// 入力を受け付けるか
        /// </summary>
        public void Activate(bool isActivate)
        {
            _isActivate = isActivate;
        }

        public void ActiveGravity(bool isActive)
        {
            _isActiveGravity = isActive;
            UpdateGravity();
        }

        private void UpdateGravity()
        {
            if (!_isActiveGravity)
            {
                _rigidbody2D.gravityScale = 0f;
                return;
            }

            var isNotGround = !CanJump();

            // 落下向きと傘の向きが逆なら落下速度を抑える
            if (_rigidbody2D.velocity.y < 0)
            {
                var dot = Vector2.Dot(
                    _umbrellaController.Direction.normalized,
                    Vector2.up);

                _rigidbody2D.gravityScale = isNotGround && _umbrellaController.IsOpened.Value
                    ? Mathf.Lerp(_baseGravityScale, _minGravityScale, dot)
                    : _baseGravityScale;
            }
        }

        public void SetInputData(InputData inputData)
        {
            if (!_isActivate)
            {
                return;
            }

            _inputData = inputData;

            if (!_isGrounded && CanJump())
            {
                OnTouchGround();
            }
            else if (!CheckOnGround())
            {
                _isGrounded = false;
            }

            var isNotGround = !CanJump();

            // 落下向きと傘の向きが逆なら落下速度を抑える
            if (_rigidbody2D.velocity.y < 0)
            {
                var dot = Vector2.Dot(
                    _umbrellaController.Direction.normalized,
                    Vector2.up);

                _rigidbody2D.gravityScale = isNotGround && _umbrellaController.IsOpened.Value
                    ? Mathf.Lerp(_baseGravityScale, _minGravityScale, dot)
                    : _baseGravityScale;
            }

            DoUpdate();
        }

        public bool CanJump()
        {
            return CheckOnGround();
        }

        private bool CheckOnGround()
        {
            return Physics2D.OverlapBox(
                _groundCheck.position,
                _groundCheckSize,
                0,
                LayerMask.GetMask("Default"));
        }

        private void DoUpdate()
        {
            _rigidbody2D.velocity = new Vector2(_inputData.Horizontal * _moveSpeed, _rigidbody2D.velocity.y);

            if (_inputData.Horizontal != 0)
            {
                _playerAnimation.SetDirection(new Vector2(_inputData.Horizontal, 0));
            }

            if (!CanJump())
            {
                _playerAnimation.OnFalling();
            }
            else if (CanJump())
            {
                if (_inputData.Horizontal != 0)
                {
                    _playerAnimation.SetWalk(new Vector2(_inputData.Horizontal, 0));
                }
                else
                {
                    _playerAnimation.Idle();
                }
            }

            // 傘による風の影響
            umbrellaAdditionalForce = Vector2.zero;

            if (_umbrellaController.IsOpened.Value)
            {
                // 傘の向きと風向きが同じ場合, 加速する
                var dot = Vector2.Dot(_umbrellaController.Direction.normalized,
                    _receivedWindSpeed.Direction.normalized);

                // 同じ向きの場合
                if (dot > 0)
                {
                    var isGround = !CanJump();
                    // 風速
                    var xForce = _receivedWindSpeed.Value.x;

                    // 傘の向きと風向きの角度によって影響度を変える
                    xForce *= dot;

                    // 風の影響度を着地中と空中で分ける
                    xForce *= isGround ? _windResistanceRatio : _windResistanceRatioWhenNotOnGround;


                    umbrellaAdditionalForce = new Vector2(xForce, 0);
                }
            }

            // 風の影響を受ける
            var windSpeed = _receivedWindSpeed.Value + umbrellaAdditionalForce;
            _rigidbody2D.AddForce(windSpeed, ForceMode2D.Force);

            if (_inputData.IsJump)
            {
                if (CanJump())
                {
                    _isGrounded = false;
                    _playerAnimation.OnJumping();
                    _rigidbody2D.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);
                }
            }
        }

        private void OnTouchGround()
        {
            _isGrounded = true;
            _playerAnimation.OnGrounded();
        }

        /// <summary>
        /// 風の空気抵抗値を設定
        /// </summary>
        /// <param name="windSpeed"></param>
        public void SetWindResistance(WindSpeed windSpeed)
        {
            _receivedWindSpeed = windSpeed;
        }

        public void SetPosition(Vector3 worldPosition)
        {
            transform.position = worldPosition;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(_groundCheck.position, _groundCheckSize);

            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(transform.position, umbrellaAdditionalForce.normalized);
        }
    }
}