using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class GotoNextState : ActionBoss
{
    public SharedInt currentState;
    public override void OnStart()
    {
        currentState.Value ++;
    }
    public override TaskStatus OnUpdate()
    {
        return base.OnUpdate();
    }
}
