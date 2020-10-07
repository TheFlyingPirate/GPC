using System;

namespace GPCEmulator.BUSDevices
{
    public class BusDevice
    {
        public UInt16 lower;
        public UInt16 upper;
        public Bus bus;
        
        public BusDevice(UInt16 l, UInt16 u, Bus b)
        {
            lower = l;
            upper = u;
            bus = b;
        }

        public void Tick()
        {
            
        }
    }
}