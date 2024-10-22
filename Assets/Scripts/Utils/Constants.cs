using UnityEngine;

namespace Utils {
    public static class Constants {
        public const string MAIN_MENU_SCENE = "Main Menu";
        public const string TUTORIAL_SCENE = "Tutorial";
        public const string CLASSIC_SCENE = "Classic";
        public const string SHAPES_SCENE = "Shapes";
        public const float MIN_SPAWN_INTERVAL = 1f;
        public const float OBSTACLE_VELOCITY = 5f;
        public const float OBSTACLE_ACCELERATION = 1.02f;
        public const float TOP_OBSTACLE_VELOCITY = 15f;
        public static readonly string SaveDataFilePath = Application.persistentDataPath + "/savefile.json";
    }
}