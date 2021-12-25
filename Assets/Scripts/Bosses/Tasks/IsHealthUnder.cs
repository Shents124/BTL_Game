using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class IsHealthUnder : BossConditional
{
    public SharedInt healthThreshold;

    public override TaskStatus OnUpdate()
    {
        return boss_Combat.GetCurrentHealth() <= healthThreshold.Value ? TaskStatus.Success : TaskStatus.Failure;
    }
}
