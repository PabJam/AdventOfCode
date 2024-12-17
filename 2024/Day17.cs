using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using Utils;

namespace _2024
{
    public static class Day17
    {
        public const string mini_test_input = "";
        public const string test_input_1 = "Register A: 729\r\nRegister B: 0\r\nRegister C: 0\r\n\r\nProgram: 0,1,5,4,3,0";
        public const string input_1 = "Register A: 59397658\r\nRegister B: 0\r\nRegister C: 0\r\n\r\nProgram: 2,4,1,1,7,5,4,6,1,4,0,3,5,5,3,0";
        public const string test_input_2 = "Register A: 2024\r\nRegister B: 0\r\nRegister C: 0\r\n\r\nProgram: 0,3,5,4,3,0";
        public const string input_2 = input_1;
        private delegate void Instruction(ref long regA, ref long regB, ref long regC, int operenad, ref int pc, ref string output);

        // do
        // 2,4 : RegB = RegA % 8
        // 1,1 : RegB = RegB xor 1
        // 7,5 : RegC = RegA / (2 ^ RegB)
        // 4,6 : RegB = RegB xor RegC
        // 1,4 : RegB = RegB xor 4
        // 0,3 : RegA = RegA / (2^3)
        // 5,5 : print (RegB % 8)
        // 3,0 : while(RegA /= 0)

        public static long Part_1(string input)
        {
            int pc = 0;
            string output = "";
            List<int> program = new List<int>();
            string[] blocks = input.Split("\r\n\r\n", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            string[] initRegs = blocks[0].Split("\r\n", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            long regA = int.Parse(Regex.Match(initRegs[0], @"Register A: (-?\d+)").Groups[1].Value);
            long regB = int.Parse(Regex.Match(initRegs[1], @"Register B: (-?\d+)").Groups[1].Value);
            long regC = int.Parse(Regex.Match(initRegs[2], @"Register C: (-?\d+)").Groups[1].Value);
            string programStr = blocks[1].Substring("Program: ".Length);
            string[] programStrArr = programStr.Split(",", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < programStrArr.Length; i++)
            {
                program.Add(int.Parse(programStrArr[i]));
            }

            while (pc < program.Count - 1) 
            {
                Instruction instruction = GetInstruction(program[pc]);
                instruction(ref regA, ref regB, ref regC, program[pc + 1], ref pc, ref output);
                pc += 2;
            }
            Console.WriteLine(output.Substring(1));
            return 0;
        }

        public static long Part_2(string input)
        {
            int pc = 0;
            string output = "";
            List<int> program = new List<int>();
            string[] blocks = input.Split("\r\n\r\n", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            string[] initRegs = blocks[0].Split("\r\n", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            long initRegA = 0;
            long initRegB = int.Parse(Regex.Match(initRegs[1], @"Register B: (-?\d+)").Groups[1].Value);
            long initRegC = int.Parse(Regex.Match(initRegs[2], @"Register C: (-?\d+)").Groups[1].Value);
            string programStr = blocks[1].Substring("Program: ".Length);
            string[] programStrArr = programStr.Split(",", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < programStrArr.Length; i++)
            {
                program.Add(int.Parse(programStrArr[i]));
            }

            int maxOutLength = 0;
            while (true)
            {
                long regA = initRegA;
                long regB = initRegB;
                long regC = initRegC;
                output = "";
                pc = 0;

                while (pc < program.Count - 1)
                {
                    Instruction instruction = GetInstruction(program[pc]);
                    instruction(ref regA, ref regB, ref regC, program[pc + 1], ref pc, ref output);
                    pc += 2;
                }

                string[] outputLines = output.Split(",", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

                if (maxOutLength < outputLines.Length)
                {
                    Console.WriteLine(maxOutLength.ToString() + " | " + initRegA.ToString());
                }
                if (output.Substring(1) == programStr) { break; }
                initRegA++;
            }
            
            return initRegA;
        }

        private static long GetComboOperand(int opcode, long regA, long regB, long regC)
        {
            long operand = 0;
            switch (opcode)
            {
                case 0:
                    operand = 0;
                    break;
                case 1:
                    operand = 1;
                    break;
                case 2:
                    operand = 2;
                    break;
                case 3:
                    operand = 3;
                    break;
                case 4:
                    operand = regA;
                    break;
                case 5:
                    operand = regB;
                    break;
                case 6:
                    operand = regC;
                    break;
                case 7:
                    throw new Exception("Can't handle combo operand 7");
            }

            return operand;
        }

        private static Instruction GetInstruction(int opcode)
        {
            switch (opcode)
            {
                case 0:
                    return adv;
                case 1:
                    return bxl;
                case 2:
                    return bst;
                case 3:
                    return jnz;
                case 4:
                    return bxc;
                case 5:
                    return _out;
                case 6:
                    return bdv;
                case 7:
                    return cdv;
            }

            throw new Exception("Unknown Opcode: " + opcode);
        }

        private static void adv(ref long regA, ref long regB, ref long regC, int operand, ref int pc, ref string output)
        {
            regA = regA / (long)BigInteger.Pow(2, (int)GetComboOperand(operand, regA, regB, regC));
        }

        private static void bxl(ref long regA, ref long regB, ref long regC, int operand, ref int pc, ref string output)
        {
            regB = regB ^ operand;
        }

        private static void bst(ref long regA, ref long regB, ref long regC, int operand, ref int pc, ref string output)
        {
            regB = GetComboOperand(operand, regA, regB, regB) % 8;
        }

        private static void jnz(ref long regA, ref long regB, ref long regC, int operand, ref int pc, ref string output)
        {
            if (regA == 0) { return; }
            pc = operand - 2;
        }

        private static void bxc(ref long regA, ref long regB, ref long regC, int operand, ref int pc, ref string output)
        {
            regB = regB ^ regC;
        }

        private static void _out(ref long regA, ref long regB, ref long regC, int operand, ref int pc, ref string output)
        {
            output += "," + (GetComboOperand(operand, regA, regB, regC) % 8).ToString();
        }

        private static void bdv(ref long regA, ref long regB, ref long regC, int operand, ref int pc, ref string output)
        {
            regB = regA / (long)BigInteger.Pow(2, (int)GetComboOperand(operand, regA, regB, regC));
        }

        private static void cdv(ref long regA, ref long regB, ref long regC, int operand, ref int pc, ref string output)
        {
            regC = regA / (long)BigInteger.Pow(2, (int)GetComboOperand(operand, regA, regB, regC));
        }



    }
}
