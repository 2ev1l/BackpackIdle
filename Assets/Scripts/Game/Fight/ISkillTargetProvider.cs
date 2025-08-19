using Game.DataBase;
using UnityEngine;

namespace Game.Fight
{
    public interface ISkillTargetProvider
    {
        public GameObject Activator { get; }
        public GameObject FindEnemy(ItemInfo forItem);
    }
}