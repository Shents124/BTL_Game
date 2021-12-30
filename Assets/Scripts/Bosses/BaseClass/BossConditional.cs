using BehaviorDesigner.Runtime.Tasks;

public class BossConditional : Conditional
{
    protected Boss_Combat boss_Combat;

    public override void OnAwake()
    {
        boss_Combat = GetComponent<Boss_Combat>();
    }
}
