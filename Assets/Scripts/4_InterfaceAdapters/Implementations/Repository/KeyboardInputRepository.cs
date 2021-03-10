using System;
using ProjectBlue.RepulserEngine.DataStore;

namespace ProjectBlue.RepulserEngine.Repository
{
    public class KeyboardInputRepository : IKeyboardInputRepository
    {

        private IKeyboardInputDataStore keyboardInputDataStore;

        public IObservable<string> OnInputAsObservable => keyboardInputDataStore.OnInputAsObservable;

        public KeyboardInputRepository(IKeyboardInputDataStore keyboardInputDataStore)
        {
            this.keyboardInputDataStore = keyboardInputDataStore;
        }
        
    }
}