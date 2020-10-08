using System;
using System.Collections.Generic;
using System.Globalization;
using GPCEmulator;
using GPCEmulator.BUSDevices.MainComponents;

namespace GPCASM
{
    internal class Program
    {
        public static Dictionary<InstructionSet,int> byteLength = new Dictionary<InstructionSet, int>();
        public static Dictionary<String,UInt16> jmpAddress = new Dictionary<string, ushort>();
        public static void Main(string[] args)
        {
            //Initialize InstructionSet
            byteLength.Add(InstructionSet.NOP,1);
            byteLength.Add(InstructionSet.LDA1,3);
            byteLength.Add(InstructionSet.STA,3);
            byteLength.Add(InstructionSet.LDB1,3);
            byteLength.Add(InstructionSet.STB,3);
            byteLength.Add(InstructionSet.ADD1,1);
            byteLength.Add(InstructionSet.ADD2,3);
            byteLength.Add(InstructionSet.MOV, 5);
            byteLength.Add(InstructionSet.JMP, 3);
       
            
            
            
            
            
        }

        public static List<UInt16> getAddressesForLines(string[] sourceCode)
        {
            List<UInt16> addresses = new List<ushort>();
            UInt16 curAddress = 0x0000;
            for (int i = 0; i < sourceCode.Length; i++)
            {
                addresses.Add(curAddress);
                if (sourceCode[i].StartsWith(".") )
                {
                    jmpAddress.Add(sourceCode[i], curAddress);
                }
                else if (sourceCode[i].StartsWith("//"))
                {
                    
                }else

                {
                    sourceCode[i] = sourceCode[i].Replace("\t", "");
                    string[] curLine = sourceCode[i].Split(' ');
                    switch (curLine[0])
                    {
                        case "NOP":
                            curAddress++;
                            break;
                        case "LDA":
                            curAddress += 3;
                            break;
                        case "STA":
                            curAddress += 3;
                            break;
                        case "LDB":
                            curAddress += 3;
                            break;
                        case "STB":
                            curAddress += 3;
                            break;
                        case "ADD":
                            if (curLine.Length == 1)
                            {
                                curAddress++;
                            }else if (curLine.Length == 2)
                            {
                                curAddress += 3;
                            }
                            else
                            {
                                throw new Exception("Invalid Argument Length for Comand ADD");
                            }
                            break;
                        case "MOV":
                            curAddress += 5;
                            break;
                        case "JMP":
                            curAddress += 3;
                            break;
                        case "DAT":
                            for (int i2 = 0; i2 < Int32.Parse(curLine[1]) / 8; i2++)
                            {
                                curAddress ++;
                            }
                           
                            break;

                        default:
                            throw new Exception("INVALID COMMAND: " + curLine[0]);
                            break;
                        
                    }
                    
                }
                
                
            }
            return addresses;
        }

        public static byte[] assemble(String[] sourceCode)
        {
            List<byte> target=new List<byte>();
            
               for (int i = 0; i < sourceCode.Length; i++)
            {
       
                if (sourceCode[i].StartsWith(".") )
                {
                
                }
                else if (sourceCode[i].StartsWith("//"))
                {
                    
                }else

                {
                    foreach(KeyValuePair<string, UInt16> entry in jmpAddress)
                    {
                        sourceCode[i] = sourceCode[i].Replace(entry.Key, entry.Value.ToString());
                    }
                    sourceCode[i] = sourceCode[i].Replace("\t", "");
                    string[] curLine = sourceCode[i].Split(' ');
                    switch (curLine[0])
                    {
                        case "NOP":
                       
                            break;
                        case "LDA":
                         
                            break;
                        case "STA":
                        
                            break;
                        case "LDB":
                      
                            break;
                        case "STB":
                     
                            break;
                        case "ADD":
                            if (curLine.Length == 1)
                            {
                           
                            }else if (curLine.Length == 2)
                            {
                                
                            }
                            break;
                        case "MOV":
                          
                            break;
                        case "JMP":
                          
                            break;
                        case "DAT":
                            Int64 value;
                            if (curLine[2].StartsWith("0x"))
                            {
                                value= Int64.Parse(curLine[2],NumberStyles.HexNumber);
                            }
                            break;
                        
                        default:
                            throw new Exception("INVALID COMMAND: " + curLine[0]);
                            break;
                        
                    }
                    
                }
                
                
            }
            
            
            

            return target.ToArray();
        }
        
        
        
        
        
    }
}