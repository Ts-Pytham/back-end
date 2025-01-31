namespace DentallApp.DataAccess;

public class AppDbContext : CustomDbContext
{
    public DbSet<GeneralTreatment> GeneralTreatments { get; set; }
    public DbSet<SpecificTreatment> SpecificTreatments { get; set; }
    public DbSet<Person> Persons { get; set; }
    public DbSet<Gender> Genders { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Dependent> Dependents { get; set; }
    public DbSet<Kinship> Kinships { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Office> Offices { get; set; }
    public DbSet<Appoinment> Appoinments { get; set; }
    public DbSet<AppoinmentStatus> AppoinmentsStatus { get; set; }
    public DbSet<EmployeeSchedule> EmployeeSchedules { get; set; }
    public DbSet<WeekDay> WeekDays { get; set; }
    public DbSet<FavoriteDentist> FavoriteDentists { get; set; }
    public DbSet<OfficeSchedule> OfficeSchedules { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddDelegateDecompiler();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new GeneralTreatmentConfiguration());
        modelBuilder.ApplyConfiguration(new SpecificTreatmentConfiguration());
        modelBuilder.ApplyConfiguration(new RoleConfiguration());
        modelBuilder.ApplyConfiguration(new GenderConfiguration());
        modelBuilder.ApplyConfiguration(new KinshipConfiguration());
        modelBuilder.ApplyConfiguration(new DependentConfiguration());
        modelBuilder.ApplyConfiguration(new OfficeConfiguration());
        modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
        modelBuilder.ApplyConfiguration(new AppoinmentStatusConfiguration());
        modelBuilder.ApplyConfiguration(new WeekDayConfiguration());
        modelBuilder.ApplyConfiguration(new EmployeeScheduleConfiguration());
        modelBuilder.ApplyConfiguration(new OfficeScheduleConfiguration());
    }
}