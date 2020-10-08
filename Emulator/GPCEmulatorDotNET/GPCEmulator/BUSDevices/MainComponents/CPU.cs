using System;
using System.ComponentModel.Design;
using System.Security.Permissions;

namespace GPCEmulator.BUSDevices.MainComponents
{
    public class CPU:BusDevice
    {
        private byte ARegister, BRegister, Accumulator, StepCounter, InstructionRegister = 0x0;
        private UInt16 MemoryPointer, XRegister, StackPointer = 0x0;
        private bool CarryFlag, ZeroFlag, EQFlag = false;

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
                case (byte)InstructionSet.NOP: //NOP
                    resetCycle();
                    break;
                
                
                case (byte)InstructionSet.LDA1:  //LDA
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
                            resetCycle();
                            break;
                        default:
                            resetCycle();
                            throw new Exception("Invalid Step for OP Code: " + InstructionRegister.ToString("X"));
                            break;
                    }
                    break;
                
                
                case (byte)InstructionSet.STA:  //STA
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
                            resetCycle();
                            break;
                        default:
                            resetCycle();
                            throw new Exception("Invalid Step for OP Code: " + InstructionRegister.ToString("X"));
                            break;
                    }
                    break;

                
                case (byte)InstructionSet.LDB1:  //LDB
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
                            resetCycle();
                            break;
                        default:
                            resetCycle();
                            throw new Exception("Invalid Step for OP Code: " + InstructionRegister.ToString("X"));
                            break;
                    }
                    break;
                
                
                case (byte)InstructionSet.STB:  //STB
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
                            resetCycle();
                            break;
                        default:
                            resetCycle();
                            throw new Exception("Invalid Step for OP Code: " + InstructionRegister.ToString("X"));
                            break;
                    }
                    break;
                
                
                case (byte)InstructionSet.ADD1: //ADD
                    ARegister = Accumulator;
                    resetCycle();
                    break;
                    
                    
                case (byte)InstructionSet.ADD2:  //ADD
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
                            resetCycle();
                            break;
                        default:
                            resetCycle();
                            throw new Exception("Invalid Step for OP Code: " + InstructionRegister.ToString("X"));
                            break;
                    }
                    break;
                
                
                case (byte)InstructionSet.MOV: //MOV
                    switch (StepCounter)
                    {
                        case 3:
                            bus.setWriteFlag(true);
                            bus.writeAddress(0xFFFF);
                            bus.writeData(ARegister);
                            break;
                        case 4:
                            MemoryPointer++;
                            bus.setWriteFlag(false);
                            bus.writeAddress(MemoryPointer);
                            break;
                        case 5:
                            XRegister = bus.readData();
                            XRegister = (UInt16)(XRegister << 8);
                            break;
                        case 6:
                            MemoryPointer++;
                            bus.setWriteFlag(false);
                            bus.writeAddress(MemoryPointer);
                            break;
                        case 7:
                            XRegister += bus.readData();
                            break;
                        case 8:
                            ARegister = bus.readData();
                            break;
                        case 9:
                            MemoryPointer++;
                            bus.setWriteFlag(false);
                            bus.writeAddress(MemoryPointer);
                            break;
                        case 10:
                            XRegister = bus.readData();
                            XRegister = (UInt16)(XRegister << 8);
                            break;
                        case 11:
                            MemoryPointer++;
                            bus.setWriteFlag(false);
                            bus.writeAddress(MemoryPointer);
                            break;
                        case 12:
                            XRegister += bus.readData();
                            break;
                        case 13:
                            bus.setWriteFlag(true);
                            bus.writeAddress(XRegister);
                            bus.writeData(ARegister);
                            break;
                        case 14:
                            bus.setWriteFlag(false);
                            bus.writeAddress(0xFFFF);
                            break;
                        case 15:
                            ARegister = bus.readData();
                            resetCycle();
                            break;
                        default:
                            resetCycle();
                            throw new Exception("Invalid Step for OP Code: " + InstructionRegister.ToString("X"));
                            break;
                    }
                    
                    break;
                    case (byte)InstructionSet.JMP: //JMP
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
                                MemoryPointer = XRegister;
                                StepCounter = 0;
                                break;


                            default:
                                resetCycle();
                                throw new Exception("Invalid Step for OP Code: " + InstructionRegister.ToString("X"));
                                break;
                                MemoryPointer++;
                        }
                        break;
                
                
                default:
                    StepCounter = 0;
                    throw new Exception("OP Code not defined");
                    break;
            }
        }

        void resetCycle()
        {
            StepCounter = 0;
            MemoryPointer++;
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

            
              
            
        }
    }
}