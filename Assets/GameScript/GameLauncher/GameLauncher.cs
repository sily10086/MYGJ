﻿using Cysharp.Threading.Tasks;
using UnityEngine;

namespace GameScript
{
    public class GameLauncher:MonoBehaviour
    {
        [SerializeField] private GameObject _groundManager;
        [SerializeField] private GameObject _eventManager;
        [SerializeField] private GameObject _playerController;
        [SerializeField] private GameObject _uIManager;
        
        private void Awake()
        {
            _eventManager = Instantiate(_eventManager);
            _eventManager.transform.position = Vector3.zero;
            _eventManager.SetActive(true);
            
            _groundManager = Instantiate(_groundManager);
            _groundManager.transform.position = Vector3.zero;
            _groundManager.SetActive(true);
            
            _playerController = Instantiate(_playerController);
            _playerController.transform.position = Vector3.zero;
            _playerController.SetActive(true);
            
            _uIManager = Instantiate(_uIManager);
            _uIManager.transform.position = Vector3.zero;
            _uIManager.SetActive(true);
        }

        private void Start()
        {
            GameData.GameData.Init();
        }
    }
}