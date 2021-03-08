using System;
using System.Collections.Generic;
using ProjectBlue.RepulserEngine.Domain.Model;

namespace ProjectBlue.RepulserEngine.Presentation
{
    public interface IEndPointListPresenter
    {
        void Save(IEnumerable<EndpointSetting> settingList);
        IEnumerable<EndpointSetting> Load();
    }
}