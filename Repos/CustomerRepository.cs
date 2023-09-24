
using Example.Data;
using Example.Models;
using Microsoft.EntityFrameworkCore;

namespace Example.Repos
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CustomerContext _context;
        private int cid = 0;
        private int did = 0;
        // public CustomerRepository(CustomerContext context)
        // static CustomerRepository()
        // {}
        public CustomerRepository()
        {
            try{
                _context = new();
                var d = new ICountryDetails[] {
                    Maps.DetailsFactory("MTN Nigeria", "MTN NG"),
                    Maps.DetailsFactory("Airtel Nigeria", "ANG"),
                    Maps.DetailsFactory("9 Mobile Nigeria", "ECN"),
                    Maps.DetailsFactory("Globacom Nigeria", "GLO NG"),
                };
                AddCountry(Maps.CountryFactory("Nigeria", "234", "NG", d));
                d = new ICountryDetails[] {
                    Maps.DetailsFactory("Vodafone Ghana", "Vodafone GH"),
                    Maps.DetailsFactory("MTN Ghana", "MTN Ghana"),
                    Maps.DetailsFactory("Tigo Ghana", "Tigo Ghana")
                };
                AddCountry(Maps.CountryFactory("Ghana", "233", "GH", d));
                d = new ICountryDetails[] {
                    Maps.DetailsFactory("MTN Benin", "MTN Benin"),
                    Maps.DetailsFactory("Moov Benin", "Moov Benin")
                };
                AddCountry(Maps.CountryFactory("Benin", "229", "BN", d));
                d = new ICountryDetails[] {
                    Maps.DetailsFactory("MTN Côte d'Ivoire", "MTN CIV")
                };
                AddCountry(Maps.CountryFactory("Côte d'Ivoire", "225", "CIV", d));
                SaveChanges();
            }catch(System.Exception)
            {}
        }
        ~CustomerRepository(){
            _context.Dispose();
        }

        public bool SaveChanges()
        {
            try
            {
                _context.SaveChanges();
                return true;
            } catch (InvalidOperationException) { throw; }
            catch (DbUpdateConcurrencyException) { return default; }
            catch (DbUpdateException) { return default; }
        }
    
        public async Task<IEnumerable<ICustomer>> GetCustomers(int interval, int rangePerInterval)
        {
            var res = await _context.Customers
                // .Include(x => x.Number)
                .Include(x => x.Country)
                // .ThenInclude(x => x.CountryCode)
                // .ThenInclude(x => x.CountryIso)
                // .ThenInclude(x => x.Name)
                .ThenInclude(x => x.CountryDetails)
                // .OrderBy(x => )
                .Skip(interval * rangePerInterval)
                .Take(rangePerInterval)
                .Select(x => Maps.Map(x))
                .ToListAsync();
            return res;
        }
        public async Task<IEnumerable<ICustomer>> GetCustomers(ICountry ctry, int interval, int rangePerInterval)
        {
            var res = await _context.Customers
                // .Include(x => x.Number)
                .Where(x => x.Country.Equals(Maps.ReverseMap(ctry)))
                .Include(x => x.Country)
                .ThenInclude(x => x.CountryDetails)
                // .OrderBy(x => )
                .Skip(interval * rangePerInterval)
                .Take(rangePerInterval)
                .Select(x => Maps.Map(x))
                .ToListAsync();
            return res;
        }
        private string[] Validate(string? phone)
        {
            if(phone == null || phone.Length < 11 || phone.Length > 14) return new string[]{};
            var digits = "0123456789";
            string[] valid = {"", "", ""};
            var index = 2;
            for(int i = phone.Length - 1; i >= 0; i--){
                var c = phone[i];
                if(digits.IndexOf(c) < 0) return new string[]{};
                else if(i == 5) index--;
                else if(i == 2) index--;
                valid[index] = c + valid[index];
            }
            return valid;
        }
        public async Task<object> GetCustomer(string? phone)
        {
            var v = Validate(phone);
            // foreach (var item in v)
            // {
            //     Console.WriteLine(item);
            // }
            if(v.Length == 0) return Task.CompletedTask;
            try
            {
                // Console.WriteLine("In try catch");
                var res = await _context.Countries
                    .Where(x => x.CountryCode == v[0])
                    .Include(x => x.CountryDetails)
                    .Select(x => new {
                        Number = phone,
                        Country = Maps.Map(x)
                    })
                    .FirstOrDefaultAsync();
                // Console.WriteLine("Completed");
                return res;
            } catch (InvalidOperationException) { throw; }
            catch (DbUpdateConcurrencyException) { return default; }
            catch (DbUpdateException) { return default; }
        }

        public async Task<bool> AddCountry(ICountry @new) {
            var model = Maps.ReverseMap(@new);
            model.Id = ++cid;
            foreach (var item in model.CountryDetails)
            {
                item.Id = ++did;
                item.CountryId = model.Id;
            }
            try
            {
                await _context.Countries
                .AddAsync(model);
            } catch (InvalidOperationException) { throw; }
            catch (DbUpdateConcurrencyException) { return default; }
            catch (DbUpdateException) { return default; }
            // Console.WriteLine($"Completed AddCountry {model.Id}");
            return true;
        }

    }
}