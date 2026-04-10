namespace BatalhaNaval;

class Program
{
    static void Main(string[] args)
    {
        char[,] tabuleiroVisivel = new char[5, 5];
        
        int tentativas = 0;
        int barcosEncontrados = 0;
        const int TOTAL_BARCOS = 3; 

        // Inicializar o tabuleiro com água escondida '~'
        for (int l = 0; l < 5; l++)
        {
            for (int c = 0; c < 5; c++)
            {
                tabuleiroVisivel[l, c] = '~';
            }
        }

        Console.WriteLine("==-- BEM-VINDO À BATALHA NAVAL --==");

        while (barcosEncontrados < TOTAL_BARCOS)
        {
            ImprimirTabuleiro(tabuleiroVisivel);
            
            Console.WriteLine($"\nTentativa nº: {tentativas + 1}");
            Console.Write("Introduza a linha (1-5): ");
            string? inputLinha = Console.ReadLine();
            Console.Write("Introduza a coluna (A-E): ");
            string? inputColuna = Console.ReadLine()?.ToUpper(); // ToUpper() caso user escrever 'a' minúsculo

            int linhaIndex = -1;
            int colunaIndex = -1;

            // 1. Validar e converter a Linha (1-5 para 0-4)
            if (int.TryParse(inputLinha, out int linhaEscolhida) && linhaEscolhida >= 1 && linhaEscolhida <= 5)
            {
                linhaIndex = linhaEscolhida - 1; 
            }

            // 2. Validar e converter a Coluna (A-E para 0-4)
            if (!string.IsNullOrEmpty(inputColuna) && inputColuna.Length == 1)
            {
                char colChar = inputColuna[0];
                if (colChar >= 'A' && colChar <= 'E')
                {
                    // Truque: Na tabela ASCII, as letras têm valores numéricos.
                    // 'A' - 'A' = 0 | 'B' - 'A' = 1 | 'C' - 'A' = 2, etc.
                    colunaIndex = colChar - 'A';
                }
            }

            // 3. Verificar se as duas coordenadas são válidas
            if (linhaIndex != -1 && colunaIndex != -1)
            {
                // Impedir repetições
                if (tabuleiroVisivel[linhaIndex, colunaIndex] != '~')
                {
                    Console.WriteLine("ERRO - Já jogaste nesta posição! Tenta outra.");
                    continue; // Volta ao início do while
                }

                tentativas++;

                bool acertou = VerificarSeExisteBarco(linhaIndex, colunaIndex); 

                if (acertou)
                {
                    Console.WriteLine("ACERTOU! Um barco foi atingido.");
                    tabuleiroVisivel[linhaIndex, colunaIndex] = 'X';
                    barcosEncontrados++;
                }
                else
                {
                    Console.WriteLine("Água... Tenta novamente.");
                    tabuleiroVisivel[linhaIndex, colunaIndex] = 'O';
                }
            }
            else
            {
                Console.WriteLine("ERRO - Coordenadas inválidas. Certifique-se de usar linhas de 1 a 5 e colunas de A a E.");
            }
        }

        ImprimirTabuleiro(tabuleiroVisivel);
        Console.WriteLine("\nPARABÉNS! Encontraste todos os barcos.");
        Console.WriteLine($"Total de tentativas: {tentativas}");
    }

    // Função para desenhar o tabuleiro com letras e números
    static void ImprimirTabuleiro(char[,] matriz)
    {
        Console.WriteLine("\n    A B C D E"); // Letras no topo
        Console.WriteLine("  -----------");
        for (int i = 0; i < 5; i++)
        {
            Console.Write((i + 1) + " | "); // Números de 1 a 5 na lateral (i + 1)
            for (int j = 0; j < 5; j++)
            {
                Console.Write(matriz[i, j] + " ");
            }
            Console.WriteLine();
        }
    }

    // MÉTODO PLACEHOLDER (alisson vai fazer essa função)
    static bool VerificarSeExisteBarco(int l, int c)
    {
        // Posição de teste: Linha 2, Coluna B (índices 1 e 1)
        if (l == 1 && c == 1) return true;
        return false; 
    }
}