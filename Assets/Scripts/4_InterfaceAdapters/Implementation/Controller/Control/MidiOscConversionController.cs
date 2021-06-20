using System;
using ProjectBlue.RepulserEngine.Domain.DataModel;
using ProjectBlue.RepulserEngine.Domain.Entity;
using ProjectBlue.RepulserEngine.Domain.UseCase;
using ProjectBlue.RepulserEngine.UseCaseInterfaces;
using UniRx;
using UnityEngine;

namespace ProjectBlue.RepulserEngine.Controllers
{
    public class MidiOscConversionController : IDisposable
    {
        private IEndPointSettingUseCase endPointSettingUseCase;
        private ISendToEndpointUseCase sendToEndpointUseCase;

        private IMidiMappingSettingUseCase midiSettingUseCase;

        private CompositeDisposable disposable = new CompositeDisposable();

        public MidiOscConversionController(
            ISendToEndpointUseCase sendToEndpointUseCase,
            IEndPointSettingUseCase endPointSettingUseCase,
            IMidiCommunicationUseCase midiCommunicationUseCase,
            IMidiMappingSettingUseCase midiSettingUseCase)
        {
            this.endPointSettingUseCase = endPointSettingUseCase;
            this.sendToEndpointUseCase = sendToEndpointUseCase;

            this.midiSettingUseCase = midiSettingUseCase;

            midiCommunicationUseCase.OnMidiAsObservable.Subscribe(OnMidiData).AddTo(disposable);
        }

        private void OnMidiData(MidiData data)
        {
            var endPoints = endPointSettingUseCase.GetCurrent();

            foreach (var mapping in midiSettingUseCase.GetCurrent())
            {
                if (data.Number == mapping.MidiNumber)
                {
                    if (mapping.MidiType == MidiType.Note)
                    {
                        if (mapping.prevValue < 127 / 2f)
                        {
                            if (data.Value >= 127 / 2f)
                            {
                                foreach (var endPoint in endPoints)
                                {
                                    if (!endPoint.ConnectionEnabled) continue;

                                    var ipEndPoint = endPoint.EndPoint;

                                    sendToEndpointUseCase.Send(ipEndPoint, mapping.OscAddressConversion,
                                        data.Value.ToString(),
                                        CommandType.Osc);
                                }
                            }
                        }
                    }

                    mapping.prevValue = data.Value;
                }
            }
        }

        public void Dispose()
        {
            disposable.Dispose();
        }
    }
}