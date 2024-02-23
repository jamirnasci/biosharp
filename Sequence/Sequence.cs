using System;
using System.IO;
class Sequence
{
    public string StringSequence { get; set; }
    public string? Header { get; set; }

    public Sequence()
    {

    }
    public Sequence(string FilePath)
    {
        try
        {
            string lines = "";
            int headersCount = 0;
            using (var reader = new StreamReader(FilePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line[0] == '>')
                    {
                        if (headersCount > 1)
                        {
                            break;
                        }
                        headersCount++;
                        Header = line;
                    }
                    else
                    {
                        lines += line;
                    }
                }

            }
            StringSequence = lines;
        }
        catch (IOException e)
        {
            Console.WriteLine(e.StackTrace);
        }
    }
    public int GetLength()
    {
        return StringSequence.Length;
    }
    public int Count(string chunk)
    {
        int matches = 0;
        for (int i = 0; i <= StringSequence.Length - chunk.Length; i++)
        {
            string stringSequenceChunk = StringSequence.Substring(i, chunk.Length);
            if (stringSequenceChunk.Equals(chunk))
            {
                matches++;
            }
        }
        return matches;
    }
}