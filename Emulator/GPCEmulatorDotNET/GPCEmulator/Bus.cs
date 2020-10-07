using System;

namespace GPCEmulator
{
    public class Bus
    {
        private byte Data;
        private UInt16 Address;
        private bool writeFlag;
        public Bus()
        {
            Data = 0x0;
            Address = 0x0;
            writeFlag = false;
        }

        public void writeData(byte data)
        {
            Data = data;
        }

        public void writeAddress(UInt16 address)
        {
            Address = address;
        }

        public byte readData()
        {
            return Data;
        }

        public UInt16 readAddress()
        {
            return Address;
        }

        public void setWriteFlag(bool flag)
        {
            writeFlag = flag;
        }

        public bool getWriteFlag()
        {
            return writeFlag;
        }
    }
}