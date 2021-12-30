using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

public class SetGlobalVariable : Action
{
    public override void OnAwake()
    {
        GlobalVariables.Instance.SetVariableValue("FacingRight", -1);
    }
    public override TaskStatus OnUpdate()
    {
        return base.OnUpdate();
    }
}
