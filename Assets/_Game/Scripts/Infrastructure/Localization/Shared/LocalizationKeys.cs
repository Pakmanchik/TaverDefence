namespace Localization.Shared
{
    public static class LocalizationKeys
    {
        // Menu
        public const string OnlineGame = "@online_game";
        public const string UpgradeScreen = "@upgrade_screen";
        public const string OfflineGame = "@offline_game";
        public const string HelloWorld = "@hello_world";
        public const string MainMenu = "@main_menu";
        public const string EndlessDescription = "@endless_description";
        public const string SandboxDescription = "@sandbox_description";
        public const string SplitscreenDescription = "@splitscreen_description";
        public const string StartGameButton = "@start_game_button";
        public const string LoadMainMenu = "@load_main_menu";
        public const string GameVariantStandard = "@variant_game_standard";

        public const string DescriptionStandardGameVariant = "@description_standard_game_variant";

        // Game
        public const string LoadGame = "@load_game";
        public const string YouWin = "@game_info_win";
        public const string YouLose = "@game_info_lose";
        public const string YouLeave = "@game_info_leave";
        public const string CoinCollected = "@game_info_collected_coin";
        public const string CoinCollectedResources = "@game_info_collected_coin_resources";
        public const string CoinCollectedEnemy = "@game_info_collected_coin_enemy";
        public const string EnemyKilled = "@game_info_collected_enemy";
        public const string EnemyKilledEasy = "@game_info_collected_enemy_easy";
        public const string EnemyKilledAcid = "@game_info_collected_enemy_acid";
        public const string OpenOptionMenu = "@game_info_open_option_menu";
        public const string OpenInventory = "@game_info_open_inventory";
        public const string ContinueGame = "@continue_button";
        public const string TotalCoins = "@game_info_total_coins";

        public const string LevelsCompleted = "@game_info_levels_completed";

        // For Items
        public const string AmmoAmmunition = "@ammo_ammunition";
        public const string BombsAmmunition = "@bombs_ammunition";
        public const string BoostsAmmunition = "@boosts_ammunition";
        public const string Bombs = "@bombs";
        public const string Guns = "@guns";

        public const string Boosts = "@boosts";

        // Bombs name
        public const string BombHorizontal = "@bomb_name_horizontal";
        public const string BombVertical = "@bomb_name_vertical";
        public const string BombCross = "@bomb_name_cross";
        public const string BombNuclear = "@bomb_name_nuclear";

        public const string BombSplash = "@bomb_name_splash";

        // Guns name
        public const string GunPistol = "@guns_name_pistol";
        public const string GunRifle = "@guns_name_rifle";
        public const string GunSubMachine = "@guns_name_submachine";

        public const string GunShotgun = "@guns_name_shotgun";

        // Boosts name
        public const string BoostsArmor = "@boosts_name_armor";

        // Shared
        public const string SettingsControl = "@settings_control";
        public const string SettingsSound = "@settings_sound";
        public const string SettingsScreen = "@settings_screen";
        public const string LanguageOption = "@language_option";
        public const string LanguageNow = "@languageNow";
        public const string Shop = "@shop";
        public const string Settings = "@settings";
        public const string ExitButton = "@exit_button";
        public const string Buy = "@buy";
        public const string Sell = "@sell";
        public const string LoadResources = "@load_resources";
        public const string InMainMenuButton = "@in_main_menu_button";
        public const string BackButton = "@back_button";
        public const string TotalVolumeSettings = "@total_volume_option";
        public const string BackgroundVolumeSettings = "@background_volume_option";

        public const string GameVolumeSettings = "@game_volume_option";

        // UpgradesKeys
        public const string WithoutUpgrades = "@upgrades_without_upgrades";
        public const string Level = "@upgrades_level";
        public const string HeadHealthPlayer = "@upgrades_head_health_player";
        public const string HealthLevel1 = "@upgrades_health_player_level1";
        public const string HealthLevel2 = "@upgrades_health_player_level2";
        public const string HealthLevel3 = "@upgrades_health_player_level3";
        public const string HealthLevel4 = "@upgrades_health_player_level4";
        public const string HealthLevel5 = "@upgrades_health_player_level5";

        // Upgrades
        public const string UpgradeButton = "@upgrade_button";
        public const string HealthHead = "@upgrade_health_head";
        public const string HealthInfoWithoutLevel = "@upgrade_health_info_without_level";
        public const string HealthInfoLevel1 = "@upgrade_health_info_level_1";

        public const string Void = "@void";

        // То что ниже для простоты дебага и всего такого
        public static string ToFriendlyString(GameStrings gameString)
        {
            switch (gameString)
            {
                case GameStrings.OnlineGame:                     return OnlineGame;
                case GameStrings.OfflineGame:                    return OfflineGame;
                case GameStrings.Settings:                       return Settings;
                case GameStrings.ExitButton:                     return ExitButton;
                case GameStrings.HelloWorld:                     return HelloWorld;
                case GameStrings.MainMenu:                       return MainMenu;
                case GameStrings.EndlessDescription:             return EndlessDescription;
                case GameStrings.SandboxDescription:             return SandboxDescription;
                case GameStrings.SplitscreenDescription:         return SplitscreenDescription;
                case GameStrings.InMainMenuButton:               return InMainMenuButton;
                case GameStrings.StartGameButton:                return StartGameButton;
                case GameStrings.SettingsControl:                return SettingsControl;
                case GameStrings.SettingsSound:                  return SettingsSound;
                case GameStrings.SettingsScreen:                 return SettingsScreen;
                case GameStrings.LanguageNow:                    return LanguageNow;
                case GameStrings.Shop:                           return Shop;
                case GameStrings.Bombs:                          return Bombs;
                case GameStrings.Guns:                           return Guns;
                case GameStrings.Boosts:                         return Boosts;
                case GameStrings.Buy:                            return Buy;
                case GameStrings.Sell:                           return Sell;
                case GameStrings.AmmoAmmunition:                 return AmmoAmmunition;
                case GameStrings.BombsAmmunition:                return BombsAmmunition;
                case GameStrings.BoostsAmmunition:               return BoostsAmmunition;
                case GameStrings.LanguageOption:                 return LanguageOption;
                case GameStrings.Void:                           return Void;
                case GameStrings.LoadGame:                       return LoadGame;
                case GameStrings.LoadResources:                  return LoadResources;
                case GameStrings.LoadMainMnu:                    return LoadMainMenu;
                case GameStrings.BombHorizontal:                 return BombHorizontal;
                case GameStrings.BombVertical:                   return BombVertical;
                case GameStrings.BombCross:                      return BombCross;
                case GameStrings.BombNuclear:                    return BombNuclear;
                case GameStrings.BombSplash:                     return BombSplash;
                case GameStrings.YouWin:                         return YouWin;
                case GameStrings.YouLose:                        return YouLose;
                case GameStrings.CoinCollected:                  return CoinCollected;
                case GameStrings.EnemyKilled:                    return EnemyKilled;
                case GameStrings.BackButton:                     return BackButton;
                case GameStrings.StandardGame:                   return GameVariantStandard;
                case GameStrings.OpenOptionMenu:                 return OpenOptionMenu;
                case GameStrings.OpenInventory:                  return OpenInventory;
                case GameStrings.ContinueGame:                   return ContinueGame;
                case GameStrings.TotalVolumeSettings:            return TotalVolumeSettings;
                case GameStrings.BackgroundVolumeSettings:       return BackgroundVolumeSettings;
                case GameStrings.GameVolumeSettings:             return GameVolumeSettings;
                case GameStrings.DescriptionStandardGameVariant: return DescriptionStandardGameVariant;
                case GameStrings.YouLeave:                       return YouLeave;
                case GameStrings.EnemyKilledEasy:                return EnemyKilledEasy;
                case GameStrings.EnemyKilledAcid:                return EnemyKilledAcid;
                case GameStrings.CoinCollectedResources:         return CoinCollectedResources;
                case GameStrings.CoinCollectedEnemy:             return CoinCollectedEnemy;
                case GameStrings.TotalCoins:                     return TotalCoins;
                case GameStrings.GunPistol:                      return GunPistol;
                case GameStrings.GunRifle:                       return GunRifle;
                case GameStrings.GunSubMachine:                  return GunSubMachine;
                case GameStrings.GunShotgun:                     return GunShotgun;
                case GameStrings.BoostsArmor:                    return BoostsArmor;
                case GameStrings.LevelsCompleted:                return LevelsCompleted;
                case GameStrings.UpgradeScreen:                  return UpgradeScreen;
                case GameStrings.UpgradeButton:                  return UpgradeButton;
                default:                                         return Void;
            }
        }

        public enum GameStrings
        {
            Void,
            OnlineGame,
            OfflineGame,
            Settings,
            ExitButton,
            HelloWorld,
            MainMenu,
            EndlessDescription,
            SandboxDescription,
            SplitscreenDescription,
            InMainMenuButton,
            BackButton,
            StartGameButton,
            SettingsControl,
            SettingsSound,
            SettingsScreen,
            LanguageNow,
            Shop,
            Bombs,
            Guns,
            Boosts,
            Buy,
            Sell,
            AmmoAmmunition,
            BombsAmmunition,
            BoostsAmmunition,
            LanguageOption,
            LoadResources,
            LoadMainMnu,
            LoadGame,
            BombHorizontal,
            YouWin,
            YouLose,
            CoinCollected,
            EnemyKilled,
            BombVertical,
            BombCross,
            BombNuclear,
            BombSplash,
            StandardGame,
            OpenOptionMenu,
            OpenInventory,
            ContinueGame,
            TotalVolumeSettings,
            BackgroundVolumeSettings,
            GameVolumeSettings,
            DescriptionStandardGameVariant,
            YouLeave,
            CoinCollectedEnemy,
            CoinCollectedResources,
            EnemyKilledAcid,
            EnemyKilledEasy,
            TotalCoins,
            GunPistol,
            GunRifle,
            GunSubMachine,
            GunShotgun,
            BoostsArmor,
            LevelsCompleted,
            UpgradeScreen,
            UpgradeButton,
        }

        public static string ToFriendlyString(KeyTipsLocalizationScreen key)
        {
            switch (key)
            {
                default: return Void;
            }
        }

        public static string ToFriendlyString(UpgradesKeys key)
        {
            switch (key)
            {
                case UpgradesKeys.WithoutUpgrades:  return WithoutUpgrades;
                case UpgradesKeys.Level:            return Level;
                case UpgradesKeys.HeadHealthPlayer: return HeadHealthPlayer;
                case UpgradesKeys.HealthLevel1:     return HealthLevel1;
                case UpgradesKeys.HealthLevel2:     return HealthLevel2;
                case UpgradesKeys.HealthLevel3:     return HealthLevel3;
                case UpgradesKeys.HealthLevel4:     return HealthLevel4;
                case UpgradesKeys.HealthLevel5:     return HealthLevel5;
                default:                            return Void;
            }
        }

        public enum KeyTipsLocalizationScreen
        {
            Tip1,
        }

        public enum UpgradesKeys
        {
            Empty,
            WithoutUpgrades,
            Level,
            HeadHealthPlayer,
            HealthLevel1,
            HealthLevel2,
            HealthLevel3,
            HealthLevel4,
            HealthLevel5,
        }
    }
}