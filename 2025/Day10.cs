using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Utils;
using Microsoft.Z3;

namespace _2025
{
    public class Day10 : IDay
    {
        public static long Part_1(string input)
        {
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            string[] targets = new string[lines.Length];
            List<int>[][] buttons = new List<int>[lines.Length][];

            for (int i = 0; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(" ", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                targets[i] = parts[0].Substring(1, parts[0].Length - 2);
                buttons[i] = new List<int>[parts.Length - 2];
                for (int j = 1; j < parts.Length - 1; j++)
                {
                    buttons[i][j - 1] = new List<int>(); 
                    parts[j] = parts[j].Substring(1, parts[j].Length - 2);
                    string[] numbers = parts[j].Split(",", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

                    for (int k = 0; k < numbers.Length; k++)
                    {
                        buttons[i][j-1].Add(int.Parse(numbers[k]));
                    }
                }

            }

            long sum = 0;
            Dictionary<string, long> map = new Dictionary<string, long>();
            List<string> currentLayer = new List<string>();
            for (int i = 0; i < targets.Length; i++)
            {
                map.Clear();
                currentLayer.Clear();

                string baseState = new string('.', targets[i].Length);
                
                map.Add(baseState, 0);
                currentLayer.Add(baseState);
                while (map.ContainsKey(targets[i]) == false)
                {
                    currentLayer = ExplorePaths(map, buttons[i], currentLayer);
                }
                sum += map[targets[i]];
            }
            return sum;
        }

        private static List<string> ExplorePaths(Dictionary<string, long> map, List<int>[] buttons, List<string> states)
        {
            List<string> addedStates = new List<string>();
            for (int i = 0; i < states.Count; i++)
            {
                for (int j = 0; j < buttons.Length; j++)
                {
                    string state = states[i];
                    state = ApplyButton(state, buttons[j]);
                    if (map.TryAdd(state, map[states[i]] + 1) == true)
                    {
                        addedStates.Add(state);
                    }
                }
            }
            return addedStates;
        }

        private static string ApplyButton(string state, List<int> button)
        {
            StringBuilder sbState = new StringBuilder(state);
            for (int i = 0; i < button.Count; i++)
            {
                sbState[button[i]] = sbState[button[i]] == '#' ? '.' : '#';
            }
            return sbState.ToString();
        }

        public static long Part_2(string input)
        {
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            string[] targets = new string[lines.Length];
            List<List<int>>[] buttons = new List<List<int>>[lines.Length];

            for (int i = 0; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(" ", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                targets[i] = parts[parts.Length - 1].Substring(1, parts[parts.Length - 1].Length - 2);
                buttons[i] = new List<List<int>>();
                for (int j = 1; j < parts.Length - 1; j++)
                {
                    buttons[i].Add(new List<int>());
                    parts[j] = parts[j].Substring(1, parts[j].Length - 2);
                    string[] numbers = parts[j].Split(",", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

                    for (int k = 0; k < numbers.Length; k++)
                    {
                        buttons[i][j - 1].Add(int.Parse(numbers[k]));
                    }
                }

            }

            long sum = 0;
            List<int> target = new List<int>();
            for (int i = 0; i < targets.Length; i++)
            {
                target.Clear();
                string[] num = targets[i].Split(",", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                for(int j = 0; j < num.Length; j++)
                {
                    target.Add(int.Parse(num[j]));
                }
                sum += Z3LinearEquationSolver(buttons[i], target);
            }
            return sum;
        }

        // Disclaimer written by AI because I have never used Z3 before to solve linear equations
        private static long Z3LinearEquationSolver(List<List<int>> sparseVectors, List<int> target)
        {
            int numVectors = sparseVectors.Count;
            int dimension = target.Count;

            //Console.WriteLine($"Solving for {numVectors} sparse vectors, dimension {dimension}");
            //Console.WriteLine($"Target: [{string.Join(", ", target)}]");
            //Console.WriteLine("\nSparse vectors:");
            //for (int i = 0; i < numVectors; i++)
            //{
            //    Console.WriteLine($"  v[{i}]: indices [{string.Join(",", sparseVectors[i])}]");
            //}

            // STEP 1: Create a Z3 Context
            // This is the "world" where all Z3 operations happen
            using (Context ctx = new Context())
            {
                // STEP 2: Create an Optimizer (instead of basic Solver)
                // Optimizer can minimize/maximize objectives while satisfying constraints
                Optimize opt = ctx.MkOptimize();

                // STEP 3: Create variables for coefficients
                // We need one coefficient for each sparse vector
                IntExpr[] coeffs = new IntExpr[numVectors];

                for (int i = 0; i < numVectors; i++)
                {
                    // MkIntConst creates an integer variable with a name
                    coeffs[i] = ctx.MkIntConst($"c{i}");

                    // STEP 4: Add constraint that coefficient must be >= 0
                    // MkGe = "greater than or equal"
                    // This ensures all coefficients are non-negative integers
                    opt.Assert(ctx.MkGe(coeffs[i], ctx.MkInt(0)));
                }

                // STEP 5: Add constraints for each dimension of the target vector
                // For each position in the target vector, the sum of coefficients
                // (where the sparse vector has a 1) must equal the target value

                for (int d = 0; d < dimension; d++)
                {
                    // Build the sum: c[0]*v[0][d] + c[1]*v[1][d] + ... + c[n]*v[n][d]
                    // where v[i][d] is 1 if d is in sparseVectors[i], else 0

                    ArithExpr[] terms = new ArithExpr[numVectors];

                    for (int v = 0; v < numVectors; v++)
                    {
                        // Check if this sparse vector has a 1 at dimension d
                        bool hasOne = sparseVectors[v].Contains(d);

                        if (hasOne)
                        {
                            // If yes, include the coefficient in the sum
                            terms[v] = coeffs[v];
                        }
                        else
                        {
                            // If no, contribute 0 to the sum
                            terms[v] = ctx.MkInt(0);
                        }
                    }

                    // MkAdd creates the sum of all terms
                    ArithExpr sum = ctx.MkAdd(terms);

                    // MkEq creates an equality constraint
                    // Assert adds the constraint to the optimizer
                    // This says: "the sum must equal target[d]"
                    opt.Assert(ctx.MkEq(sum, ctx.MkInt(target[d])));
                }

                // STEP 6: Define the objective to minimize
                // We want to minimize the sum of all coefficients
                // MkAdd sums all coefficient variables
                ArithExpr objective = ctx.MkAdd(coeffs);

                // MkMinimize tells the optimizer to find the smallest value
                // of this expression while satisfying all constraints
                opt.MkMinimize(objective);

                // STEP 7: Solve the problem
                // Check() runs the solver and returns SATISFIABLE, UNSATISFIABLE, or UNKNOWN
                //Console.WriteLine("\nSolving...");

                if (opt.Check() == Status.SATISFIABLE)
                {
                    // STEP 8: Extract the solution
                    // Model contains the values Z3 found for all variables
                    Model model = opt.Model;

                    //Console.WriteLine("\n✓ Solution found!");
                    //Console.WriteLine("\nCoefficients:");

                    long sumCoeffs = 0;
                    long[] coeffValues = new long[numVectors];

                    for (int i = 0; i < numVectors; i++)
                    {
                        // Evaluate gets the concrete value Z3 assigned to this variable
                        var coeffValue = model.Evaluate(coeffs[i]);

                        // Convert to integer
                        long intValue = long.Parse(coeffValue.ToString());
                        coeffValues[i] = intValue;
                        sumCoeffs += intValue;

                        //Console.WriteLine($"  c[{i}] = {intValue}  (for vector [{string.Join(",", sparseVectors[i])}])");
                    }

                    //Console.WriteLine($"\n✓ Sum of coefficients: {sumCoeffs}");
                    //Console.WriteLine($"  (This is the MINIMUM possible sum)");

                    // STEP 9: Verify the solution
                    //Console.WriteLine("\nVerification:");
                    long[] computed = new long[dimension];

                    for (int d = 0; d < dimension; d++)
                    {
                        long sum = 0;
                        for (int v = 0; v < numVectors; v++)
                        {
                            if (sparseVectors[v].Contains(d))
                            {
                                sum += coeffValues[v];
                            }
                        }
                        computed[d] = sum;
                    }

                    //Console.WriteLine($"Computed: [{string.Join(", ", computed)}]");
                    //Console.WriteLine($"Target:   [{string.Join(", ", target)}]");
                    //
                    //bool match = computed.SequenceEqual(target);
                    //Console.WriteLine($"Match: {(match ? "✓ YES" : "✗ NO")}");
                    //
                    //if (match)
                    //{
                    //    Console.WriteLine("\n✓ Solution is correct!");
                    //}

                    return sumCoeffs;
                }
                else if (opt.Check() == Status.UNSATISFIABLE)
                {
                    Console.WriteLine("\n✗ No solution exists!");
                    Console.WriteLine("The target vector cannot be expressed as a combination of the given sparse vectors.");
                }
                else
                {
                    Console.WriteLine("\n? Unknown status (timeout or error)");
                }
            }
            return -1;
        }

        private static bool CheckValid(List<int> state, List<int> target)
        {
            for (int i = 0; i < state.Count; i++)
            {
                if (state[i]  > target[i]) { return false; }
            }
            return true;
        }
        private static List<int> GetState(List<List<int>> buttons, List<int> presses, int length)
        {
            List<int> state = Enumerable.Repeat(0, length).ToList();
            for (int i = 0; i < presses.Count; i++)
            {
                for (int j = 0; j < buttons[i].Count; j++)
                {
                    state[buttons[i][j]] += presses[i]; 
                }
            }
            return state;
        }

        private static List<List<int>> ExploreJoltage(Dictionary<string, long> map, List<List<int>> buttons, List<List<int>> states, List<int> target)
        {
            List<List<int>> addedStates = new List<List<int>>();
            for (int i = 0; i < states.Count; i++)
            {
                for (int j = 0; j < buttons.Count; j++)
                {
                    List<int> state = new List<int>(states[i]); 
                    string baseStateStr = string.Join(",", state.Select(n => n.ToString()).ToArray());
                    state = ApplyButtonJoltage(state, buttons[j]);
                    bool tooLarge = false;
                    for (int k = 0; k < state.Count; k++)
                    {
                        if (state[k] > target[k]) { tooLarge = true; break; }
                    }
                    if (tooLarge) { continue; }
                    string stateStr = string.Join(",", state.Select(n => n.ToString()).ToArray());
                    if (map.TryAdd(stateStr, map[baseStateStr] + 1) == true)
                    {
                        addedStates.Add(state);
                    }
                }
            }
            return addedStates;
        }

        private static List<int> ApplyButtonJoltage(List<int> state, List<int> button)
        {
       
            for (int i = 0; i < button.Count; i++)
            {
                state[button[i]]++;
            }
            return state;
        }
    }
}
