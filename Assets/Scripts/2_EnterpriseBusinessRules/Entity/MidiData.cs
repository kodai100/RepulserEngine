namespace ProjectBlue.RepulserEngine.Domain.Entity
{
    public enum MidiType
    {
        Note,
        CC
    }

    public class MidiData
    {
        public MidiType midiType;
        public int Channel;
        public int Number;
        public int Value; // CC: controlValue, Note: noteOn or off

        public float ConvertValueToFloat()
        {
            return Value / 127f;
        }
    }
}