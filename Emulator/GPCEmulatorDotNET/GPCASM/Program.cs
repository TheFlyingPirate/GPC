using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using GPCEmulator;
using GPCEmulator.BUSDevices.MainComponents;

namespace GPCASM
{
 
    
    internal class Program
    {
        
      
        public static Dictionary<String,byte> Signatures = new Dictionary<String, byte>();
        public static Dictionary<String,UInt16> JmpAddress = new Dictionary<string, ushort>();
        public static void Main(string[] args)
        {
            Console.WriteLine("---GPCASM---");
            int FileCount = 0;
            string input = ".\\main.asm";
            string output = ".\\main.bin";
            string signatureFile = ".\\config\\sig.txt";
            if (args.Length == 2)
            {
                input = args[0];
                output = args[1];
            }else if (args.Length == 1)
            {
                input = args[0];
                output = args[0].Replace(".asm", ".bin");
            }
            foreach (string s in  File.ReadLines(signatureFile).ToArray())
            {
                Signatures.Add(s.Split(',')[0], Convert.ToByte(s.Split(',')[1], 16));
            }
            try
            {
                Console.WriteLine("Reading File: " + input);
                string[] source = PreParse(File.ReadLines(input).ToList(),ref FileCount).ToArray();
                try
                {
                    Console.WriteLine("Resolving Labels");
                    GetAddressesForLines(source);
                    try
                    {
                        Console.WriteLine("Assembling Files");
                        byte[] target = Assemble(source);
                        try
                        {
                            Console.WriteLine("Writing to binary to: " + output);
                            File.WriteAllBytes(output, target);
                            Console.WriteLine("Successfully Assembled: " + FileCount.ToString() + " File(s) with " + source.Length.ToString() + " Lines to " + target.Length.ToString() + " Bytes!");
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                            throw;
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            Console.WriteLine("Press any Key to Exit");
            Console.ReadKey();
        }
        
        public static List<String> PreParse(List<String> sourceCode, ref int FileCounter)
        {
            
            List<String> output=new List<string>();
            foreach (string s in sourceCode)
            {
                if (s.StartsWith("<include>"))
                {
                    string s1 = s.Replace("<include>", "").Replace("</include>","");
                   
                    Console.WriteLine("Reading File: " + s1);
                    if (File.Exists(s1))
                    {
                        List<string> ns = PreParse(File.ReadLines(s1).ToList(),ref FileCounter);
                        foreach (string s2 in ns)
                        {
                            output.Add(s2);

                        }
                    }else{
                        throw new Exception("File: " + s1 + " not found");
                    }
                }
                else
                {
                    output.Add(s);
                }
            }
            return output;
        }


        public static void GetAddressesForLines(string[] sourceCode)
        {
            List<UInt16> addresses = new List<ushort>();
            UInt16 curAddress = 0x0000;
            for (int i = 0; i < sourceCode.Length; i++)
            {
                addresses.Add(curAddress);
                if (sourceCode[i].StartsWith(".") )
                {
                    JmpAddress.Add(sourceCode[i], curAddress);
                }
                else if (sourceCode[i].StartsWith("//"))
                {
                    
                }
                else if (sourceCode[i].StartsWith("'"))
                {
                    if (sourceCode[i].EndsWith("'"))
                    {
                        curAddress += (UInt16)(sourceCode[i].Length - 2);
                    }
                    else
                    {
                        throw new Exception("Invalid String at Line: " + i.ToString());
                    }
                
                }
                else
                {
                    sourceCode[i] = sourceCode[i].Replace("\t", "");
                    string[] curLine = sourceCode[i].Split(' ');
                 
                    if (curLine[0] == "DAT")
                    {
                        for (int parameterIndex = 0; parameterIndex < Int32.Parse(curLine[1]) / 8; parameterIndex++)
                        {
                            curAddress ++;
                        }
                    }
                    else
                    {
                        curAddress++; 
                        for (int parameterIndex = 1; parameterIndex < curLine.Length; parameterIndex++)
                        {
                            if (curLine[parameterIndex].StartsWith("#"))
                            {
                                curAddress++;
                            }
                            else
                            {
                                curAddress += 2;
                            }
                        }
                    }
                }
            }
        }

        public static byte[] Assemble(String[] sourceCode)
        {
            List<byte> target=new List<byte>();
            
            for (int i = 0; i < sourceCode.Length; i++)
            {
       
                if (sourceCode[i].StartsWith(".") )
                {
                    
                }
                else if (sourceCode[i].StartsWith("//"))
                {
                    
                }else if (sourceCode[i].StartsWith("'"))
                {
                    for (int i2 = 1; i2 < sourceCode[i].Length - 1; i2++)
                    {
                        target.Add(Convert.ToByte(sourceCode[i][i2]));
                    }
                }
                else
                {
                    foreach(KeyValuePair<string, UInt16> entry in JmpAddress)
                    {
                        sourceCode[i] = sourceCode[i].Replace(entry.Key, entry.Value.ToString());
                    }
                    sourceCode[i] = sourceCode[i].Replace("\t", "");
                    string[] curLine = sourceCode[i].Split(' ');
                    UInt64 val = 0;
                    string sig = curLine[0];
                    if (sig == "DAT")
                    {
                        UInt64 value;
                        value = relitiveParser(curLine[2]);
                        int st = Int32.Parse((curLine[1]))/8;
                        List<byte> inverseList=new List<byte>();
                        for (int parameterIndex = 0; parameterIndex < st; parameterIndex++)
                        {
                            inverseList.Add((byte)value);
                            value = value >> 8;
                        }

                        for (int parameterIndex = inverseList.Count - 1; parameterIndex >= 0; parameterIndex--)
                        {
                            target.Add(inverseList[parameterIndex]);
                        }
                    }
                    else
                    {
                        for (int parameterIndex = 1; parameterIndex < curLine.Length; parameterIndex++)
                        {
                            if (curLine[parameterIndex].StartsWith("#"))
                            {
                                sig += ".D";
                            }
                            else
                            {
                                sig += ".A";
                            }
                        }
                        if (Signatures.ContainsKey(sig))
                        {
                            target.Add(Signatures[sig]);
                            string[] parms = sig.Split('.');
                            for (int parameterIndex = 1; parameterIndex < parms.Length; parameterIndex++)
                            {
                                if (parms[parameterIndex] == "D")
                                {
                                    val = relitiveParser(curLine[parameterIndex]);
                                    target.Add((byte)val);
                                }else if (parms[parameterIndex] == "A")
                                {
                                    val = relitiveParser(curLine[parameterIndex]);
                                    target.Add((byte)(val >> 8));
                            
                                    target.Add((byte)val);
                                }
                                else
                                {
                                    throw new Exception("Invalid Signature Parameter");
                                }
                            }

                        }
                        else
                        {
                            throw new Exception("OP Code Signature <"+ sig +"> at line: " + i.ToString() + " is not a valid Signature");
                        }
                    }
                }
            }
            return target.ToArray();
        }
        
        public static UInt64 relitiveParser(String input)
        {
            UInt64 output = 0;
            if (input.Contains("+"))
            {
                string[] ninput = input.Split('+');
                foreach (string s in ninput)
                {
                    output += convertFromString(s.Replace(" ", ""));
                }
            }else if (input.Contains("-"))
            {
                string[] ninput = input.Split('-');
                output = convertFromString(ninput[0].Replace(" ", "")) * 2;
                foreach (string s in ninput)
                {
                    output -= convertFromString(s.Replace(" ", ""));
                }
            }
            else
            {
                output = convertFromString(input);
            }
            return output;
        }

        public static UInt64 convertFromString(String input)
        {
            UInt64 output = 0;
            Int64 neg = 0;
            if (input.StartsWith("0x"))
            {
                output = Convert.ToUInt64(input,16);
            }else if (input.StartsWith("0b"))
            {
                output = Convert.ToUInt64(input.Replace("0b", ""),2);
            }
            else if(input.StartsWith("-"))
            {
                neg = Convert.ToInt64(input);
                output = (UInt64) neg;
                
            }
            else
            {
                output = Convert.ToUInt64(input);
            }
            return output;
        }
    }
}