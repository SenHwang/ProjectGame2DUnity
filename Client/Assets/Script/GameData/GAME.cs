using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GAME
{

    #region PATH EQUIPMENT
    public const string BODY_PATH = "Characters/Body";
    public const string EMOJI_PATH = "Characters/Emoji";
    public const string HAIR_PATH = "Characters/Hair";
    public const string HAT_PATH = "Characters/Hat";
    public const string PANTS_PATH = "Characters/Pants";
    public const string SHOE_PATH = "Characters/Shoe";
    public const string CLOTHER_PATH = "Characters/Clother";
    public const string WEAPON_PATH = "Characters/Weapon";
    #endregion

    //PATH NPC Sprite
    public const string NPC_PATH = "NPC/Sprites";

    #region MAP SET
    public const int MAX_TITLE = 32;
    public static int MAX_MAP;
    public const string MAP_PATH = "Map";
    public const string MAP_DATA_PATH = "MapData";
    public static int MAP_START = 0;
    #endregion

    #region PLAYER
    public const float SPEED = 7f;
    #endregion

    #region NPC
    public static int MAX_NPC = 10;
    public static NPC[] npcs = new NPC[MAX_NPC];
    #endregion

    #region Item
    public static int MAX_ITEM = 100;
    public static ItemRec[] items = new ItemRec[MAX_ITEM];
    #endregion

    #region Inventory
    public static int MAX_INVENTORY = 49;
    public const string ICON_ITEM_PATH = "Items/ItemIcons/";
    public const string ICON_ITEM_SKILL_PATH = "Skill/SkillIcon/";
    #endregion

    #region Skill
    public static int MAX_SKILL = 100;
    public static SkillRec[] skills = new SkillRec[MAX_SKILL];
    #endregion

    #region Animation
    public const string ANIMATION_PATH = "Animations/";
    public static Texture2D[] animation;
    public static GameObject objectAnimation;
    #endregion

    #region Audio path
    public const string MUSIC_PATH = "Audio/Music/";
    public const string SRC_PATH = "Audio/Src/";
    #endregion


}
