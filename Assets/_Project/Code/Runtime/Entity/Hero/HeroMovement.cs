using Code.Runtime.Data;
using Code.Runtime.Data.PlayerData;
using Code.Runtime.Logic;
using Code.Runtime.Services.PersistentProgress;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;

namespace Code.Runtime.Entity.Hero
{
    [RequireComponent(typeof(CharacterController))]
    public class HeroMovement : MonoBehaviour, ISave
    {
        [SerializeField] private HeroStats _stats;
        private CharacterController _characterController;
        private PlayerControls _controls;

        private float MovementSpeed => _stats.Stats["MoveSpeed"].Value;
        
        private readonly float _rollForce = 1000f;
        private Camera _camera;

        [Inject]
        private void Construct(PlayerControls controls)
        {
            _controls = controls;
        }
        public void UpdateData(PlayerData data)
        {
            data.WorldData.PositionOnLevel = new PositionOnLevel(CurrentLevel(),
                transform.position.AsVectorData());
        }

        public void LoadData(PlayerData data)
        {
            if (CurrentLevel() == data.WorldData.PositionOnLevel.Level)
            {
                var savedPosition = data.WorldData.PositionOnLevel.Position;
                if (savedPosition != null) Warp(savedPosition);
            }
        }

        private void Awake()
        {
            _camera = Camera.main;
            _characterController = GetComponent<CharacterController>();
        }

        public void DashTask(float dashTime, float dashSpeed)
        {
            var movementVector =
                _camera.transform.TransformDirection(_controls.Player.Movement.ReadValue<Vector2>());
            if (movementVector.sqrMagnitude < Constants.Epsilon)
            {
                movementVector = transform.forward;
            }
            movementVector.y = 0;
            movementVector.Normalize();

            transform.forward = movementVector;
            

            Dash(movementVector, dashTime, dashSpeed).Forget();
        }

        private async UniTaskVoid Dash(Vector3 movementVector, float dashTime, float dashSpeed)
        {
            float startTime = Time.time;

            while (Time.time < startTime + dashTime)
            {
                _characterController.Move(movementVector * dashSpeed * Time.deltaTime);
                movementVector.y = 0;
                movementVector += Physics.gravity;
                await UniTask.Yield();
            }
        }

        public void Movement()
        { 
            var movementVector = Vector3.zero;

            var moveAxis = _controls.Player.Movement.ReadValue<Vector2>();

            if (moveAxis.sqrMagnitude > Constants.Epsilon)
            {
                movementVector = _camera.transform.TransformDirection(moveAxis);
                movementVector.y = 0;
                movementVector.Normalize();
            }

            movementVector += Physics.gravity;

            _characterController.Move(movementVector * MovementSpeed * Time.deltaTime);
        }

        private void Warp(Vector3Data to)
        {
            _characterController.enabled = false;
            transform.position = to.AsUnityVector().AddY(_characterController.height);
            _characterController.enabled = true;
        }

        private static string CurrentLevel()
        {
            return SceneManager.GetActiveScene().name;
        }
    }
}