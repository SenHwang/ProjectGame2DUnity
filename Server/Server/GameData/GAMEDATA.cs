using Server.Data;
using Server.GameData;
using Server.ObjectGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    static class GAMEDATA
    {
        public const float MoveSpeed = 10f /Constants.TICK_PER_SEC;

        #region PATH APPLICATION
        public static string APPLICATION_PATH;
        #endregion

        #region MAP
        public const int MAP_MAX = 3;
        public const string MAP_PATH = "./Data/Map/";
        public static MapStruct[] maps = new MapStruct[GAMEDATA.MAP_MAX];
        public static string[] mapsString = new string[MAP_MAX];
        public const int MAP_SPAWN_ON_DEAD = 0;
        #endregion

        #region ACCOUNT
        public const string ACCOUNT_TYPE = ".acc";
        public const string ACCOUNT_PATH = "\\Data\\Account\\";
        #endregion

        #region TileSet
        public const string TILESETS_PATH = "./Data/Tilesets/";
        #endregion

        #region Party
        public const int MAX_PARTY_MEMBERS = 4;
        public const int MAX_PARTYS = 50;
        public static PartyRec[] Partys = new PartyRec[GAMEDATA.MAX_PARTYS];
        #endregion

        #region NPC
        public const int MAX_NPC = 100;
        public static NPC[] npcs = new NPC[MAX_NPC];
        public const string NPC_SOURCE_PATH = "./Data/NPC/image/";
        public const string NPC_DATA_PATH = "./Data/NPC/data/";
        #endregion

        #region Skill
        public const int MAX_SKILL = 100;
        public static SkillRec[] skills = new SkillRec[MAX_SKILL];
        public const string ANIMATION_SOURCE_PATH = "./Data/Animation/";
        public const string SKILL_ICON_SOURCE_PATH = "./Data/Skill/Icons/";
        public const string SKILL_DATA_PATH = "./Data/Skill/Data/";
        #endregion

        #region Item
        public const int MAX_ITEM = 100;
        public static ItemRec[] items = new ItemRec[MAX_ITEM];
        public const string ITEM_ICON_SOURCE_PATH = "./Data/Item/Icons/";
        public const string ITEM_DATA_PATH = "./Data/Item/Data/";
        #endregion

        #region Inventory
        public static int MAX_INVENTORY = 49;
        #endregion

    }
}
