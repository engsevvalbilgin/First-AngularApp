using System.Data.Entity;
using System.Data.SqlClient;
using System.Threading.Tasks;


namespace StajIlkProje.Models
{
    public class AppDBContext:DbContext
    {
        
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Activity> Activities { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Product> Products { get; set; }

        public async Task<Task<int>> GetOpenDuration(int activityId)
        {
            var result = Database.SqlQuery<int>(
                "GetOpenDuration @ActivityId",
                new SqlParameter("@ActivityId", activityId)
            ).SingleOrDefaultAsync();

            return result;
        }

    }
}