# TirTest2D

Прототип 2D-тира на Unity 2020.3: стрельба из пушки по движущимся целям (птицам), полный игровой цикл от загрузки до экрана результатов.

## Геймплей

- Пушка с прицеливанием и стрельбой (`Cannon`, `PlayerShooting`), типы снарядов: ядро и бомба (`Cannonball`, `Bomb`) с фабриками-спавнерами.
- Враги (птицы) со спавном по случайным точкам и движением по кривым (Path Creator), 2D-анимация.
- Баланс уровней, параметры снарядов и врагов через ScriptableObjects (`LevelBalanceSO`, `BulletSettingsSO`, `EnemySettingsSO`).

## Архитектура

- Двухуровневая state machine: уровень приложения (Loading → Gameplay) и уровень геймплея (Loading → Preview → Play → Result), общая реализация в `Common/StateMachine`.
- DI на Zenject: инсталлеры на каждый модуль (application, gameplay, player, bullets, enemies, UI).
- Экраны UI через контроллер экранов (`ScreenController`), сцены через `SceneLoader`.
- Разделение слоёв: `Application` / `Gameplay` / `Services` / `Common`.

## Запуск

1. Открыть проект в Unity 2020.3.x.
2. Открыть стартовую сцену и запустить Play Mode.

## Стек

Unity 2020.3, C#, Zenject, DOTween, Path Creator, Unity Input System (generated controls).
