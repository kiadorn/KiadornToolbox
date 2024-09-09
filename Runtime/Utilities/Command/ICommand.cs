namespace Kiadorn.Utilities
{
    public interface ICommand
    {
        void Execute();
        void Undo();
    }
}
