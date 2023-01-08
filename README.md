# What CSGAAP is
This is CSharp Graphical Auhtor Attribution Program and it is basically a rewrite of [EVLLAB's JGAAP](https://github.com/evllabs/JGAAP) in C# language.

This program is feature-incomplete and should not be used for serious text analysis.

# Pros
CSGAAP a lot faster and more memory efficient than JGAAP.

Both CSGAAP and JGAAP *should* produce roughly the same result when used with the same input.

CSGAAP has more convenient user interface than JGAAP.
# Cons
CSGAAP does not include AAAC problems to try out in the GUI

CSGAAP implementation is not complete and contais some errors, here are the list of currently non-working features:
## Canonicizers
- StripComments - Not implemented
## Classifiers
- LDA - Not implemented
- LeaveOneOutNoDistanceDriver - Not implemented
- MahalanobisDistance - Not implemented
- MarkovChainAnalysis - Not implemented
- ThinXent - Not implemented
- WEKADecisionStump - Not implemented
- WEKAJ48DecisionTree - Not implemented
- WEKALeastMedSq - Not implemented
- WEKALinearRegression - Not implemented
- WEKAMultilayerPerceptron - Not implemented
- WEKANaiveBayes - Not implemented
- WEKASMO - Not implemented
- WEKAVotedPerceptron - Not implemented
- Xent2 - Not implemented
## EventCullers
- CoefficientOfVariation - Tests failing
- IndexOfDispersion - Tests failing
- InformationGain - Tests failing
- MeanAbsoluteDeviation - Tests failing
- RangeCuller - Tests failing
- StandardDeviationCuller - Tests failing
- VarianceCuller - Tests failing
- WeightedVariance - Tests failing
## EventDrivers
- CoarsePOSTagger - Excluded: MaxentTagger does not want to initialize
- POSNGramEventDriver - Excluded: MaxentTagger does not want to initialize
- PartOfSpeechEventDriver - Excluded: MaxentTagger does not want to initialize
- DefinitionsEventDriver - Not implemented
- StanfordNamedEntityRecognizer - Not implemented
- StanfordPartOfSpeechEventDriver - Not implemented
- StanfordPartOfSpeechNGramEventDriver - Not implemented
- SuffixEventDriver - Not implemented
- SyllableTransitionEventDriver - Not implemented
- TruncatedFreqEventDriver - Not implemented
- TruncatedNamingTimeEventDriver - Not implemented
- TruncatedReactionTimeEventDriver - Not implemented
- VowelMNLetterWordEventDriver - Not implemented
- WordSyllablesEventDriver - Not implemented
- WordsBeforeAfterNamedEntities - Not implemented
