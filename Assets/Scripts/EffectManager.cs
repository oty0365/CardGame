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
    Predation = 1<<6,
    DrainLife = 1<<7,
    Frighten = 1<<8,
    MindControl = 1<<9,
    Move = 1<<10,
    Execution = 1<<11,
    DoubleAttack = 1<<12,
    AttackAll = 1<<13,
    WingsOfRuin = 1<<14,
    AtomicBom = 1<<15,
    QuickBody = 1<<16,
    PayBack = 1<<17
}
[System.Flags]
public enum MagicEffects
{
    None = 0,
    FireBall=1<<0,
    ThunderBolt=1<<1,
    IceAge=1<<2,
    DoubleDraw= 1<<3,
    ChangeFaith = 1<<4,
    EyesOfDeepSea = 1<<5,
    BulkUp = 1<<6,
    HealthPotion = 1<<7,
    PoisionousPotion = 1<<8,
    DualBladed = 1<<9,
    TheErosion = 1<<10,
    TheBarrior = 1<<11,
    TheFall = 1<<12

}
[System.Flags]
public enum PassiveEffects
{
    None = 0,
    ChargeStamina=1<<0,
    BloodyDay= 1<<1
}

//일단 이 아래는 나중에 이펙트 다룰때 처리할거 아직 신경 안써도 됨
public class EffectManager : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
