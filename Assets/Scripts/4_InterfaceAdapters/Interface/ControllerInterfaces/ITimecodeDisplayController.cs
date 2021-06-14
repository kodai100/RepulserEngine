using System;
using ProjectBlue.RepulserEngine.Domain.DataModel;
using ProjectBlue.RepulserEngine.Domain.Entity;

namespace ProjectBlue.RepulserEngine.Controllers
{
    public interface ITimecodeDisplayController
    {
        IObservable<TimecodeData> OnUpdateTimecodeAsObservable { get; }
    }
}