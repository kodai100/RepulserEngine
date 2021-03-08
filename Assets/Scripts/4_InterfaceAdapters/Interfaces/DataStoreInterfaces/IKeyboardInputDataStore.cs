using System;

namespace ProjectBlue.RepulserEngine.DataStore
{
    public interface IKeyboardInputDataStore
    {
        IObservable<string> OnInputAsObservable { get; }
    }
}