using System;

namespace BugArena
{
    public class BootstrapState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly DIContainer _diContainer;
        private readonly SceneLoader _sceneLoader;
        private readonly SceneCurtain _sceneCurtain;

        public BootstrapState(GameStateMachine stateMachine, DIContainer diContainer, SceneLoader sceneLoader, SceneCurtain sceneCurtain)
        {
            _stateMachine = stateMachine;
            _diContainer = diContainer;
            _sceneLoader = sceneLoader;
            _sceneCurtain = sceneCurtain;

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
            RegisterBaseServices();
            RegisterDataServices();
            RegisterMenuServices();
            RegisterEffectServices();

            RegisterPlayerFactory();
            RegisterEntityFactories();
            RegisterItemFactories();
            RegisterEnemyFactory();
        }

        private void RegisterBaseServices()
        {
            _diContainer.Register<IPrefabProvider>(new PrefabProvider());
            _diContainer.Register<IConfigProvider>(new ConfigProvider());
            _diContainer.Register<IInputService>(new InputService());

            _diContainer.Register<IPauseService>(new PauseService());
            _diContainer.Register<IHitTimer>(new HitTimer(_diContainer.Resolve<ICoroutineRunner>()));

            _diContainer.Register<IArenaAccessService>(new ArenaAccessService());
        }

        private void RegisterMenuServices()
        {
            _diContainer.Register<IMenuFactory>(new MenuFactory(
                _stateMachine,
                _diContainer.Resolve<IPrefabProvider>(),
                _diContainer.Resolve<IInputService>(),
                _diContainer.Resolve<IPauseService>(),
                _diContainer.Resolve<IScoreAccessService>()));
            _diContainer.Register<IMenuService>(new MenuService(_diContainer.Resolve<IMenuFactory>()));
        }

        private void RegisterDataServices()
        {
            _diContainer.Register<IScoreAccessService>(new ScoreAccessService());
            _diContainer.Register<ISaveLoadService>(new SaveLoadService(_diContainer.Resolve<IScoreAccessService>()));
        }

        private void RegisterEffectServices()
        {
            _diContainer.Register<IEffectFactory>(new EffectFactory(_diContainer.Resolve<IPrefabProvider>()));
            _diContainer.Register<IEffectAppearanceFactory>(new EffectAppearanceFactory());

            _diContainer.Register<IEffectSpawner>(new EffectSpawner(
                _diContainer.Resolve<IEffectFactory>(),
                _diContainer.Resolve<IEffectAppearanceFactory>()
                ));

            _diContainer.Register<IVisualFeedback>(new VisualFeedback());
        }

        private void RegisterEntityFactories()
        {
            _diContainer.Register<BombEntityFactory>(new BombEntityFactory(
                _diContainer.Resolve<IPrefabProvider>(),
                _diContainer.Resolve<IConfigProvider>(),
                _diContainer.Resolve<IEffectSpawner>(),
                _diContainer.Resolve<IScoreAccessService>(),
                _diContainer.Resolve<IVisualFeedback>()
                ));

            _diContainer.Register<ArrowEntityFactory>(new ArrowEntityFactory(
                _diContainer.Resolve<IPrefabProvider>(),
                _diContainer.Resolve<IConfigProvider>(),
                _diContainer.Resolve<IScoreAccessService>(),
                _diContainer.Resolve<IVisualFeedback>()
                ));

            _diContainer.Register<RockEntityFactory>(new RockEntityFactory(
                _diContainer.Resolve<IPrefabProvider>(),
                _diContainer.Resolve<IConfigProvider>(),
                _diContainer.Resolve<IScoreAccessService>(),
                _diContainer.Resolve<IVisualFeedback>()
                ));

            _diContainer.Register<SwordEntityFactory>(new SwordEntityFactory(
                _diContainer.Resolve<IPrefabProvider>(),
                _diContainer.Resolve<IConfigProvider>(),
                _diContainer.Resolve<IScoreAccessService>(),
                _diContainer.Resolve<IVisualFeedback>()
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

            _diContainer.Register<RockItemFactory>(new RockItemFactory(
                _diContainer.Resolve<IPrefabProvider>(),
                _diContainer.Resolve<IConfigProvider>(),
                _diContainer.Resolve<RockEntityFactory>()
                ));

            _diContainer.Register<SwordItemFactory>(new SwordItemFactory(
                _diContainer.Resolve<IPrefabProvider>(),
                _diContainer.Resolve<IConfigProvider>(),
                _diContainer.Resolve<SwordEntityFactory>()
                ));
        }

        private void RegisterPlayerFactory()
        {
            _diContainer.Register<PlayerFactory>(new PlayerFactory(
                _diContainer.Resolve<IPrefabProvider>(),
                _diContainer.Resolve<IConfigProvider>(),
                _diContainer.Resolve<IEffectSpawner>()
                ));
        }

        private void RegisterEnemyFactory()
        {
            _diContainer.Register<EnemyFactory>(new EnemyFactory(
            _diContainer.Resolve<IPrefabProvider>(),
            _diContainer.Resolve<IConfigProvider>(),
            _diContainer.Resolve<IEffectSpawner>()
            ));
        }
    }
}