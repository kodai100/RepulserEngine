using System;

namespace ProjectBlue.RepulserEngine.Repository
{
    public interface IKeyboardInputRepository
    {
        IObservable<string> OnInputAsObservable { get; }
    }
}