using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class SetThreshlod : ActionBoss
{
    public SharedInt healthThreshold;

    public override void OnStart()
    {
        healthThreshold.Value -= 10;
    }
    public override TaskStatus OnUpdate()
    {
        return TaskStatus.Success;
    }
}
