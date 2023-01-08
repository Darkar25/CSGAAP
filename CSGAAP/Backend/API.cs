using CSGAAP.Exceptions;
using CSGAAP.Generics;
using CSGAAP.Languages;
using CSGAAP.Util;
using Serilog;
using StructLinq;
using StructLinq.Distinct;
using StructLinq.IList;
using StructLinq.Select;
using StructLinq.Where;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace CSGAAP.Backend
{
    public class API : INotifyPropertyChanged
    {
        public static API Instance { get; } = new();
        public static API PrivateInstance => new();

        private Language? _lang;
        public Language Language
        {
            get => _lang ??= new English();
            set
            {
                _lang = value;
                PropertyChanged?.Invoke(this, new(nameof(Language)));
            }
        }
        public ObservableCollection<EventDriver> EventDrivers { get; } = new();
        public ObservableCollection<EventCuller> EventCullers { get; } = new();
        public ObservableCollection<AnalysisDriver> AnalysisDrivers { get; } = new();
        public ObservableCollection<Canonicizer> Canonicizers { get; } = new();

        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<Document> Documents { get; } = new();

        // ugly, maybe i should change this
        public WhereEnumerable<Document, 
            ListEnumerable<Document, 
                IList<Document>>,
            IListEnumerator<Document,
                IList<Document>>> UnknownDocuments => Documents.ToStructEnumerable().Where(x => !x.IsKnownAuthor, x => x);

        // ugly, maybe i should change this
        public WhereEnumerable<Document,
            ListEnumerable<Document,
                IList<Document>>,
            IListEnumerator<Document,
                IList<Document>>> KnownDocuments => Documents.ToStructEnumerable().Where(x => x.IsKnownAuthor, x => x);

        // VERY ugly, i should change this!
        public DistinctEnumerable<string,
            SelectEnumerable<Document, string,
                WhereEnumerable<Document,
                    ListEnumerable<Document,
                IList<Document>>,
            IListEnumerator<Document,
                IList<Document>>>, 
                WhereEnumerator<Document, 
                    IListEnumerator<Document,
                IList<Document>>>>, 
            SelectEnumerator<Document, string, 
                WhereEnumerator<Document, 
                    IListEnumerator<Document,
                IList<Document>>>>, 
            EqualityComparer<string>> Authors => KnownDocuments.Select(x => x.Author!, x => x).Distinct(x => x); // Change type to StructEnumerable
        
        public ILookup<string, Document> DocumentsByAuthor => KnownDocuments.ToEnumerable().ToLookup(x => x.Author!);

        public CancellationTokenSource? Cancellation { get; private set; }

        public API()
        {
            Documents.CollectionChanged += (s, a) => {
                PropertyChanged?.Invoke(this, new(nameof(UnknownDocuments)));
                PropertyChanged?.Invoke(this, new(nameof(KnownDocuments)));
                PropertyChanged?.Invoke(this, new(nameof(Authors)));
                PropertyChanged?.Invoke(this, new(nameof(DocumentsByAuthor)));
            };
        }

        public async Task LoadCanonicize()
        {
            await Parallel.ForEachAsync(Documents, new ParallelOptions()
            {
                CancellationToken = Cancellation!.Token,
                MaxDegreeOfParallelism = System.Environment.ProcessorCount
            }, async (d, ct) => {
                ct.ThrowIfCancellationRequested();
                try
                {
                    await d.Load(Language, ct);
                    d.Canonicize(Canonicizers.Where(x => ((DocumentType)x["docType"]).HasFlag(d.Type)));
                }
                catch (LanguageParsingException e)
                {
                    Log.Fatal($"Could not Parse Language: {Language.DisplayName} on File:{d.URI} Title:{d.Title}", e);
                    throw new Exception($"Could not Parse Language: {Language.DisplayName} on File:{d.URI} Title:{d.Title}", e);
                }
                catch (CanonicizationException e)
                {
                    Log.Fatal($"Could not Canonicize File:{d.URI} Title:{d.Title}", e);
                    throw new Exception($"Could not Canonicize File:{d.URI} Title:{d.Title}", e);
                }
                catch (Exception e)
                {
                    Log.Fatal($"Could not load File:{d.URI} Title:{d.Title}", e);
                    throw new Exception($"Could not load File:{d.URI} Title:{d.Title}", e);
                }
                Log.Information($"Document: {d.Title} has finished canonicizing.");
            });
        }

        public async Task Eventify()
        {
            await Parallel.ForEachAsync(Documents, new ParallelOptions()
            {
                CancellationToken = Cancellation!.Token,
                MaxDegreeOfParallelism = System.Environment.ProcessorCount
            }, async (d, ct) => {
                foreach (var ed in EventDrivers)
                {
                    ct.ThrowIfCancellationRequested();
                    try
                    {
                        d.EventSets.Add(ed, ed.CreateEventSet(d.Text));
                    }
                    catch (EventGenerationException e)
                    {
                        Log.Error($"Could not Eventify with {ed.DisplayName} on File:{d.URI} Title:{d.Title}", e);
                        throw new Exception($"Could not Eventify with {ed.DisplayName} on File:{d.URI} Title:{d.Title}", e);
                    }
                }
                Log.Information($"Document: {d.Title} has finished eventifying.");
            });
        }

        public async Task Cull()
        {
            await Parallel.ForEachAsync(EventDrivers, new ParallelOptions()
            {
                CancellationToken = Cancellation!.Token,
                MaxDegreeOfParallelism = System.Environment.ProcessorCount
            }, async (driver, ct) => {
                var events = Documents.Select(x => x.EventSets[driver]);
                foreach (var culler in EventCullers)
                {
                    ct.ThrowIfCancellationRequested();
                    var c = (EventCuller)culler.NewInstanceWithParams();
                    c.Initialize(events);
                    foreach (var doc in Documents)
                    {
                        ct.ThrowIfCancellationRequested();
                        doc.EventSets[driver] = new(c.Cull(doc.EventSets[driver]).ForceEnumerate()); // Enumerate the collection to avoid reenumerating it every time analyzer wants to access it
                        Log.Information($"Finished Culling for document {doc.Title} using {culler.DisplayName}");
                    }
                }
                Log.Information($"Finished Culling {driver.DisplayName}");
            });
        }

        public async Task Analyze()
        {
            await Parallel.ForEachAsync(AnalysisDrivers, new ParallelOptions()
            {
                CancellationToken = Cancellation!.Token,
                MaxDegreeOfParallelism = System.Environment.ProcessorCount
            }, async (driver, ct) => {
                ct.ThrowIfCancellationRequested();
                var ad = (AnalysisDriver)driver.NewInstanceWithParams();
                ad.Distance = driver.Distance;
                Log.Information($"Training {ad.DisplayName}");
                ad.Train(KnownDocuments.ToEnumerable());
                Log.Information($"Finished Training {ad.DisplayName}");
                foreach (var doc in UnknownDocuments)
                {
                    ct.ThrowIfCancellationRequested();
                    Log.Information($"Beginnig analyze on document: {doc}");
                    doc.Results[ad] = ad.Analyze(doc);
                    Log.Information($"Analysis finished for document: {doc}");
                }
                Log.Information($"Analysis completed for driver: {ad.DisplayName}");
            });
        }

        public void Reset()
        {
            foreach (var doc in Documents)
            {
                doc.EventSets.Clear();
                doc.Results.Clear();
            }
        }

        public async Task<bool> Execute()
        {
            if (Cancellation is not null) throw new InvalidOperationException("There is already another process running on this instance.");
            Cancellation = new();
            try
            {
                Reset();
                await LoadCanonicize();
                await Eventify();
                await Cull();
                await Analyze();
                return true;
            }
            catch (OperationCanceledException)
            {
                return false;
            }
            finally
            {
                Cancellation = null;
            }
        }

        public string FormattedResult()
        {
            var sb = new StringBuilder();
            foreach (var doc in UnknownDocuments.Where(x => x.Results.Count > 0, x => x))
            {
                foreach (var ae in doc.Results.Keys)
                {
                    if (!doc.Results[ae].Any()) continue;
                    sb.AppendLine($"{doc.Title} ");
                    sb.AppendLine("Canonicizers: ");
                    if (Canonicizers.Count == 0) sb.AppendLine("    none");
                    else foreach (var canon in Canonicizers)
                            sb.AppendLine($"    {canon.DisplayName}; {canon.FormattedParameters}");
                    sb.AppendLine("EventDrivers: ");
                    foreach (var ed in doc.EventSets.Keys)
                        sb.AppendLine($"    {ed.DisplayName}; {ed.FormattedParameters}");
                    sb.AppendLine("EventCullers: ");
                    if (EventCullers.Count == 0) sb.AppendLine("    none");
                    else foreach (var ec in EventCullers)
                            sb.AppendLine($"    {ec.DisplayName}; {ec.FormattedParameters}");
                    sb.AppendLine("Analysis: ");
                    sb.AppendLine($"    {ae.DisplayName}; {ae.FormattedParameters}");
                    sb.AppendLine();
                    int count = 0; // Keeps a relative count (adjusted for ties)
                    int fullCount = 0; // Keeps the absolute count (does not count ties)
                    double lastResult = double.NaN;
                    foreach (var res in doc.Results[ae])
                    {
                        fullCount++;
                        if (res.Value != lastResult) count = fullCount;
                        lastResult = res.Value;
                        sb.AppendLine($"{count}. {res.Key} {res.Value}");
                    }
                    sb.AppendLine();
                    sb.AppendLine();
                }
            }
            return sb.ToString();
        }
    }
}