using System;

namespace RadixSortGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Bem-vindo ao Jogo Radix Sort Games!");
            
            // 1. Geração de números aleatórios
            Random rand = new Random();
            int[] data = new int[6];
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = rand.Next(10, 100); // Gera números entre 10 e 99
            }

            Console.WriteLine("Tente ordenar os números manualmente:");
            Console.WriteLine(string.Join(", ", data));

            // 2. Solicita ao usuário a tentativa de ordenação
            Console.WriteLine("Digite os números ordenados (separados por espaço):");
            string input = Console.ReadLine();
            int[] userOrder = Array.ConvertAll(input.Split(' '), int.Parse);

            // 3. Verifica se o usuário acertou
            int[] sortedData = RadixSort(data);
            if (CompareArrays(userOrder, sortedData))
            {
                Console.WriteLine("Parabéns! Você ordenou corretamente.");
            }
            else
            {
                Console.WriteLine("Ops! Você não acertou a ordem.");
                Console.WriteLine("Veja como o Radix Sort organiza os números:");
                DisplayRadixSortProcess(data);
            }
        }

        // Função de comparação dos arrays
        static bool CompareArrays(int[] userOrder, int[] correctOrder)
        {
            if (userOrder.Length != correctOrder.Length)
                return false;

            for (int i = 0; i < userOrder.Length; i++)
            {
                if (userOrder[i] != correctOrder[i])
                    return false;
            }

            return true;
        }

        // Função de ordenação Radix Sort
        static int[] RadixSort(int[] data)
        {
            int max = GetMax(data);

            for (int exp = 1; max / exp > 0; exp *= 10)
            {
                data = CountSort(data, exp);
            }

            return data;
        }

        // encontrar o valor máximo
        static int GetMax(int[] data)
        {
            int max = data[0];
            for (int i = 1; i < data.Length; i++)
            {
                if (data[i] > max)
                    max = data[i];
            }
            return max;
        }

        // Função de ordenação por contagem para cada dígito
        static int[] CountSort(int[] data, int exp)
        {
            int[] output = new int[data.Length];
            int[] count = new int[10];

            // Conta ocorrências dos dígitos
            for (int i = 0; i < data.Length; i++)
                count[(data[i] / exp) % 10]++;

            // Ajusta count[] para ter as posições corretas
            for (int i = 1; i < 10; i++)
                count[i] += count[i - 1];

            // Preenche o array de saída
            for (int i = data.Length - 1; i >= 0; i--)
            {
                int digit = (data[i] / exp) % 10;
                output[count[digit] - 1] = data[i];
                count[digit]--;
            }

            return output;
        }

        // Função para exibir o processo do Radix Sort passo a passo
        static void DisplayRadixSortProcess(int[] data)
        {
            int max = GetMax(data);
            for (int exp = 1; max / exp > 0; exp *= 10)
            {
                Console.WriteLine($"\nOrdenando pelo dígito na posição {exp}:");
                data = CountSort(data, exp);
                Console.WriteLine(string.Join(", ", data));
            }

            Console.WriteLine("\nOrdenação completa!");
        }
    }
}
