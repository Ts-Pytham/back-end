﻿namespace DentallApp.Features.GeneralTreatments.DTOs;

public class GeneralTreatmentInsertDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    [Required]
    [Image]
    public IFormFile Image { get; set; }
    public int Duration { get; set; }
}
