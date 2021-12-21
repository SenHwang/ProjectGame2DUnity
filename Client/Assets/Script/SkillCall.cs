using Assets.Script.Types;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct SkillUseCall
{
    public InventorySkill.SkillInventory skill;
    public GameObject castAnim;
    public bool anim1;

    public GameObject spriteSkill;
    public GameObject spriteGetHit;    
    
    public bool anim2;
    public bool anim3;
}

public class SkillCall : MonoBehaviour
{
    public static List<SkillUseCall> skillCall = new List<SkillUseCall>();


    public static void CallSkill(InventorySkill.SkillInventory skill)
    {
        //GameObject spriteSkill = AnimationManager.CallAnimation(new Vector3(0, 0, 0), int.Parse(skill.item.spriteSkill));

        //GameObject spriteGetHit = AnimationManager.CallAnimation(new Vector3(2, 0, 0), int.Parse(skill.item.spriteGetHit));
        GameObject localPlayer = GameManager.players[Client.instance.myId].gameObject;
        Vector3 playerPoss = localPlayer.transform.position;
        playerPoss.y -= .5f;
        GameObject castAnim = null;

        if (skill.item.castAnim != "None")
        {
            castAnim = AnimationManager.CallAnimation(playerPoss, int.Parse(skill.item.castAnim), true);
            castAnim.gameObject.transform.SetParent(localPlayer.transform);

            AnimationCalledStruct anim = new AnimationCalledStruct();
            anim.index = int.Parse(skill.item.castAnim);
            anim.DestroyOnDone = true;
            anim.animationType = AnimationType.none;
            anim.idPlayer = Client.instance.myId;
            ClientSend.PlayerCallAnimation(playerPoss, anim);

        }
        //castAnim = AnimationManager.CallAnimationByTime(playerPoss, int.Parse(skill.item.castAnim),2f);

        SkillUseCall skillpush = new SkillUseCall();
        skillpush.skill = skill;
        skillpush.castAnim = castAnim;
        skillpush.anim1 = true;

        skillpush.spriteSkill = null;
        skillpush.spriteGetHit = null;      
        
        skillCall.Add(skillpush);
    }

    public static void Update()
    {
        int countSkill = skillCall.Count;
        if (countSkill == 0) return;
        try
        {
            for (int i = 0; i < countSkill; i++)
            {
                if (skillCall[i].anim1 == true && skillCall[i].castAnim != null)
                    continue;
                else
                {
                    if (skillCall[i].anim3 == true && skillCall[i].spriteGetHit == null && skillCall[i].spriteSkill == null)
                    {
                        skillCall.RemoveAt(i);
                        continue;
                    }
                    else if (skillCall[i].anim3 == false)
                    {
                        SkillUseCall skillChange = new SkillUseCall();
                        skillChange = skillCall[i];
                        skillChange.anim1 = false;

                        skillChange.anim2 = true;
                        skillChange.anim3 = true;

                        GameObject spriteGetHit = null;
                        GameObject spriteSkill = null;

                        if (skillChange.skill.item.targetAble)
                        {
                            if(TargetBar.idTarget == -1)
                                ChatInput.instance.UpdateChatBar(1, "System: Target not found!");
                            else if (TargetBar.idTarget != -1 && 
                                TargetBar.idTarget != Client.instance.myId && 
                                GameManager.players[TargetBar.idTarget].mapID == GAME.MAP_START)
                            {
                                Player playerGet = GameManager.players[TargetBar.idTarget];
                                if (skillChange.skill.item.range < Vector2.Distance(playerGet.gameObject.transform.position, GameManager.players[Client.instance.myId].gameObject.transform.position))
                                {
                                    ChatInput.instance.UpdateChatBar(1, "System: Target too far!");
                                }
                                else
                                {
                                    //call dmg and anim
                                    if (skillChange.skill.item.spriteGetHit != "None")
                                    {
                                        spriteGetHit = AnimationManager.CallAnimation(playerGet.position, int.Parse(skillChange.skill.item.spriteGetHit), true);

                                        AnimationCalledStruct anim = new AnimationCalledStruct();
                                        anim.index = int.Parse(skillChange.skill.item.spriteGetHit);
                                        anim.DestroyOnDone = true;
                                        anim.animationType = AnimationType.none;
                                        anim.idPlayer = -1;
                                        ClientSend.PlayerCallAnimation(playerGet.position, anim);
                                    }
                                    if (skillChange.skill.item.spriteSkill != "None")
                                    {
                                        spriteSkill = AnimationManager.CallAnimation(playerGet.position, int.Parse(skillChange.skill.item.spriteSkill), true);
                                        
                                        AnimationCalledStruct anim = new AnimationCalledStruct();
                                        anim.index = int.Parse(skillChange.skill.item.spriteSkill);
                                        anim.DestroyOnDone = true;
                                        anim.animationType = AnimationType.none;
                                        anim.idPlayer = -1;
                                        ClientSend.PlayerCallAnimation(playerGet.position, anim);
                                    }

                                    if (Party.isMemberTeam(Client.instance.myId, TargetBar.idTarget))
                                        return;

                                    ClientSend.SkillGetHit( Client.instance.myId, "", TargetBar.idTarget, GAME.MAP_START);

                                }
                            }                          
                        }
                        

                        skillChange.spriteGetHit = spriteGetHit;
                        skillChange.spriteSkill = spriteSkill;
                        skillCall[i] = skillChange;
                    }




                }

            }
        }
        catch
        {

        }
        //if (skillCall[0].spriteSkill == null && skillCall[0].spriteGetHit == null && skillCall[0].castAnim == null)
        //    skillCall.RemoveAt(0);
    }
}
