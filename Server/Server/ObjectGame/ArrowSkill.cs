using Server.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{

    [Serializable]
    public class ArrowSkill
    {
        public string sprite;
        public Vector3 locationShoot;
        public float length;
        public int idOwner; 
        public string idSkill;
        public float speed;
        /// <summary>
        /// up:12
        /// down:0
        /// left:4
        /// right:8
        /// </summary>
        public int face;
        public Vector3 poss;
        public ArrowSkill(Vector3 position, float length, int idOwner, string idSkill, float speed, int face )
        {
            this.locationShoot = position;
            this.poss = position;
            this.length = length;
            this.idOwner = idOwner;
            this.idSkill = idSkill;
            this.speed = speed;
            this.face = face;
        }


        public static bool LengthAlive(Vector3 start, float length, int face, Vector3 poss)
        {
            Vector3 vector3 = poss;

            if (face == 12)//up
            {
                if ((start.y + length) >= vector3.y) // hết range skill
                    return true;
                else
                    return false;
            }
            else if (face == 0)
            {
                if ((start.y - length) <= vector3.y)
                    return true;
                else
                    return false;
            }
            else if (face == 4)
            {
                if ((start.x - length) <= vector3.x)
                    return true;
                else
                    return false;
            }
            else
            {
                if ((start.x + length) >= vector3.x)
                    return true;
                else
                    return false;
            }

        }
       

        public static void ArrowUpdatePosition(ArrowSkill skill, int map)
        {
            Vector3 vector3 = skill.poss;
            if (!LengthAlive(skill.locationShoot, skill.length, skill.face, vector3))
            {
                //TODO sendpackge destroy id
                ServerSend.SendBackDataArrowToClient(skill.idOwner, skill.idSkill, skill.poss, false, map);
                Map.skillsInMap[map].Remove(skill);
                return;
            }
            if (skill.face == 12)
            {
                vector3.y += skill.speed;
            }
            else if (skill.face == 0)
            {
                vector3.y -= skill.speed;
            }
            else if (skill.face == 4)
            {
                vector3.x -= skill.speed;
            }
            else
            {
                vector3.x += skill.speed;
            }
            skill.poss = vector3;
            ServerSend.SendBackDataArrowToClient(skill.idOwner, skill.idSkill, skill.poss, true, map);
        }

    }
}
