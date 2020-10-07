using System;
using System.ComponentModel.Design;
using System.Security.Permissions;

namespace GPCEmulator.BUSDevices.MainComponents
{
    public class CPU:BusDevice
    {
        private byte ARegister, BRegister, Accumulator, StepCounter, InstructionRegister = 0x0;
        private UInt16 MemoryPointer, XRegister = 0x0;
        private bool CarryFlag, ZeroFlag;

        public UInt16 getMemoryPointer()
        {
            return MemoryPointer;
        }
        public CPU(ushort l, ushort u, Bus b) : base(l, u, b)
        {
            bus = b;
            lower = l;
            upper = u;
        }

        private void Fetch()
        {
            if (StepCounter == 1)
            {
              bus.setWriteFlag(false);
              bus.writeAddress(MemoryPointer);
            }
            else
            {
             
                InstructionRegister = bus.readData();
            }
        }

        private void Execute()
        {
            switch (InstructionRegister)
            {
                case 0x00: //NOP
                    StepCounter = 0;
                    break;
                
                
                case 0x01:  //LDA
                    switch (StepCounter)
                    {
                        case 3:
                            MemoryPointer++;
                            bus.setWriteFlag(false);
                            bus.writeAddress(MemoryPointer);
                            break;
                        case 4:
                            XRegister = bus.readData();
                            XRegister = (UInt16)(XRegister << 8);
                            break;
                        case 5:
                            MemoryPointer++;
                            bus.setWriteFlag(false);
                            bus.writeAddress(MemoryPointer);
                            break;
                        case 6:
                            XRegister += bus.readData();
                            break;
                        case 7:
                            bus.setWriteFlag(false);
                            bus.writeAddress(XRegister);
                            break;
                        case 8:
                            ARegister = bus.readData();
                            StepCounter = 0;
                            break;
                        default:
                            StepCounter = 0;
                            throw new Exception("Invalid Step for OP Code: " + InstructionRegister.ToString("X"));
                            break;
                    }
                    break;
                
                
                case 0x02:  //STA
                    switch (StepCounter)
                    {
                        case 3:
                            MemoryPointer++;
                            bus.setWriteFlag(false);
                            bus.writeAddress(MemoryPointer);
                            break;
                        case 4:
                            XRegister = bus.readData();
                            XRegister = (UInt16)(XRegister << 8);
                            break;
                        case 5:
                            MemoryPointer++;
                            bus.setWriteFlag(false);
                            bus.writeAddress(MemoryPointer);
                            break;
                        case 6:
                            XRegister += bus.readData();
                            break;
                        case 7:
                            bus.setWriteFlag(true);
                            bus.writeAddress(XRegister);
                            bus.writeData(ARegister);
                            StepCounter = 0;
                            break;
                        default:
                            StepCounter = 0;
                            throw new Exception("Invalid Step for OP Code: " + InstructionRegister.ToString("X"));
                            break;
                    }
                    break;

                
                case 0x03:  //LDB
                    switch (StepCounter)
                    {
                        case 3:
                            MemoryPointer++;
                            bus.setWriteFlag(false);
                            bus.writeAddress(MemoryPointer);
                            break;
                        case 4:
                            XRegister = bus.readData();
                            XRegister = (UInt16)(XRegister << 8);
                            break;
                        case 5:
                            MemoryPointer++;
                            bus.setWriteFlag(false);
                            bus.writeAddress(MemoryPointer);
                            break;
                        case 6:
                            XRegister += bus.readData();
                            break;
                        case 7:
                            bus.setWriteFlag(false);
                            bus.writeAddress(XRegister);
                            break;
                        case 8:
                            BRegister = bus.readData();
                            StepCounter = 0;
                            break;
                        default:
                            StepCounter = 0;
                            throw new Exception("Invalid Step for OP Code: " + InstructionRegister.ToString("X"));
                            break;
                    }
                    break;
                
                
                case 0x04:  //STB
                    switch (StepCounter)
                    {
                        case 3:
                            MemoryPointer++;
                            bus.setWriteFlag(false);
                            bus.writeAddress(MemoryPointer);
                            break;
                        case 4:
                            XRegister = bus.readData();
                            XRegister = (UInt16)(XRegister << 8);
                            break;
                        case 5:
                            MemoryPointer++;
                            bus.setWriteFlag(false);
                            bus.writeAddress(MemoryPointer);
                            break;
                        case 6:
                            XRegister += bus.readData();
                            break;
                        case 7:
                            bus.setWriteFlag(true);
                            bus.writeAddress(XRegister);
                            bus.writeData(BRegister);
                            StepCounter = 0;
                            break;
                        default:
                            StepCounter = 0;
                            throw new Exception("Invalid Step for OP Code: " + InstructionRegister.ToString("X"));
                            break;
                    }
                    break;
                
                
                case 0x05: //ADD
                    ARegister = Accumulator;
                    StepCounter = 0;
                    break;
                    
                    
                case 0x06:  //STB
                    switch (StepCounter)
                    {
                        case 3:
                            MemoryPointer++;
                            bus.setWriteFlag(false);
                            bus.writeAddress(MemoryPointer);
                            break;
                        case 4:
                            XRegister = bus.readData();
                            XRegister = (UInt16)(XRegister << 8);
                            break;
                        case 5:
                            MemoryPointer++;
                            bus.setWriteFlag(false);
                            bus.writeAddress(MemoryPointer);
                            break;
                        case 6:
                            XRegister += bus.readData();
                            break;
                        case 7:
                            bus.setWriteFlag(true);
                            bus.writeAddress(XRegister);
                            bus.writeData(Accumulator);
                            StepCounter = 0;
                            break;
                        default:
                            StepCounter = 0;
                            throw new Exception("Invalid Step for OP Code: " + InstructionRegister.ToString("X"));
                            break;
                    }
                    break;
                
                
                default:
                    StepCounter = 0;
                    throw new Exception("OP Code not defined");
                    break;
            }
        }
        
        
        
         public new void Tick()
        {
            
            //Run Accumulator
            if (ARegister + BRegister > 0xFF)
            {
                CarryFlag = true;
                Accumulator = (byte)((int)ARegister + (int)BRegister - 0xFF);
            }
            else
            {
                CarryFlag = false;
                Accumulator = (byte)(ARegister + BRegister);
            }
            
            
            
            StepCounter++;
            if (StepCounter < 3)
            {
                Fetch();
            }
            else
            {
                Execute();
            }

            if (StepCounter == 0)
            {
                MemoryPointer++;
            }
        }
    }
}