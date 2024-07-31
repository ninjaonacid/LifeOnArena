using System;
using System.Collections.Generic;
using Code.Runtime.Core.Audio;
using Code.Runtime.Services.PauseService;
using Code.Runtime.Services.PersistentProgress;
using Code.Runtime.Services.SaveLoad;
using InstantGamesBridge;
using InstantGamesBridge.Modules.Game;
using UnityEngine;
using VContainer.Unity;
using Application = UnityEngine.Device.Application;

namespace Code.Runtime.Modules.WebApplicationModule
{
    public class WebAppController : IInitializable, IDisposable
    {
        private readonly AudioService _audioService;
        private readonly PauseService _pauseService;
        private readonly SaveLoadService _saveLoad;
        private readonly IGameDataContainer _gameData;

        private const string YANDEX_LEADERBOARD_NAME = "LevelLeaderboard";

        private readonly Dictionary<string, object> _leaderBoard = new();
        public WebAppController(AudioService audioService, PauseService pauseService, SaveLoadService saveLoad, IGameDataContainer gameData)
        {
            _audioService = audioService;
            _pauseService = pauseService;
            _saveLoad = saveLoad;
            _gameData = gameData;
        }

        public void Initialize()
        {
            if (WebApplication.IsWebApp)
            {
                Application.targetFrameRate = 60;
                Bridge.game.visibilityStateChanged += HandleVisibilityLogic;
            }

            if (WebApplication.IsWebApp && IsLeaderboardSupported)
            {
                _saveLoad.DataSaved += WriteLeaderboard;
            }
        }

        private void WriteLeaderboard()
        {
            _leaderBoard.Clear();

            var heroLevel = _gameData.PlayerData.PlayerExp.Level;
            
            switch (Bridge.platform.id)
            {
                case "yandex":
                    _leaderBoard.Add("Level", heroLevel);
                    _leaderBoard.Add("LevelLeaderboard", YANDEX_LEADERBOARD_NAME);
                    break;
            }
            
            Bridge.leaderboard.SetScore(_leaderBoard, LeaderBoardWriteOperation);
        }

        private void LeaderBoardWriteOperation(bool result)
        {
            if (result)
            {
                Debug.Log("LeaderboardWriteSuccessful");
            }
            else
            {
                Debug.LogError("Leaderboard write error");
            }
        }

        private void HandleVisibilityLogic(VisibilityState state)
        {
            switch (state)
            {
                case VisibilityState.Hidden:
                    _pauseService.PauseGame();
                    _audioService.MuteAll(true);
                    _saveLoad.SaveData();
                    break;
                case VisibilityState.Visible:
                    _pauseService.UnpauseGame();
                    _audioService.MuteMusic(!_gameData.AudioData.isMusicOn);
                    _audioService.MuteSounds(!_gameData.AudioData.isSoundOn);
                    break;
            }
        }
        

        private bool IsLeaderboardSupported => Bridge.leaderboard.isSupported;

        public void Dispose()
        {
            _saveLoad.DataSaved -= WriteLeaderboard;
            Bridge.game.visibilityStateChanged -= HandleVisibilityLogic;
        }
    }
}
