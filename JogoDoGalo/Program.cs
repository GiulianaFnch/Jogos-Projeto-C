using System;
using System.Threading;

namespace JogoDoGalo
{
    class Program
    {
        // Variáveis globais do jogo
        static char[,] tabuleiro = new char[3, 3]; // matriz 3x3 que representa o tabuleiro do jogo
        static char jogadorAtual = 'X'; // indica de quem é a vez; o jogador X começa sempre
        static int contadorJogadas = 0; // conta o número total de jogadas feitas durante a partida
        static Random rnd = new Random(); // gerador aleatório único usado pelo computador

        static void Main(string[] args) // método principal que inicia o programa
        {
            bool jogarNovamente = true; // controla se o utilizador quer jogar outra vez

            while (jogarNovamente) // repete o programa enquanto o utilizador quiser continuar
            {
                bool contraPC = MostrarMenu(); // mostra o menu inicial e devolve true se for contra o computador

                InicializarJogo(); // limpa o tabuleiro e reinicia as variáveis da partida
                bool jogoAcabou = false; // indica se a partida terminou

                while (!jogoAcabou) // continua enquanto não houver vitória nem empate
                {
                    DesenharTabuleiro(); // mostra o tabuleiro atualizado

                    if (contraPC && jogadorAtual == 'O') // se for modo contra computador e for a vez do O, o computador joga
                    {
                        JogadaComputador();
                    }
                    else // caso contrário, joga um utilizador
                    {
                        FazerJogada();
                    }

                    if (VerificarVitoria()) // verifica se o jogador atual ganhou
                    {
                        DesenharTabuleiro(); // mostra o tabuleiro final
                        Console.WriteLine($"\nPARABÉNS! O jogador '{jogadorAtual}' venceu em {contadorJogadas} jogadas!");
                        jogoAcabou = true; // termina a partida
                    }
                    else if (Empate()) // verifica se o jogo terminou empatado
                    {
                        DesenharTabuleiro(); // mostra o estado final do tabuleiro
                        Console.WriteLine("\nEMPATE! O tabuleiro está cheio e ninguém venceu.");
                        jogoAcabou = true; // termina a partida
                    }
                    else
                    {
                        AlternarJogador(); // passa a vez para o outro jogador
                    }
                }

                Console.Write("\nQueres jogar novamente? (s/n): ");
                string resposta = Console.ReadLine(); // lê a resposta do utilizador

                if (string.IsNullOrWhiteSpace(resposta)) // se o utilizador não escrever nada, o jogo termina
                {
                    jogarNovamente = false;
                }
                else
                {
                    jogarNovamente = resposta.Trim().ToLower() == "s"; // continua apenas se a resposta for s
                }
            }
        }

        static bool MostrarMenu()
        {
            while (true) // repete até o utilizador escolher uma opção válida
            {
                Console.Clear();
                Console.WriteLine("========================");
                Console.WriteLine("      JOGO DO GALO      ");
                Console.WriteLine("========================");
                Console.WriteLine("1. Jogador vs Jogador");
                Console.WriteLine("2. Jogador vs Computador");
                Console.Write("Escolhe o modo de jogo (1 ou 2): ");

                string opcao = Console.ReadLine(); // lê a opção escolhida no menu

                if (opcao == "1") // se escolher 1, joga contra outro jogador
                {
                    return false;
                }
                else if (opcao == "2") // se escolher 2, joga contra o computador
                {
                    return true;
                }
                else
                {
                    Console.WriteLine("\nErro: opção inválida! Prime uma tecla para tentar novamente.");
                    Console.ReadKey();
                }
            }
        }

        static void InicializarJogo()
        {
            for (int i = 0; i < 3; i++) // percorre as linhas do tabuleiro
            {
                for (int j = 0; j < 3; j++) // percorre as colunas do tabuleiro
                {
                    tabuleiro[i, j] = ' '; // coloca espaço vazio em cada posição
                }
            }

            jogadorAtual = 'X'; // o jogador X começa sempre
            contadorJogadas = 0; // reinicia o contador de jogadas
        }

        static void AlternarJogador()
        {
            jogadorAtual = (jogadorAtual == 'X') ? 'O' : 'X'; // troca o jogador atual entre X e O
        }

