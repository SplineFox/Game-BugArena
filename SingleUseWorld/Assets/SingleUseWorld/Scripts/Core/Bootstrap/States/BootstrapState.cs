using System;

namespace SingleUseWorld
{
    public class BootstrapState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly DIContainer _diContainer;
        private readonly SceneLoader _sceneLoader;

        public BootstrapState(GameStateMachine stateMachine, DIContainer diContainer, SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _diContainer = diContainer;
            _sceneLoader = sceneLoader;

            RegisterServices();
        }

        public void Enter()
        {
            _sceneLoader.Load(SceneName.Bootstrap, EnterLoadScoreState);
        }

        public void Exit()
        {
        }

        private void EnterLoadScoreState()
        {
            _stateMachine.Enter<LoadScoreState>();
        }

        private void RegisterServices()
        {
            _diContainer.Register<IPlayerInput>(new PlayerInput());
            _diContainer.Register<IPrefabProvider>(new PrefabProvider());
            _diContainer.Register<IConfigProvider>(new ConfigProvider());

            _diContainer.Register<IEffectFactory>(new EffectFactory(_diContainer.Resolve<IPrefabProvider>()));
            _diContainer.Register<IEffectAppearanceFactory>(new EffectAppearanceFactory());

            _diContainer.Register<IEffectSpawner>(new EffectSpawner(
                _diContainer.Resolve<IEffectFactory>(),
                _diContainer.Resolve<IEffectAppearanceFactory>()
                ));

            RegisterEntityFactories();
            RegisterItemFactories();
        }

        private void RegisterEntityFactories()
        {
            _diContainer.Register<BombEntityFactory>(new BombEntityFactory(
                _diContainer.Resolve<IPrefabProvider>(),
                _diContainer.Resolve<IConfigProvider>(),
                _diContainer.Resolve<IEffectSpawner>(),
                _diContainer.Resolve<Score>(),
                _diContainer.Resolve<HitTimer>(),
                _diContainer.Resolve<CameraShaker>()
                ));

            _diContainer.Register<ArrowEntityFactory>(new ArrowEntityFactory(
                _diContainer.Resolve<IPrefabProvider>(),
                _diContainer.Resolve<IConfigProvider>(),
                _diContainer.Resolve<Score>(),
                _diContainer.Resolve<HitTimer>(),
                _diContainer.Resolve<CameraShaker>()
                ));

            _diContainer.Register<SkullEntityFactory>(new SkullEntityFactory(
                _diContainer.Resolve<IPrefabProvider>(),
                _diContainer.Resolve<IConfigProvider>(),
                _diContainer.Resolve<Score>(),
                _diContainer.Resolve<HitTimer>(),
                _diContainer.Resolve<CameraShaker>()
                ));

            _diContainer.Register<SwordEntityFactory>(new SwordEntityFactory(
                _diContainer.Resolve<IPrefabProvider>(),
                _diContainer.Resolve<IConfigProvider>(),
                _diContainer.Resolve<Score>(),
                _diContainer.Resolve<HitTimer>(),
                _diContainer.Resolve<CameraShaker>()
                ));
        }

        private void RegisterItemFactories()
        {
            _diContainer.Register<BombItemFactory>(new BombItemFactory(
                _diContainer.Resolve<IPrefabProvider>(),
                _diContainer.Resolve<IConfigProvider>(),
                _diContainer.Resolve<BombEntityFactory>()
                ));

            _diContainer.Register<BowItemFactory>(new BowItemFactory(
                _diContainer.Resolve<IPrefabProvider>(),
                _diContainer.Resolve<IConfigProvider>(),
                _diContainer.Resolve<ArrowEntityFactory>()
                ));

            _diContainer.Register<SkullItemFactory>(new SkullItemFactory(
                _diContainer.Resolve<IPrefabProvider>(),
                _diContainer.Resolve<IConfigProvider>(),
                _diContainer.Resolve<SkullEntityFactory>()
                ));

            _diContainer.Register<SwordItemFactory>(new SwordItemFactory(
                _diContainer.Resolve<IPrefabProvider>(),
                _diContainer.Resolve<IConfigProvider>(),
                _diContainer.Resolve<SwordEntityFactory>()
                ));
        }
    }
}