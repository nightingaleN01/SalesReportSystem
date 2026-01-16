using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Interfaces;
using System.Text;
using Entities;

namespace Services
{
    public class CsvReaderService : ICSVReader
    {
        public IEnumerable<SalesReportDto> ReadFile(Stream stream)
        {
            var result = new List<SalesReportDto>();

            using var reader = new StreamReader(stream);

            // Read header
            var headerLine = reader.ReadLine();
            if (string.IsNullOrWhiteSpace(headerLine))
                return result;

            var headers = SplitCsvLine(headerLine);

            // Build header index map
            var headerMap = headers
                .Select((h, i) => new { Header = h.Trim(), Index = i })
                .ToDictionary(x => x.Header, x => x.Index);

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                var columns = SplitCsvLine(line);

                var row = new SalesReportDto
                {
                    ProductName = Get(columns, headerMap, "Product Name"),
                    CategoryName = Get(columns, headerMap, "Category"),
                    Region = Get(columns, headerMap, "Region"),

                    DateOfSale = DateTime.Parse(Get(columns, headerMap, "Date of Sale")),
                    QuantitySold = int.Parse(Get(columns, headerMap, "Quantity Sold")),
                    UnitPrice = decimal.Parse(Get(columns, headerMap, "Unit Price")),
                    Discount = decimal.Parse(Get(columns, headerMap, "Discount")),
                    ShippingCost = decimal.Parse(Get(columns, headerMap, "Shipping Cost")),

                    PaymentMethod = Get(columns, headerMap, "Payment Method"),

                    CustomerName = Get(columns, headerMap, "Customer Name"),
                    CustomerEmail = Get(columns, headerMap, "Customer Email"),
                    CustomerAddress = Get(columns, headerMap, "Customer Address")
                };

                result.Add(row);
            }

            return result;
        }

        

        private static string Get(
            List<string> columns,
            Dictionary<string, int> map,
            string header)
        {
            return map.ContainsKey(header)
                ? columns[map[header]].Trim('"')
                : string.Empty;
        }

        private static List<string> SplitCsvLine(string line)
        {
            var values = new List<string>();
            var current = new StringBuilder();
            bool inQuotes = false;

            foreach (char c in line)
            {
                if (c == '"')
                {
                    inQuotes = !inQuotes;
                    continue;
                }

                if (c == ',' && !inQuotes)
                {
                    values.Add(current.ToString());
                    current.Clear();
                }
                else
                {
                    current.Append(c);
                }
            }

            values.Add(current.ToString());
            return values;
        }
    }

}
