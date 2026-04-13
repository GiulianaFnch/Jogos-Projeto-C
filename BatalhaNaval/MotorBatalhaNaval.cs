using System;

class MotorBatalhaNaval
{
    // ═══════════════════════════════════════════════
    //  CONSTANTES
    // ═══════════════════════════════════════════════
    private const int TAMANHO = 5;
    private const int TOTAL_BARCOS = 3;
    private const char AGUA = '~';
    private const char BARCO = 'B';
    private const char ACERTO = 'X';
    private const char MISS = 'O';

    // ═══════════════════════════════════════════════
    //  MATRIZES
    // ═══════════════════════════════════════════════

    // Tabuleiro REAL (oculto) — onde estão os barcos
    private char[,] tabuleiro = new char[TAMANHO, TAMANHO];

    // Tabuleiro de TIROS já efetuados (para impedir repetições)
    private bool[,] jaAtirado = new bool[TAMANHO, TAMANHO];

    // Contador de barcos afundados
    private int barcosAfundados = 0;

    // ═══════════════════════════════════════════════
    //  INICIALIZAÇÃO
    // ═══════════════════════════════════════════════

    public void InicializarTabuleiro(bool aleatorio = false)
    {
        // Preencher tudo com água
        for (int i = 0; i < TAMANHO; i++)
            for (int j = 0; j < TAMANHO; j++)
            {
                tabuleiro[i, j] = AGUA;
                jaAtirado[i, j] = false;
            }

        barcosAfundados = 0;

        if (aleatorio)
            DistribuirBarcosAleatorio();
        else
            DistribuirBarcosFixo();
    }

    // Posições fixas/manuais (para debug e fase de desenvolvimento)
    private void DistribuirBarcosFixo()
    {
        tabuleiro[1, 1] = BARCO;
        tabuleiro[3, 2] = BARCO;
        tabuleiro[0, 4] = BARCO;
    }

    //  DESAFIO OPCIONAL: distribuição aleatória sem sobreposição
    private void DistribuirBarcosAleatorio()
    {
        Random rng = new Random();
        int colocados = 0;

        while (colocados < TOTAL_BARCOS)
        {
            int linha = rng.Next(0, TAMANHO);
            int coluna = rng.Next(0, TAMANHO);

            if (tabuleiro[linha, coluna] == AGUA)
            {
                tabuleiro[linha, coluna] = BARCO;
                colocados++;
            }
        }
    }

    // ═══════════════════════════════════════════════
    //  LÓGICA DE TIRO — método principal chamado pela Pessoa 4
    // ═══════════════════════════════════════════════

    /// <summary>
    /// Processa um tiro nas coordenadas (linha, coluna).
    /// Retorna: "ACERTO", "AGUA" ou "REPETIDO"
    /// </summary>
    public string ProcessarTiro(int linha, int coluna)
    {
        // 1. Verificar jogada repetida
        if (jaAtirado[linha, coluna])
            return "REPETIDO";

        // Marcar como já atirado
        jaAtirado[linha, coluna] = true;

        // 2. Verificar se acertou num barco
        if (tabuleiro[linha, coluna] == BARCO)
        {
            tabuleiro[linha, coluna] = ACERTO; // Atualiza tabuleiro real
            barcosAfundados++;
            return "ACERTO";
        }

        // 3. Água
        tabuleiro[linha, coluna] = MISS;
        return "AGUA";
    }

    // ═══════════════════════════════════════════════
    //  CONDIÇÃO DE VITÓRIA
    // ═══════════════════════════════════════════════

    /// <summary>
    /// Retorna true se todos os barcos foram afundados.
    /// </summary>
    public bool VerificarVitoria()
    {
        return barcosAfundados >= TOTAL_BARCOS;
    }

    public int GetBarcosAfundados() => barcosAfundados;
    public int GetTotalBarcos() => TOTAL_BARCOS;

    // ═══════════════════════════════════════════════
    //  DEBUG — Mostrar tabuleiro real (só para testes!)
    //  A Pessoa 4 NÃO deve chamar este método no jogo final
    // ═══════════════════════════════════════════════

    public void MostrarTabuleiroReal()
    {
        Console.WriteLine("\n[DEBUG] Tabuleiro Real:");
        Console.Write("  ");
        for (int j = 0; j < TAMANHO; j++) Console.Write(j + " ");
        Console.WriteLine();

        for (int i = 0; i < TAMANHO; i++)
        {
            Console.Write(i + " ");
            for (int j = 0; j < TAMANHO; j++)
                Console.Write(tabuleiro[i, j] + " ");
            Console.WriteLine();
        }
    }
}

