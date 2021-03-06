﻿using System;
using System.Collections.Generic;
using System.Xml.Schema;

namespace GPCEmulator.BUSDevices.MainComponents
{
    public class Memory : BusDevice
    {
        public List<byte> Mem;
        
        public UInt16 lastChanged = 0;
        public Memory(ushort l, ushort u, Bus b) : base(l, u, b)
        {
            lower = l;
            upper = u;
            bus = b;
            Mem = new List<byte>();
           for (int i = 0;i <= u-l;i++)
            {
             Mem.Add(0x00); 
          
            }
        }
        public void OnMemoryChanged(UInt16 i)
        {
            lastChanged = i;
        
        }
        public new void Tick()
        {
            if (bus.readAddress() >= lower && bus.readAddress() <= upper)
            {
                if (bus.getWriteFlag() == true)
                {
                    Mem[bus.readAddress()] = bus.readData();
                    OnMemoryChanged(bus.readAddress());
                }
                else
                {
                    bus.writeData(Mem[bus.readAddress()]);
                   
                   
                }
            }
        }
    }
}