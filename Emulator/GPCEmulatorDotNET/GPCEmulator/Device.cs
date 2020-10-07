using System.Collections.Generic;
using GPCEmulator.BUSDevices;
using GPCEmulator.BUSDevices.MainComponents;

namespace GPCEmulator
{
    public class Device
    {
        public Memory RAM;
        public CPU Cpu;
        public Bus Bus;
        public List<BusDevice> Devices;
        public Device()
        {
            Devices = new List<BusDevice>();
            Bus=new Bus();
            Cpu=new CPU(0,0xFFFF, Bus);
            RAM = new Memory(0,0xFFFF,Bus);
            Devices.Add(Cpu);
            Devices.Add(RAM);
        }

        public void Tick()
        {
          Cpu.Tick();
          RAM.Tick();
        }
    }
}