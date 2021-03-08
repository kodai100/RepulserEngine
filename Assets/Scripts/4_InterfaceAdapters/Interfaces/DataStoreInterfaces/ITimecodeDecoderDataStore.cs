using System;
using Ltc;
using ProjectBlue.RepulserEngine.Domain.Model;

namespace ProjectBlue.RepulserEngine.DataStore
{
    public interface ITimecodeDecoderDataStore
    {
        IObservable<TimecodeData> OnTimecodeUpdatedAsObserbable { get; }
    }
}