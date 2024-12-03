namespace Kiadorn.ScriptableEvents
{
    public interface IScriptableEventListener
    {
        void OnEventRaised(ScriptableEvent eventRaised);
    }
}