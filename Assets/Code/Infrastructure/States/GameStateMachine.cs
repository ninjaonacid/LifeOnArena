using System;
using System.Collections.Generic;
using Code.Infrastructure.Services;
using Code.Logic;
using Code.Services;
using Code.Services.PersistentProgress;
using Code.Services.SaveLoad;
using Code.UI.Services;
using UnityEngine;

namespace Code.Infrastructure.States
{
    public class GameStateMachine : IGameStateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;

        public GameStateMachine(SceneLoader sceneLoader, LoadingCurtain curtain,
            AllServices services)
        {
            _states = new Dictionary<Type, IExitableState>
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, services),
                [typeof(LoadProgressState)] = new LoadProgressState(this,
                    services.Single<IProgressService>(),
                    services.Single<ISaveLoadService>()),
                [typeof(MainMenuState)] = new MainMenuState(this, sceneLoader, services, curtain),
                [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader,
                    curtain, services, services.Single<IUIFactory>()),
                [typeof(GameLoopState)] = new GameLoopState(this, sceneLoader, services.Single<IGameEventHandler>()),
            };
        }

        public void Enter<TState>() where TState : class, IState
        {
            IState state = ChangeState<TState>();
            state.Enter();
            Debug.Log(state.ToString());
        }


        public void Enter<TState, TPayload>(TPayload payload) where TState :
            class, IPayloadedState<TPayload>
        {
            var state = ChangeState<TState>();
            state.Enter(payload);
        }

        private TState GetState<TState>() where TState : class, IExitableState
        {
            return _states[typeof(TState)] as TState;
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();
            var state = GetState<TState>();
            _activeState = state;
            return state;
        }
    }
}