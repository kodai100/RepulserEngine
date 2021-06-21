using System;
using ProjectBlue.RepulserEngine.Domain.DataModel;
using ProjectBlue.RepulserEngine.Domain.Entity;
using ProjectBlue.RepulserEngine.Domain.UseCase;
using ProjectBlue.RepulserEngine.Domain.ViewModel;
using ProjectBlue.RepulserEngine.UseCaseInterfaces;
using UniRx;

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
            foreach (var mapping in midiSettingUseCase.GetCurrent())
            {
                if (data.Number != mapping.MidiNumber) continue;

                switch (mapping.MidiSendType)
                {
                    case MidiSendType.Bypass:
                        SendThrough(data, mapping);
                        break;
                    case MidiSendType.OnOnly:
                        SendOnOnly(data, mapping);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                mapping.prevValue = data.Value;
            }
        }

        private void SendOnOnly(MidiData data, MidiMappingSettingViewModel mapping)
        {
            if (mapping.prevValue > 127 / 2f) return;
            if (data.Value <= 127 / 2f) return;

            var endPoints = endPointSettingUseCase.GetCurrent();

            foreach (var endPoint in endPoints)
            {
                if (!endPoint.ConnectionEnabled) continue;

                sendToEndpointUseCase.Send(endPoint.EndPoint, mapping.OscAddressConversion,
                    data.Value.ToString(),
                    CommandType.Osc);
            }
        }

        private void SendThrough(MidiData data, MidiMappingSettingViewModel mapping)
        {
            var endPoints = endPointSettingUseCase.GetCurrent();

            foreach (var endPoint in endPoints)
            {
                if (!endPoint.ConnectionEnabled) continue;

                sendToEndpointUseCase.Send(endPoint.EndPoint, mapping.OscAddressConversion,
                    data.Value.ToString(),
                    CommandType.Osc);
            }
        }

        public void Dispose()
        {
            disposable.Dispose();
        }
    }
}