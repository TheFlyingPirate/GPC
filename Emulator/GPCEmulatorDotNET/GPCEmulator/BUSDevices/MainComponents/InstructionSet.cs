namespace GPCEmulator.BUSDevices.MainComponents
{
    public enum InstructionSet
    {
        NOP = 0x00,     //No Operation, 3 Clock Cycles, No Parameters
        LDA1= 0x01,     //Load A Register from Memory, 8 Clock Cycles, 16bit Parameter
        LDA2= 0x02,
        LDB1 = 0x03,    //Load B Register from Memory, 8 Clock Cycles, 16bit Parameter
        LDB2 = 0x04,
        STA = 0x05,     //Store A Register from Memory, 7 Clock Cycles, 16bit Parameter
        STB = 0x06,     //Store Register from Memory, 7 Clock Cycles, 16bit Parameter
        ADD1 = 0x10,    //ADD A and B Register store to Result in A Register, 3 Clock Cycles, No Parameter
        ADD2 = 0x11,    //ADD A and B Register and store Result in Memory, 8 Clock Cycles, 16bit Parameter
        ADC1 = 0x12,
        ADC2 = 0x13,
        AND1 = 0x14,
        AND2 = 0x15,
        OR1 = 0x16,
        OR2 = 0x17,
        NOT1=0x18,
        NOT2=0x19,
        XOR1=0x1A,
        XOR2=0x1B,
        SL1=0x1C,
        SL2=0x1D,
        SR1=0x1E,
        SR2=0x1F,
        JMP = 0x30,     //Jumps to target Address in Memory, 7 Clock Cycles, 16 bit Parameter
        JZE = 0x31,
        JCA = 0x32,
        JEQ = 0x33,
        JNE = 0x34,
        JSR = 0x35,
        RSR = 0x36
        
    }
}