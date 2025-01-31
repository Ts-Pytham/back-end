﻿namespace DentallApp.Features.Dependents;

public class Dependent : ModelWithSoftDelete
{
    public int UserId { get; set; }
    public User User { get; set; }
    public int PersonId { get; set; }
    public Person Person { get; set; }
    public int KinshipId { get; set; }
    public Kinship Kinship { get; set; }
}
