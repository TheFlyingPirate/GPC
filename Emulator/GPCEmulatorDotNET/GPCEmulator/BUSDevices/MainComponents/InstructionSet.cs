namespace GPCEmulator.BUSDevices.MainComponents
{
    public enum InstructionSet
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
        CMP = 0x0B      //Sets JMP flag if A Register is greater than > B Register, x Clock Cycles, No Parameter
    }
}