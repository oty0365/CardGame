using UnityEngine;

[System.Flags]
public enum MonsterEffects
{
    None = 0,
    Roar = 1<<0,
    SpineBody = 1<<1,
    Tocxic = 1<<2,
    Flames =1<<3,
    Imortality = 1<<4,
    Grow = 1<<5,
    Predation = 1<<6
}
[System.Flags]
public enum MagicEffects
{
    None = 0,
    FireBall=1<<0,
    ThunderBolt=1<<1,
    IceAge=1<<2
}
[System.Flags]
public enum PassiveEffects
{
    None = 0,
    ChargeStamina=1<<0,
    BloodyDay= 1<<1
}

//�ϴ� �� �Ʒ��� ���߿� ����Ʈ �ٷ궧 ó���Ұ� ���� �Ű� �Ƚᵵ ��
public class EffectManager : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
