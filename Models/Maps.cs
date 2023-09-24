using Example.Repos;
namespace Example.Models
{
    public static class Maps
    {
        private class CountryDetails : ICountryDetails {
            public CountryDetails(string op, string opc)
            {
                Operator = op;
                OperatorCode = opc;
            }
            public string Operator { get; }
            public string OperatorCode { get; }
        }
        public static ICountryDetails DetailsFactory(string op, string opc)
        {
            return new CountryDetails(op, opc);
        }
        private class Country : ICountry {
            public Country(string n, string cc, string iso, IEnumerable<ICountryDetails> d)
            {
                Name = n;
                CountryCode = cc;
                CountryIso = iso;
                CountryDetails = d;
            }
            public string Name { get; }
            public string CountryCode { get; }
            public string CountryIso { get; }
            public IEnumerable<ICountryDetails> CountryDetails { get; }
        }
        public static ICountry CountryFactory(string n, string c, string i, IEnumerable<ICountryDetails> d)
        {
            return new Country(n, c, i, d);
        }
        private class Customer : ICustomer {
            public Customer(ICountry c)
            {
                Country = c;
            }
            public ICountry Country { get; }
            public string Number { get; } = "";
        }
        public static ICountryDetails Map(CountryDetailsModel from)
            => new CDMap().Map(from);
        public static CountryDetailsModel ReverseMap(ICountryDetails to)
            => new CDMap().ReverseMap(to);
        private class CDMap : IMapper<CountryDetailsModel, ICountryDetails>
        {
            public ICountryDetails Map(CountryDetailsModel from)
            {
                if (from == null) return null;
                return new CountryDetails(from.Operator, from.OperatorCode);
            }
            public CountryDetailsModel ReverseMap(ICountryDetails to)
            {
                if (to == null) return null;
                return new(){Operator = to.Operator, OperatorCode = to.OperatorCode};
            }
        }
        public static ICountry Map(CountryModel from)
            => new CMap().Map(from);
        public static CountryModel ReverseMap(ICountry to)
            => new CMap().ReverseMap(to);
        private class CMap : IMapper<CountryModel, ICountry>
        {
            private IEnumerable<ICountryDetails> BulkMap(IEnumerable<CountryDetailsModel> c)
            {
                var l = new List<ICountryDetails>();
                foreach (var item in c)
                {
                    l.Add(DetailsMapper.Map(item));
                }
                return l;
            }
            private IEnumerable<CountryDetailsModel> BulkReverseMap(IEnumerable<ICountryDetails> c)
            {
                var l = new List<CountryDetailsModel>();
                foreach (var item in c)
                {
                    l.Add(DetailsMapper.ReverseMap(item));
                }
                return l;
            }
            public ICountry Map(CountryModel from)
            {
                if (from == null) return null;
                return new Country(from.Name, from.CountryCode, from.CountryIso, BulkMap(from.CountryDetails));
            }
            public CountryModel ReverseMap(ICountry to)
            {
                if (to == null) return null;
                return new(){Name = to.Name, CountryCode = to.CountryCode, CountryIso = to.CountryIso, CountryDetails = BulkReverseMap(to.CountryDetails) };
            }
            private CDMap DetailsMapper { get; } = new();
        }
        public static ICustomer Map(CustomerModel from)
            => new CSMap().Map(from);
        public static CustomerModel ReverseMap(ICustomer to)
            => new CSMap().ReverseMap(to);
        private class CSMap : IMapper<CustomerModel, ICustomer>
        {
            public CSMap()
            {
            }
            public ICustomer Map(CustomerModel from)
            {
                if (from == null) return null;
                return new Customer(new CMap().Map(from.Country));
            }
            public CustomerModel ReverseMap(ICustomer to)
            {
                if (to == null) return null;
                return new(){Country = new CMap().ReverseMap(to.Country)};
            }
        }
    }
}