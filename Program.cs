using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*******
 * Read input from Console
 * Use: Console.WriteLine to output your result to STDOUT.
 * Use: Console.Error.WriteLine to output debugging information to STDERR;
 *       
 * ***/
namespace CSharpContestProject
{
    /// <summary>
    /// Implémenté la solution dans cette classe
    /// </summary>
    public class Solution : SolutionAbstract
    {
        int[][] terminal;
        Dictionary<Tuple<int,int>, int> valueCalc;

        override public void Main()
        {
            int nbTerminal = int.Parse(ReadLine());

            terminal = Enumerable.Range(0, nbTerminal ).Select(e => ReadLine().Split(' ').Select(t => int.Parse(t)).ToArray()).ToArray();

            valueCalc = new Dictionary<Tuple<int, int>, int>();
            var nb = Calc(0, nbTerminal-1);

            WriteLine(nb.ToString());
        }
        int Calc(int start, int end)
        {
            var newStart = start + 1;
            if (newStart > end)
            {
                return 0;
            }
            else
            {
                var key = new Tuple<int, int>(Math.Min(start, end), Math.Max(start, end));

                int result = 0;

                if (!valueCalc.TryGetValue(key, out result))
                {
                    var query = Enumerable.Range(newStart, end - newStart + 1).Select(i =>
                    {
                        return terminal[start][i] + Calc(newStart, i - 1) + Calc(i + 1, end);
                    }).ToList();
                    result = Math.Max(query.Max(), Calc(newStart, end));

                    valueCalc.Add(key, result);
                }

                return result;

            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            using (var s = new Solution())
            {
#if DEBUG
                DateTime dt = DateTime.Now;
#endif
                s.Main();
#if DEBUG
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine((DateTime.Now - dt).TotalMilliseconds);
                Console.ReadLine();
#endif
            }
        }
    }

   

   public abstract class SolutionAbstract  : IDisposable
    {
        const int nbTest = 1;

        System.IO.StreamReader fileInput;
        System.IO.StreamReader fileOutput;

        public SolutionAbstract()
        {
#if DEBUG
            fileInput = new System.IO.StreamReader($"{Environment.CurrentDirectory}\\test\\input{nbTest}.txt");
            fileOutput = new System.IO.StreamReader($"{Environment.CurrentDirectory}\\test\\output{nbTest}.txt");
#endif
        }
        public string ReadLine()
        {
#if DEBUG
            return fileInput.ReadLine();
#else
            return Console.ReadLine();
#endif
        }

        public void WriteDebug(String debug)
        {
#if DEBUG
            Console.ForegroundColor = ConsoleColor.DarkYellow;
#endif
            Console.Error.WriteLine("debug");
        }

        public void WriteLine(String msg)
        {
#if DEBUG
            string result = fileOutput.ReadLine();
            if (msg == result)
            {

                Console.ForegroundColor = ConsoleColor.Green;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"Attendu ({result})\t ");
                
            }
#endif
            Console.WriteLine(msg);
        }

        // Ce code est ajouté pour implémenter correctement le modèle supprimable.
        void IDisposable.Dispose()
        {
#if DEBUG
            fileInput.Close();
            fileOutput.Close();
#endif

        }
        abstract public void Main();

    }

}
