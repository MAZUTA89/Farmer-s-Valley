
namespace Scripts.InteractableObjects
{
    public interface IInteractableObjectFactory<out T, in D> 
    {
        T Create(D createData);
    }
    
}
