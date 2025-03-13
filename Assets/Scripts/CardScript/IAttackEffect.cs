namespace CardScript
{
    public interface IAttackEffect : ICardEffectScript
    {
        int Damage { get; }
    }
}