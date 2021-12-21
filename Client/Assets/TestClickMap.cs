using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static Map;

public class TestClickMap : MonoBehaviour
{
    // Start is called before the first frame update

    private bool isClicked = false;
    private float times = 0;

    public static GameObject EditMap;
    public static bool blockEnable = false;
    public static bool lineEnable = false;
    public static bool clickVectorEnable = false;
    public static bool npcSetEnable = false;

    public static LengthMap lengthMap;

    public static int y = 10;//up
    public static int y1 = -9;//down

    public static int x = 17;//right
    public static int x1 = -16;//left    

    public static List<NPCInMap> npcInMap;
    public static List<MapBlock> blockList;    

    private void FixedUpdate()
    {
        if (lineEnable)
        {
            if (lengthMap.left == 0 && lengthMap.right == 0 && lengthMap.up == 0 && lengthMap.down == 0) return;
            for (int i = (int) lengthMap.left; i < (int)lengthMap.right; i++)
            {
                for (int z = (int)lengthMap.down; z < (int)lengthMap.up ; z++)
                {
                    Debug.DrawLine(new Vector3(i, z, 0), new Vector3(i, z + 1, 0), Color.white, .1f);
                    Debug.DrawLine(new Vector3(i, z, 0), new Vector3(i + 1, z, 0), Color.white, .1f);
                }
            }
            foreach (MapBlock mb in blockList)
            {
                if (mb.blockType == BlockType.Box)
                {
                    OnDrawBox(mb.location);
                }
                else if (mb.blockType == BlockType.Circle)
                {
                    OnDrawCircle(mb.location);
                }
            }
        }

        if (isClicked)
        {
            if (times == 10)
            {
                times = 0;
                isClicked = false;
                return;
            }
            times++;
            return;
        }

        if (blockEnable)
        {           
            if (Input.GetKey(KeyCode.Mouse0))
            {
                isClicked = true;                
                MapUpdateUI.textScreen.GetComponent<TextDisplay>().map = GAME.MAP_START;
                Vector2 poss = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 vector2Int = new Vector2();
                vector2Int.x = (int)poss.x;
                vector2Int.y = (int)poss.y;

                if (poss.x < 0)
                {
                    vector2Int.x -= 0.5f;
                    if (vector2Int.x < x1) return;
                }
                else
                {
                    vector2Int.x += 0.5f;
                    if (vector2Int.x > x) return;
                }
                   
                if (poss.y < 0)
                {
                    vector2Int.y -= 0.5f;
                    if (vector2Int.y < y1) return;
                }
                else
                {
                    vector2Int.y += 0.5f;
                    if (vector2Int.y > y) return;
                }                   

                OnSetBlock(vector2Int);
            }
        }

        if (clickVectorEnable)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                isClicked = true;
                MapUpdateUI.textScreen.GetComponent<TextDisplay>().map = GAME.MAP_START;
                Vector2 poss = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 vector2Int = new Vector2();
                vector2Int.x = (int)poss.x;
                vector2Int.y = (int)poss.y;

                if (poss.x < 0)
                    vector2Int.x -= 0.5f;
                else
                    vector2Int.x += 0.5f;

                if (poss.y < 0)
                    vector2Int.y -= 0.5f;
                else
                    vector2Int.y += 0.5f;

                MapUpdateUI.textScreen.GetComponent<Text>().text = $"{vector2Int.ToString()}";
                GameObject textScreen1 = Instantiate(MapUpdateUI.textScreen, (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition), new Quaternion());
            }
        }

        if (npcSetEnable)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                isClicked = true;
                Vector2 poss = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 vector2Int = new Vector2();
                vector2Int.x = (int)poss.x;
                vector2Int.y = (int)poss.y;

                if (poss.x < 0)
                {
                    vector2Int.x -= 0.5f;
                    if (vector2Int.x < x1) return;
                }
                else
                {
                    vector2Int.x += 0.5f;
                    if (vector2Int.x > x) return;
                }

                if (poss.y < 0)
                {
                    vector2Int.y -= 0.5f;
                    if (vector2Int.y < y1) return;
                }
                else
                {
                    vector2Int.y += 0.5f;
                    if (vector2Int.y > y) return;
                }

                OnSetNPC(vector2Int);
            }
        }

        if (Input.GetKey(KeyCode.Mouse1))
        {
            isClicked = true;
            Camera.main.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }


    }

    private void OnSetBlock(Vector2 vt)
    {
        MapBlock mb = blockList.SingleOrDefault(x => x.location == vt);
        
        if(mb.blockType == 0)
        {
            mb.blockType = BlockType.Box;
            mb.location = vt;
            blockList.Add(mb);
        }else if(mb.blockType == BlockType.Box)
        {
            blockList.Remove(mb);
            mb.blockType = BlockType.Circle;
            blockList.Add(mb);
        }
        else
        {
            blockList.Remove(mb);
        }
        
    }

    private void OnDrawBox(Vector2 vt)
    {
        int Xmin = (int)(vt.x - 0.5f);
        int Xmax = (int)(vt.x + 0.5f);

        int Ymin = (int)(vt.y - 0.5f);
        int Ymax = (int)(vt.y + 0.5f);


        Debug.DrawLine(new Vector3(Xmin, Ymin, 0), new Vector3(Xmin, Ymax, 0), Color.red, .1f);

        Debug.DrawLine(new Vector3(Xmin, Ymax, 0), new Vector3(Xmax, Ymax, 0), Color.red, .1f);

        Debug.DrawLine(new Vector3(Xmax, Ymax, 0), new Vector3(Xmax, Ymin, 0), Color.red, .1f);

        Debug.DrawLine(new Vector3(Xmax, Ymin, 0), new Vector3(Xmin, Ymin, 0), Color.red, .1f);

    }

    private void OnDrawCircle(Vector2 vt)
    {
        int Xmin = (int)(vt.x - 0.5f);
        int Xmax = (int)(vt.x + 0.5f);

        int Ymin = (int)(vt.y - 0.5f);
        int Ymax = (int)(vt.y + 0.5f);


        Debug.DrawLine(new Vector3(Xmin, Ymin, 0), new Vector3(Xmin, Ymax, 0), Color.blue, .1f);

        Debug.DrawLine(new Vector3(Xmin, Ymax, 0), new Vector3(Xmax, Ymax, 0), Color.blue, .1f);

        Debug.DrawLine(new Vector3(Xmax, Ymax, 0), new Vector3(Xmax, Ymin, 0), Color.blue, .1f);

        Debug.DrawLine(new Vector3(Xmax, Ymin, 0), new Vector3(Xmin, Ymin, 0), Color.blue, .1f);
    }

    private void OnSetNPC(Vector2 vector2Int)
    {
        NPCInMap npcGet = Map.maps[GAME.MAP_START].npcInMaps.SingleOrDefault(x => x.pos == vector2Int);
        if (npcGet.pos != vector2Int)
        {
            //đây là chưa có thì add thằng mới vào
            NPCInMap newNPC = new NPCInMap();
            newNPC.pos = vector2Int;
            newNPC.dir = 0;
            newNPC.npcID = EditMapFunc.indexNPC;
            Map.maps[GAME.MAP_START].npcInMaps.Add(newNPC);
        }
        else
        {
            NPCInMap newNPC = npcGet;
            Map.maps[GAME.MAP_START].npcInMaps.Remove(npcGet);
            newNPC.dir++;
            if (newNPC.dir > 3)
                return;
            Map.maps[GAME.MAP_START].npcInMaps.Add(newNPC);
            // còn đây là có thằng npc ở vị trí này
            // nên tiếp tục bấm sẽ chuyển dir
            // nếu dir >3 thì xóa thằng npc này
        }
    }


}
