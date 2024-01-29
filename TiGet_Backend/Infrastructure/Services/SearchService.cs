using Domain.Entities;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.NGram;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Document = Lucene.Net.Documents.Document;

namespace Infrastructure.Services
{
    public class TicketSearchService
    {
        private const string LuceneDirectory = "LuceneIndex";
        private static FSDirectory _directory;

        static TicketSearchService()
        {
            if (!System.IO.Directory.Exists(LuceneDirectory))
                System.IO.Directory.CreateDirectory(LuceneDirectory);

            _directory = FSDirectory.Open(new DirectoryInfo(LuceneDirectory));
        }

        public static void AddTicketToIndex(Ticket ticket)
        {
            using var writer = new IndexWriter(_directory, new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30), IndexWriter.MaxFieldLength.UNLIMITED);

            var doc = new Document();
            doc.Add(new Field("Id", ticket.Id.ToString(), Field.Store.YES, Field.Index.ANALYZED));
            doc.Add(new Field("TimeToGo", ticket.TimeToGo.ToString(), Field.Store.YES, Field.Index.ANALYZED));
            doc.Add(new Field("SourceId", ticket.SourceId.ToString(), Field.Store.YES, Field.Index.ANALYZED));
            doc.Add(new Field("DestId", ticket.DestinationId.ToString(), Field.Store.YES, Field.Index.ANALYZED));
            doc.Add(new Field("VehicleId", ticket.VehicleId.ToString(), Field.Store.YES, Field.Index.ANALYZED));
            doc.Add(new Field("Price", ticket.Price.ToString(), Field.Store.YES, Field.Index.ANALYZED));
            doc.Add(new Field("Source", ticket.Source.Name, Field.Store.YES, Field.Index.ANALYZED));
            doc.Add(new Field("Destination", ticket.Destination.Name, Field.Store.YES, Field.Index.ANALYZED));
            doc.Add(new Field("VehicleType", ticket.Vehicle.Name.ToString(), Field.Store.YES, Field.Index.ANALYZED));

            writer.AddDocument(doc);
            writer.Optimize();
            writer.Dispose();
        }


        /*public IEnumerable<Ticket> SearchTickets(string source = null, string destination = null, DateTime? timeToGo = null, decimal? price = null)
        {
            using var searcher = new IndexSearcher(_directory, true);
            var booleanQuery = new BooleanQuery();

            if (!string.IsNullOrEmpty(source))
            {
                var sourceQuery = new TermQuery(new Term("Source", source));
                booleanQuery.Add(sourceQuery, Occur.MUST);
            }

            if (!string.IsNullOrEmpty(destination))
            {
                var destinationQuery = new TermQuery(new Term("Destination", destination));
                booleanQuery.Add(destinationQuery, Occur.MUST);
            }

            if (timeToGo.HasValue)
            {
                var timeToGoQuery = NumericRangeQuery.NewLongRange("TimeToGo", timeToGo.Value.Ticks, timeToGo.Value.Ticks, true, true);
                booleanQuery.Add(timeToGoQuery, Occur.MUST);
            }

            if (price.HasValue)
            {
                var priceQuery = NumericRangeQuery.NewDoubleRange("Price", (double)price.Value, (double)price.Value, true, true);
                booleanQuery.Add(priceQuery, Occur.MUST);
            }

            var hits = searcher.Search(booleanQuery, 10).ScoreDocs;

            foreach (var hit in hits)
            {
                var document = searcher.Doc(hit.Doc);
                yield return new Ticket
                {
                    Id = Guid.Parse(document.Get("Id")),
                    TimeToGo = DateTime.Parse(document.Get("TimeToGo")),
                    Price = (double)decimal.Parse(document.Get("Price")),
                    Source = document.Get("Source"),
                    SourceId = Guid.Parse(document.Get("SourceId")),
                    Destination = document.Get("Destination"),
                    DestinationId = Guid.Parse(document.Get("DestId")),
                };
            }
        }*/
        public IEnumerable<string> GetSuggestedCities(string input)
        {
            using var searcher = new IndexSearcher(_directory, true);
            var ngramAnalyzer = new NGramAnalyzer();
            var queryParser = new QueryParser(Lucene.Net.Util.Version.LUCENE_30, "Source", ngramAnalyzer);

            var sourceQuery = queryParser.Parse($"{input}~1");

            var hits = searcher.Search(sourceQuery, 10).ScoreDocs;

            var suggestedCities = hits.Select(hit =>
            {
                var document = searcher.Doc(hit.Doc);
                return document.Get("Source");
            });

            return suggestedCities;
        }

        public IEnumerable<string> GetSuggestedDestinations(string input)
        {
            using var searcher = new IndexSearcher(_directory, true);
            var ngramAnalyzer = new NGramAnalyzer();
            var queryParser = new QueryParser(Lucene.Net.Util.Version.LUCENE_30, "Destination", ngramAnalyzer);

            // Set min and max n-gram lengths to create front edge n-grams
            var destinationQuery = queryParser.Parse($"{input}~1");

            var hits = searcher.Search(destinationQuery, 10).ScoreDocs;

            var suggestedDestinations = hits.Select(hit =>
            {
                var document = searcher.Doc(hit.Doc);
                return document.Get("Destination");
            });

            return suggestedDestinations;
        }

        public IEnumerable<string> GetSuggestedVehicleTypes(string input)
        {
            using var searcher = new IndexSearcher(_directory, true);
            var ngramAnalyzer = new NGramAnalyzer();
            var queryParser = new QueryParser(Lucene.Net.Util.Version.LUCENE_30, "VehicleType", ngramAnalyzer);

            var vehicleTypeQuery = queryParser.Parse($"{input}~1");

            var hits = searcher.Search(vehicleTypeQuery, 10).ScoreDocs;

            var suggestedVehicleTypes = hits.Select(hit =>
            {
                var document = searcher.Doc(hit.Doc);
                return document.Get("VehicleType");
            });

            return suggestedVehicleTypes;
        }





    }
    public class NGramAnalyzer : Analyzer
    {
        public override TokenStream TokenStream(string fieldName, TextReader reader)
        {
            TokenStream tokenStream = new StandardTokenizer(Lucene.Net.Util.Version.LUCENE_30, reader);
            tokenStream = new StandardFilter(tokenStream);
            tokenStream = new LowerCaseFilter(tokenStream);
            tokenStream = new NGramTokenFilter(tokenStream, 1, 5);
            return tokenStream;
        }
    }
    /*public class EdgeNGramAnalyzer : Analyzer
    {
        private readonly int _minGram;
        private readonly int _maxGram;

        public EdgeNGramAnalyzer(int minGram, int maxGram)
        {
            _minGram = minGram;
            _maxGram = maxGram;
        }

        public override TokenStream TokenStream(string fieldName, TextReader reader)
        {
            TokenStream tokenStream = new StandardTokenizer(Lucene.Net.Util.Version.LUCENE_30, reader);
            tokenStream = new StandardFilter(tokenStream);
            tokenStream = new LowerCaseFilter(tokenStream);
            tokenStream = new EdgeNGramTokenFilter(tokenStream, _minGram, _maxGram);
            return tokenStream;
        }
    }*/

}