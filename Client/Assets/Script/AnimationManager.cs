using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct AnimationCalledStruct
{
    public int index; //này là id của thằng anim
    public int idPlayer;
    public bool DestroyOnDone;
    public float timeDestroy;
    public float rangeDestroy;
    public float speedObject;
    public AnimationType animationType;
}

/// <summary>
/// get list animation và lưu trong GameData để đễ gọi khi bắt đầu game,
/// dạng như load game khi mở game
/// </summary>
public class AnimationManager: MonoBehaviour
{
    /// <summary>
    /// load list animation rồi lưu vào animation
    /// </summary>
    public static void LoadAnimList()
    {
        //load list animations rồi get name, count
        //set animation = new String[count];
        //set list vào trong animation
        GAME.animation = Resources.LoadAll<Texture2D>(GAME.ANIMATION_PATH);
    }

    /// <summary>
    /// poss: vị trí animation
    /// index: index animation trong list animation
    /// destroy sau khi chạy hết anim
    /// </summary>
    /// <param name="poss"></param>
    /// <param name="index"></param>
    public static GameObject CallAnimation(Vector3 poss, int index,bool DestroyOnDone)
    {
        GameObject animation = Instantiate(GAME.objectAnimation, poss, new Quaternion()) as GameObject;
        animation.GetComponent<AnimationFunc>().index = index;
        animation.GetComponent<AnimationFunc>().isDestroy = DestroyOnDone;
        animation.GetComponent<AnimationFunc>().type = AnimationType.none;
        return animation;
    }

    /// <summary>
    /// poss: vị trí animation
    /// index: index animation trong list animation
    /// destroy sau khi chạy hết time
    /// </summary>
    /// <param name="poss"></param>
    /// <param name="index"></param>
    /// <param name="timeDestroy"></param>
    /// <returns></returns>
    public static GameObject CallAnimationByTime(Vector3 poss, int index, float timeDestroy)
    {
        GameObject animation = Instantiate(GAME.objectAnimation, poss, new Quaternion()) as GameObject;
        animation.GetComponent<AnimationFunc>().index = index;
        animation.GetComponent<AnimationFunc>().timeDestroy = timeDestroy;
        animation.GetComponent<AnimationFunc>().type = AnimationType.byTime;
        return animation;
    }

    /// <summary>
    /// poss: vị trí animation
    /// index: index animation trong list animation
    /// destroy sau khi chạy ra khỏi vị trí ban đầu một range
    /// </summary>
    /// <param name="poss"></param>
    /// <param name="index"></param>
    /// <param name="rangeDestroy"></param>
    /// <returns></returns>
    public static GameObject CallAnimationByRange(Vector3 poss, int index, float rangeDestroy,float speedObject)
    {
        GameObject animation = Instantiate(GAME.objectAnimation, poss, new Quaternion()) as GameObject;
        animation.GetComponent<AnimationFunc>().index = index;
        animation.GetComponent<AnimationFunc>().rangeDestroy = rangeDestroy;
        animation.GetComponent<AnimationFunc>().speedMove = speedObject;
        animation.GetComponent<AnimationFunc>().type = AnimationType.byRange;
        return animation;
    }

    public static GameObject CallAnimation(Vector3 poss, AnimationCalledStruct anim)
    {
        if (anim.animationType == AnimationType.none)
            return CallAnimation(poss, anim.index, anim.DestroyOnDone);
        else if (anim.animationType == AnimationType.byTime)
            return CallAnimationByTime(poss, anim.index, anim.timeDestroy);
        else if (anim.animationType == AnimationType.byRange)
            return CallAnimationByRange(poss, anim.index, anim.rangeDestroy, anim.speedObject);
        else return null;
    }
}
