using System;
using System.Collections;
using System.ComponentModel.Design;
using System.Security.Permissions;

namespace GPCEmulator.BUSDevices.MainComponents
{
    public class CPU:BusDevice
    {
        private byte ARegister, BRegister, Accumulator, ORRegister, ANDRegister,NOTRegister,XORRegister,SLRegister,SRRegister, StepCounter, InstructionRegister = 0x0;
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
                
                case (byte)InstructionSet.LDA2:
                    switch (StepCounter)
                    {
                        case 3:
                            MemoryPointer++;
                            bus.setWriteFlag(false);
                            bus.writeAddress(MemoryPointer); 
                            break;
                        case 4:
                            ARegister = bus.readData();
                            resetCycle();
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
                
                case (byte)InstructionSet.LDB2:
                    switch (StepCounter)
                    {
                        case 3:
                            MemoryPointer++;
                            bus.setWriteFlag(false);
                            bus.writeAddress(MemoryPointer); 
                            break;
                        case 4:
                            BRegister = bus.readData();
                            resetCycle();
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
                
                case (byte)InstructionSet.ADC1: //ADC
                    if (ARegister + BRegister > 0xFF)
                    {
                        CarryFlag = true;
                    }
                    else
                    {
                        CarryFlag = false;
                    }

                    ARegister = Accumulator;
                    resetCycle();
                    break;
                    
                    
                case (byte)InstructionSet.ADC2:  //ADC
                    switch (StepCounter)
                    {
                        case 3:
                            if (ARegister + BRegister > 0xFF)
                            {
                                CarryFlag = true;
                            }
                            else
                            {
                                CarryFlag = false;
                            }
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
                    
                case (byte)InstructionSet.AND1: //AND
                    ARegister = ANDRegister;
                    resetCycle();
                    break;
                    
                    
                case (byte)InstructionSet.AND2:  //AND
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
                            bus.writeData(ANDRegister);
                            resetCycle();
                            break;
                        default:
                            resetCycle();
                            throw new Exception("Invalid Step for OP Code: " + InstructionRegister.ToString("X"));
                            break;
                    }
                    break;
                case (byte)InstructionSet.OR1: //OR
                    ARegister = ORRegister;
                    resetCycle();
                    break;
                    
                    
                case (byte)InstructionSet.OR2:  //OR
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
                            bus.writeData(ORRegister);
                            resetCycle();
                            break;
                        default:
                            resetCycle();
                            throw new Exception("Invalid Step for OP Code: " + InstructionRegister.ToString("X"));
                            break;
                    }
                    break;
                case (byte)InstructionSet.NOT1: //NOT
                    ARegister = NOTRegister;
                    resetCycle();
                    break;
                    
                    
                case (byte)InstructionSet.NOT2:  //NOT
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
                            bus.writeData(NOTRegister);
                            resetCycle();
                            break;
                        default:
                            resetCycle();
                            throw new Exception("Invalid Step for OP Code: " + InstructionRegister.ToString("X"));
                            break;
                    }
                    break;
                case (byte)InstructionSet.XOR1: //XOR
                    ARegister = XORRegister;
                    resetCycle();
                    break;
                    
                    
                case (byte)InstructionSet.XOR2:  //XOR
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
                            bus.writeData(XORRegister);
                            resetCycle();
                            break;
                        default:
                            resetCycle();
                            throw new Exception("Invalid Step for OP Code: " + InstructionRegister.ToString("X"));
                            break;
                    }
                    break;
                case (byte)InstructionSet.SL1: //SL
                    ARegister = SLRegister;
                    resetCycle();
                    break;
                    
                    
                case (byte)InstructionSet.SL2:  //SL
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
                            bus.writeData(SLRegister);
                            resetCycle();
                            break;
                        default:
                            resetCycle();
                            throw new Exception("Invalid Step for OP Code: " + InstructionRegister.ToString("X"));
                            break;
                    }
                    break;
                case (byte)InstructionSet.SR1: //SR
                    ARegister = SRRegister;
                    resetCycle();
                    break;
                    
                    
                case (byte)InstructionSet.SR2:  //SR
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
                            bus.writeData(SRRegister);
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
                                
                case (byte)InstructionSet.JZE: //JZE
                    if (ZeroFlag)
                    {
                        switch (StepCounter)
                        {
                            case 3:
                                MemoryPointer++;
                                bus.setWriteFlag(false);
                                bus.writeAddress(MemoryPointer);
                                break;
                            case 4:
                                XRegister = bus.readData();
                                XRegister = (UInt16) (XRegister << 8);
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
                        
                    }else
                    {
                        MemoryPointer++;
                        MemoryPointer++;
                        resetCycle();
                        break;
                    }

                    break;
                case (byte)InstructionSet.JCA: //JCA
                    if (CarryFlag)
                    {
                        switch (StepCounter)
                        {
                            case 3:
                                MemoryPointer++;
                                bus.setWriteFlag(false);
                                bus.writeAddress(MemoryPointer);
                                break;
                            case 4:
                                XRegister = bus.readData();
                                XRegister = (UInt16) (XRegister << 8);
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
                        
                    }else
                    {
                        MemoryPointer++;
                        MemoryPointer++;
                        resetCycle();
                        break;
                    }

                    break;
                case (byte)InstructionSet.JEQ: //JEQ
                    if (EQFlag)
                    {
                        switch (StepCounter)
                        {
                            case 3:
                                MemoryPointer++;
                                bus.setWriteFlag(false);
                                bus.writeAddress(MemoryPointer);
                                break;
                            case 4:
                                XRegister = bus.readData();
                                XRegister = (UInt16) (XRegister << 8);
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
                        
                    }else
                    {
                        MemoryPointer++;
                        MemoryPointer++;
                        resetCycle();
                        break;
                    }

                    break;
                case (byte)InstructionSet.JNE: //JNE
                    if (!EQFlag)
                    {
                        switch (StepCounter)
                        {
                            case 3:
                                MemoryPointer++;
                                bus.setWriteFlag(false);
                                bus.writeAddress(MemoryPointer);
                                break;
                            case 4:
                                XRegister = bus.readData();
                                XRegister = (UInt16) (XRegister << 8);
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
                        
                    }else
                    {
                        MemoryPointer++;
                        MemoryPointer++;
                        resetCycle();
                        break;
                    }

                    break;
                case (byte)InstructionSet.JSR: //JSR
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
                           
                            bus.writeAddress(StackPointer);
                            bus.setWriteFlag(true);
                            break;
                        case 8:
                            bus.writeData((byte) (MemoryPointer>>8));
                            StackPointer--;
                            break;
                        case 9:
                            bus.writeAddress(StackPointer);
                            bus.setWriteFlag(true);
                            break;
                        case 10:
                            bus.writeData((byte) MemoryPointer);
                            StackPointer--;
                            MemoryPointer = XRegister;
                            StepCounter = 0;
                            bus.setWriteFlag(false);
                            break;
                        default:
                            resetCycle();
                            throw new Exception("Invalid Step for OP Code: " + InstructionRegister.ToString("X"));
                            break;
                            MemoryPointer++;
                    }
                    break;    
                case (byte)InstructionSet.RSR: //RSR
                    switch (StepCounter)
                    {
                        case 3: case 5: 
                            StackPointer++;
                            bus.setWriteFlag(false);
                            break;
                        case 4:
                            XRegister = bus.readData();
                            break;
                        case 6:
                            XRegister += (ushort) (bus.readData() << 8);
                            break;
                        case 7:
                            MemoryPointer = XRegister;
                            resetCycle();
                            break;
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
                
                Accumulator = (byte)((int)ARegister + (int)BRegister - 0x100);
            }
            else
            {
               
                Accumulator = (byte)(ARegister + BRegister);
            }

          
            EQFlag = ARegister == BRegister;
            ZeroFlag = ARegister == 0x00;
            ANDRegister = (byte)(ARegister & BRegister);
            NOTRegister = (byte)(ARegister^0b11111111);
            XORRegister = (byte)(ARegister ^ BRegister);
            ORRegister = (byte)(ARegister | BRegister);
            SLRegister = (byte)(ARegister << 1);
            SRRegister = (byte)(ARegister >> 1);
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