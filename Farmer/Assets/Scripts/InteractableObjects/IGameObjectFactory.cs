
namespace Scripts.InteractableObjects
{
    public interface IGameObjectFactory<out T, in D> 
    {
        T Create(D createData);
    }
}
