using System;
using Common.Event;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameScript
{
    public class GameLauncher:MonoBehaviour
    {
        [SerializeField] private GameObject _groundManager;
        [SerializeField] private GameObject _eventManager;
        [SerializeField] private GameObject _playerController;
        private void Awake()
        {
            _eventManager = Instantiate(_eventManager);
            _eventManager.transform.position = Vector3.zero;
            _groundManager = Instantiate(_groundManager);
            _groundManager.transform.position = Vector3.zero;
            _playerController = Instantiate(_playerController);
            _playerController.transform.position = Vector3.zero;
        }

        private void Start()
        {
            
        }

        private void Update()
        {
            
        }
    }
}