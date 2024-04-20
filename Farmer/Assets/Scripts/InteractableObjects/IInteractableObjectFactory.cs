
namespace Scripts.InteractableObjects
{
    public interface IInteractableObjectFactory<out T, in D>
        where T : IInteractable
    {
        T Create(D createData);
    }
}
