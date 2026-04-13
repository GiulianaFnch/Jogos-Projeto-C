namespace BatalhaNaval;

class Program
{
    static void Main(string[] args)
    {
        // 1. Instanciamos o motor 
        MotorBatalhaNaval motor = new MotorBatalhaNaval();
        
        // 2. Criamos a matriz de exibição (Interface)
        char[,] tabuleiroVisivel = new char[5, 5];
        int tentativas = 0;

        // Inicializar a interface e o motor
        InicializarInterface(tabuleiroVisivel);
        motor.InicializarTabuleiro(aleatorio: true); // Podemos escolher se queremos fixo ou aleatório!

        Console.WriteLine("==-- BEM-VINDO À BATALHA NAVAL --==");

        // 3. O loop agora pergunta ao motor se o jogo acabou
        while (!motor.VerificarVitoria())
        {
            ImprimirTabuleiro(tabuleiroVisivel);
            
            Console.WriteLine($"\nTentativa nº: {tentativas + 1}");
            Console.Write("Introduza a linha (1-5): ");
            string? inputLinha = Console.ReadLine();
            Console.Write("Introduza a coluna (A-E): ");
            string? inputColuna = Console.ReadLine()?.ToUpper();

            // Tradução de coordenadas (A-E/1-5 para 0-4)
            if (ValidarInput(inputLinha, inputColuna, out int linhaIdx, out int colunaIdx))
            {
                // 4. CHAMADA AO MOTOR: Processar o tiro
                string resultado = motor.ProcessarTiro(linhaIdx, colunaIdx);

                if (resultado == "REPETIDO")
                {
                    Console.WriteLine("⚠ Já jogaste nesta posição! Tenta outra.");
                }
                else
                {
                    tentativas++; // Só conta tentativa se não for repetida

                    if (resultado == "ACERTO")
                    {
                        Console.WriteLine("ACERTOU! Um barco foi atingido.");
                        tabuleiroVisivel[linhaIdx, colunaIdx] = 'X';
                    }
                    else // "AGUA"
                    {
                        Console.WriteLine("Água... Tenta novamente.");
                        tabuleiroVisivel[linhaIdx, colunaIdx] = 'O';
                    }
                }
            }
            else
            {
                Console.WriteLine("Erro: Coordenadas inválidas. Use 1-5 e A-E.");
            }
        }

        // 5. Finalização
        ImprimirTabuleiro(tabuleiroVisivel);
        Console.WriteLine("\nPARABÉNS! Encontraste todos os barcos.");
        Console.WriteLine($"Total de tentativas: {tentativas}");
    }

    // --- MÉTODOS DE APOIO DA INTERFACE (PESSOA 4) ---

    static void InicializarInterface(char[,] matriz)
    {
        for (int i = 0; i < 5; i++)
            for (int j = 0; j < 5; j++)
                matriz[i, j] = '~';
    }

    static void ImprimirTabuleiro(char[,] matriz)
    {
        Console.WriteLine("\n    A B C D E");
        Console.WriteLine("  -----------");
        for (int i = 0; i < 5; i++)
        {
            Console.Write((i + 1) + " | ");
            for (int j = 0; j < 5; j++)
            {
                Console.Write(matriz[i, j] + " ");
            }
            Console.WriteLine();
        }
    }

    static bool ValidarInput(string? l, string? c, out int li, out int ci)
    {
        li = -1; ci = -1;
        if (int.TryParse(l, out int valL) && valL >= 1 && valL <= 5)
        {
            li = valL - 1;
            if (!string.IsNullOrEmpty(c) && c.Length == 1 && c[0] >= 'A' && c[0] <= 'E')
            {
                ci = c[0] - 'A';
                return true;
            }
        }
        return false;
    }
}