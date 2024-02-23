class NeedlemanWunsch
{
    public NeedlemanWunsch(){

    }
    public static AlignmentResultModel AlignSequences(string seq1, string seq2, int match = 1, int mismatch = -1, int gap = -1)
    {
        int rows = seq1.Length + 1;
        int cols = seq2.Length + 1;

        // Inicialização da matriz de pontuações
        int[,] scoreMatrix = new int[rows, cols];

        // Inicialização da matriz de direções
        int[,] directionMatrix = new int[rows, cols];

        // Inicialização da primeira coluna
        for (int i = 1; i < rows; i++)
        {
            scoreMatrix[i, 0] = gap * i;
        }

        // Inicialização da primeira linha
        for (int j = 1; j < cols; j++)
        {
            scoreMatrix[0, j] = gap * j;
        }

        // Preenchimento da matriz de pontuações
        for (int i = 1; i < rows; i++)
        {
            for (int j = 1; j < cols; j++)
            {
                int diagonalScore = seq1[i - 1] == seq2[j - 1] ? scoreMatrix[i - 1, j - 1] + match : scoreMatrix[i - 1, j - 1] + mismatch;
                int upScore = scoreMatrix[i - 1, j] + gap;
                int leftScore = scoreMatrix[i, j - 1] + gap;

                // Escolha do máximo entre os três possíveis movimentos
                int maxScore = Math.Max(diagonalScore, Math.Max(upScore, leftScore));

                // Atualização da matriz de pontuações
                scoreMatrix[i, j] = maxScore;

                // Atualização da matriz de direções
                if (maxScore == diagonalScore)
                {
                    directionMatrix[i, j] = 1; // Diagonal
                }
                else if (maxScore == upScore)
                {
                    directionMatrix[i, j] = 2; // Cima
                }
                else
                {
                    directionMatrix[i, j] = 3; // Esquerda
                }
            }
        }

        // Alinhamento reverso
        string alignedSeq1 = "";
        string alignedSeq2 = "";
        int x = rows - 1;
        int y = cols - 1;
        while (x > 0 || y > 0)
        {
            if (directionMatrix[x, y] == 1) // Diagonal
            {
                alignedSeq1 = seq1[x - 1] + alignedSeq1;
                alignedSeq2 = seq2[y - 1] + alignedSeq2;
                x--;
                y--;
            }
            else if (directionMatrix[x, y] == 2) // Cima
            {
                alignedSeq1 = seq1[x - 1] + alignedSeq1;
                alignedSeq2 = "-" + alignedSeq2;
                x--;
            }
            else // Esquerda
            {
                alignedSeq1 = "-" + alignedSeq1;
                alignedSeq2 = seq2[y - 1] + alignedSeq2;
                y--;
            }
        }
        AlignmentResultModel result = new ();
        int matches = 0;
        int gaps = 0;
        for(int i = 0; i < alignedSeq1.Length; i++){
            if(alignedSeq1[i] == alignedSeq2[i]){
                matches++;
            }else if(alignedSeq2[i] == '-'){
                gaps++;
            }
        }
        result.gaps = gaps;
        result.matches = matches;
        result.seq1 = alignedSeq1;
        result.seq2 = alignedSeq2;
        result.similarity = matches * 100.0 / alignedSeq1.Length;

        return result;
    }
}