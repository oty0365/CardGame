
using UnityEngine;


//아직 캐스터랑 타겟들의 클래스가 없어서 GameObject로 임시로 해둔것, 바꿀 예정
namespace CardScript
{
    public interface ICardEffectScript 
    {
        public void Effect(GameObject caster, GameObject target);
    }
}