        static bool VerificarVitoria()
        {
            for (int i = 0; i < 3; i++) // verifica linhas e colunas
            {
                if (tabuleiro[i, 0] != ' ' &&
                    tabuleiro[i, 0] == tabuleiro[i, 1] &&
                    tabuleiro[i, 1] == tabuleiro[i, 2]) // verifica se existe vitória numa linha
                {
                    return true;
                }

                if (tabuleiro[0, i] != ' ' &&
                    tabuleiro[0, i] == tabuleiro[1, i] &&
                    tabuleiro[1, i] == tabuleiro[2, i]) // verifica se existe vitória numa coluna
                {
                    return true;
                }
            }

            if (tabuleiro[0, 0] != ' ' &&
                tabuleiro[0, 0] == tabuleiro[1, 1] &&
                tabuleiro[1, 1] == tabuleiro[2, 2]) // verifica a diagonal principal
            {
                return true;
            }

            if (tabuleiro[0, 2] != ' ' &&
                tabuleiro[0, 2] == tabuleiro[1, 1] &&
                tabuleiro[1, 1] == tabuleiro[2, 0]) // verifica a diagonal secundária
            {
                return true;
            }

            return false; // se nenhuma condição for satisfeita, não há vencedor
        }

        static bool Empate()
        {
            return contadorJogadas >= 9; // se já houve 9 jogadas e ninguém venceu, é empate
        }

        static void DesenharTabuleiro()
        {
            Console.Clear();
            Console.WriteLine("========================");
            Console.WriteLine("      JOGO DO GALO      ");
            Console.WriteLine("========================\n");
            Console.WriteLine($"[ Jogadas: {contadorJogadas} ]");
            Console.WriteLine($"[ Turno do: {jogadorAtual} ]\n");

            Console.WriteLine("      0   1   2");
            Console.WriteLine("    +---+---+---+");

            for (int i = 0; i < 3; i++) // percorre as linhas para desenhar o tabuleiro
            {
                Console.Write("  " + i + " | ");

                for (int j = 0; j < 3; j++) // percorre as colunas da linha atual
                {
                    Console.Write(tabuleiro[i, j] + " | "); // escreve o conteúdo da célula
                }

                Console.WriteLine();
                Console.WriteLine("    +---+---+---+");
            }

            Console.WriteLine();
        }

        static void FazerJogada()
        {
            bool jogadaValida = false; // controla se a jogada introduzida é válida

            while (!jogadaValida) // repete até o jogador inserir uma jogada correta
            {
                Console.WriteLine($"Jogador '{jogadorAtual}', é a tua vez!");
                Console.Write("Escolhe a linha (0, 1 ou 2): ");
                string linhaInput = Console.ReadLine(); // lê a linha
                Console.Write("Escolhe a coluna (0, 1 ou 2): ");
                string colunaInput = Console.ReadLine(); // lê a coluna

                if (string.IsNullOrWhiteSpace(linhaInput) || string.IsNullOrWhiteSpace(colunaInput)) // verifica se algum valor foi deixado em branco
                {
                    Console.WriteLine("\nErro: tens de preencher linha e coluna.\n");
                    continue;
                }

                if (int.TryParse(linhaInput, out int linha) && int.TryParse(colunaInput, out int coluna)) // valida se as entradas são inteiros
                {
                    if (linha >= 0 && linha <= 2 && coluna >= 0 && coluna <= 2) // verifica se estão dentro dos limites do tabuleiro
                    {
                        if (tabuleiro[linha, coluna] == ' ') // verifica se a posição está livre
                        {
                            tabuleiro[linha, coluna] = jogadorAtual; // coloca o símbolo do jogador
                            contadorJogadas++; // incrementa o número de jogadas
                            jogadaValida = true; // termina o ciclo porque a jogada é válida
                        }
                        else
                        {
                            Console.WriteLine("\nErro: essa posição já está ocupada! Tenta de novo.\n");
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nErro: posição inválida! Os números têm de ser 0, 1 ou 2.\n");
                    }
                }
                else
                {
                    Console.WriteLine("\nErro: entrada inválida! Digita apenas números inteiros.\n");
                }
            }
        }

        static void JogadaComputador()
        {
            Console.WriteLine("O computador está a pensar...");
            Thread.Sleep(1000); // pausa curta para simular que o computador está a pensar

            if (contadorJogadas >= 9) // proteção extra para evitar tentar jogar num tabuleiro cheio
            {
                return;
            }

            bool jogadaValida = false; // controla se o computador já encontrou uma posição válida

            while (!jogadaValida) // repete até encontrar uma casa vazia
            {
                int linha = rnd.Next(0, 3); // sorteia uma linha entre 0 e 2
                int coluna = rnd.Next(0, 3); // sorteia uma coluna entre 0 e 2

                if (tabuleiro[linha, coluna] == ' ') // se a posição estiver vazia, faz a jogada
                {
                    tabuleiro[linha, coluna] = jogadorAtual;
                    contadorJogadas++;
                    jogadaValida = true;
                }
            }
        }
    }
}

