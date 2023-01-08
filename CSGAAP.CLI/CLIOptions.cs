using CommandLine;

namespace CSGAAP.CLI
{
    public class CLIOptions
    {
        [Option('c', "canonicizers", SetName = "default", Separator = ' ', HelpText = "A list of the canonicizers to use")]
        public required IEnumerable<string> Canonicizers { get; init; }

        // Cant use "es" as shortname, instead grab the letter eventseT
        [Option('t', "eventset", SetName = "default", Required = true, HelpText = "The method of dividing the document into quantifyable features")]
        public required string EventDriver { get; init; }

        // Cant use "ec" as shortname, instead grab the letter eventcUllers
        [Option('u', "eventcullers", SetName = "default", Separator = ' ', HelpText = "A list of the EventCullers to use")]
        public required IEnumerable<string> EventCullers { get; init; }

        [Option('a', "analysis", SetName = "default", Required = true, HelpText = "Method of Statistical analysis of the document")]
        public required string AnalysisDriver { get; init; }

        [Option('d', "distance", SetName = "default", HelpText = "A method of quantifying distance between documents, required for some analysis methods")]
        public string? DistanceFunction { get; init; }

        // Cant use "lang" as shortname, instead grab the letter laNguage
        [Option('n', "language", Default = "english", HelpText = "The language the working documents are in, also set the charset files will be read in")]
        public required string Language { get; init; }

        [Option('l', "load", SetName = "default", Required = true, HelpText = "A csv file of the documents to operate on. The file use the columns Author,FilePath,Title")]
        public required string LoadFile { get; init; }

        [Option('s', "save", SetName = "default", HelpText = "Write JGAAP's output to a specified file")]
        public string? SaveFile { get; init; }

        // Cant use "ee" as shortname, instead grab the letter Experimentengine
        [Option('e', "experimentengine", SetName = "experimentengine", HelpText = "Batch processing, pass in a csv file of experiments")]
        public string? ExperimentEngine { get; init; }
    }
}