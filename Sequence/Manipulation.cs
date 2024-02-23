class Manipulation
{
    public Manipulation()
    {
    }
    public double GetGcContent(Sequence sequence)
    {
        int GC = sequence.Count("GC");
        return GC * 100 / sequence.GetLength();
    }
    public string Reverse(Sequence sequence)
    {
        string reversedSequence = "";
        for (int i = 0; i < sequence.GetLength(); i++)
        {
            reversedSequence = sequence.StringSequence.Substring(i, 1) + reversedSequence;
        }
        return reversedSequence;
    }
    public string Complement(Sequence sequence)
    {
        string complementSequence = "";

        for (int i = 0; i < sequence.GetLength(); i++)
        {
            switch (sequence.StringSequence.Substring(i, 1))
            {
                case "A":
                    complementSequence += "T";
                    break;
                case "T":
                    complementSequence += "A";
                    break;
                case "G":
                    complementSequence += "G";
                    break;
                case "C":
                    complementSequence += "C";
                    break;
                default:
                    break;
            }
        }
        return complementSequence;
    }
    public string ReverseComplement(Sequence sequence)
    {
        string reversed = Reverse(sequence);
        sequence.StringSequence = reversed;
        return Complement(sequence);
    }
    public string FastaFormat(Sequence sequence){
        string formated = sequence.Header+"\n";
        int limit = 0;
        for(int i = 0; i < sequence.GetLength(); i++){
            if(limit == 80){
                formated += "\n";
                limit = 0;
            }
            formated += sequence.StringSequence.Substring(i, 1);
            limit++;
        }
        return formated;
    }
}