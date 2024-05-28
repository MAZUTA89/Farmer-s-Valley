
namespace Scripts.StateMachine
{
    public interface IUniversalState
    {
        void Enter();
        void Exit();
        void Perform();
    }
}
