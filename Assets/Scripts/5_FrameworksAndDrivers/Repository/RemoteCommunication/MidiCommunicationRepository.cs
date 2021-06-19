using System;
using System.Collections.Generic;
using ProjectBlue.RepulserEngine.Domain.Entity;
using RtMidi.LowLevel;
using Zenject;
using UniRx;
using UnityEngine;

namespace ProjectBlue.RepulserEngine.Repository
{
    public class MidiCommunicationRepository : IMidiCommunicationRepository, ITickable, IDisposable
    {
        public IObservable<MidiData> OnMidiAsObservable => onMidiSubject;

        private Subject<MidiData> onMidiSubject = new Subject<MidiData>();


        MidiProbe probe;
        List<MidiInPort> ports = new List<MidiInPort>();

        public MidiCommunicationRepository()
        {
            probe = new MidiProbe(MidiProbe.Mode.In);
        }

        public void Tick()
        {
            // Rescan when the number of ports changed.
            if (ports.Count != probe.PortCount)
            {
                DisposePorts();
                ScanPorts();
            }

            // Process queued messages in the opened ports.
            foreach (var p in ports) p?.ProcessMessages();
        }

        private void ScanPorts()
        {
            for (var i = 0; i < probe.PortCount; i++)
            {
                var name = probe.GetPortName(i);
                Debug.Log("MIDI-in port found: " + name);

                ports.Add(IsRealPort(name)
                    ? new MidiInPort(i)
                    {
                        OnNoteOn = (byte channel, byte note, byte velocity) =>
                        {
                            onMidiSubject.OnNext(
                                new MidiData {midiType = MidiType.Note, Number = note, Value = 127});
                        },

                        OnNoteOff = (byte channel, byte note) =>
                        {
                            onMidiSubject.OnNext(new MidiData {midiType = MidiType.Note, Number = note, Value = 0});
                        },

                        OnControlChange = (byte channel, byte number, byte value) =>
                        {
                            onMidiSubject.OnNext(
                                new MidiData {midiType = MidiType.CC, Number = number, Value = value});
                        },
                    }
                    : null
                );
            }
        }

        private bool IsRealPort(string name)
        {
            return !name.Contains("Through") && !name.Contains("RtMidi");
        }

        private void DisposePorts()
        {
            foreach (var p in ports) p?.Dispose();
            ports.Clear();
        }

        public void Dispose()
        {
            probe?.Dispose();
            DisposePorts();

            onMidiSubject.Dispose();
        }
    }

    public class MidiCommunicationRepositoryInstaller : Installer<MidiCommunicationRepositoryInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<MidiCommunicationRepository>().AsSingle();
        }
    }
}