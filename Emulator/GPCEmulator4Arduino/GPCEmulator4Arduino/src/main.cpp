#include <Arduino.h>

enum InstructionSet
{
    NOP = 0x00,     //No Operation, 3 Clock Cycles, No Parameters
    LDA1= 0x01,     //Load A Register from Memory, 8 Clock Cycles, 16bit Parameter
    STA = 0x02,     //Store A Register from Memory, 7 Clock Cycles, 16bit Parameter
    LDB1 = 0x03,    //Load B Register from Memory, 8 Clock Cycles, 16bit Parameter
    STB = 0x04,     //Store Register from Memory, 7 Clock Cycles, 16bit Parameter
    ADD1 = 0x05,    //ADD A and B Register store to Result in A Register, 3 Clock Cycles, No Parameter
    ADD2 = 0x06,    //ADD A and B Register and store Result in Memory, 8 Clock Cycles, 16bit Parameter
    MOV = 0x07,     //Moves Data between Memory Addresses, 15 Clock Cycles, 16bit Parameter, 16bit Parameter
    JMP = 0x08,     //Jumps to target Address in Memory, 7 Clock Cycles, 16 bit Parameter
    JCM = 0x09,     //Jumps to target Address in Memory if JMP flag = true, x Clock Cycles, 16 bit Parameter
    JNC = 0x0A,     //Jumps to target Address in Memory if JMP flag = false, x Clock Cycles, 16 bit Parameter
    CMP = 0x0B,     //Sets JMP flag if A Register is greater than > B Register, x Clock Cycles, No Parameter
}

class Bus
{
  private byte Data = 0x00;
  private UInt16 Address = 0x00;
  private bool WriteFlag = false;

  public void writeData(byte data)
  {
    Data = data;
  }
  public byte readData()
  {
    return Data;
  }
  public void writeAddress(UInt16 address)
  {
    Address = address;
  }
  public UInt16 readAddress()
  {
    return Address;
  }
  public void setWriteFlag(bool writeFlag)
  {
    WriteFlag = writeFlag
  }
  public bool getWriteFlag()
  {
    return WriteFlag;
  }
}

class CPU
{
  private byte ARegister, BRegister, Accumulator, StepCounter, InstructionRegister = 0x00;
  private UInt16 MemoryPointer, XRegister, StackPointer = 0x00;
  private bool CarryFlag, ZeroFlag, EQFlag = false;

  private void ResetCycle()
  {
    StepCounter = 0;
    MemoryPointer++;
  }

  private void GetAddress(byte step)
  {
    switch (step)
    {
      case 3:
          MemoryPointer++;
          Bus.setWriteFlag(false);
          Bus.writeAddress(MemoryPointer);
          break;
      case 4:
          XRegister = Bus.readData();
          XRegister = (UInt16)(XRegister << 8);
          break;
      case 5:
          MemoryPointer++;
          Bus.setWriteFlag(false);
          Bus.writeAddress(MemoryPointer);
          break;
      case 6:
          XRegister += Bus.readData();
          break;
    }
  }


  private void Fetch()
  {
    if (StepCounter == 1)
    {
      Bus.setWriteFlag(false);
      Bus.writeAddress(MemoryPointer);
    }
    else
    {

        InstructionRegister = Bus.readData();
    }
  }

  private void Execute()
  {
    switch (InstructionRegister)
    {
      case (byte)InstructionSet.NOP:
        resetCycle();
        break;
      case (byte)InstructionSet.LDA1:
        if(stepCounter==7)
        {
          Bus.setWriteFlag(false);
          Bus.writeAddress(XRegister);
        }
        else if(stepCounter==8)
        {

        }
        else
        {
          ARegister = Bus.readData();
          ResetCycle();
        }
        break;
      case (byte)InstructionSet.STA:
        if(stepCounter==7)
        {
          Bus.setWriteFlag(true);
          Bus.writeAddress(XRegister);
          Bus.writeData(ARegister);
          resetCycle();
        }
        else
        {
          GetAddress(StepCounter);
        }
        break;
      case (byte)InstructionSet.LDB1:
          if(stepCounter==7)
          {
            Bus.setWriteFlag(false);
            Bus.writeAddress(XRegister);
          }
          else if(stepCounter==8)
          {

          }
          else
          {
            BRegister = Bus.readData();
            ResetCycle();
          }
        break;
      case (byte)InstructionSet.STB:
          if(stepCounter==7)
          {
            Bus.setWriteFlag(true);
            Bus.writeAddress(XRegister);
            Bus.writeData(BRegister);
            resetCycle();
          }
          else
          {
            GetAddress(StepCounter);
          }
        break;
      case (byte)InstructionSet.ADD1:
        ARegister = Accumulator;
        resetCycle();
        break;
      case (byte)InstructionSet.ADD2:
        if(StepCounter==7)
        {
          Bus.setWriteFlag(true);
          Bus.writeAddress(XRegister);
          Bus.writeData(Accumulator);
          resetCycle();
        }
        else
        {
          GetAddress(StepCounter);
        }
    }
  }
}




void setup() {
  // put your setup code here, to run once:
}

void loop() {
  // put your main code here, to run repeatedly:
}
