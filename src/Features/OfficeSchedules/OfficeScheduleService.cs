using DentallApp.Features.WeekDays;

namespace DentallApp.Features.OfficeSchedules;

public class OfficeScheduleService : IOfficeScheduleService
{
    private readonly IOfficeScheduleRepository _officeScheduleRepository;

    public OfficeScheduleService(IOfficeScheduleRepository officeScheduleRepository)
    {
        _officeScheduleRepository = officeScheduleRepository;
    }

    public async Task<Response> CreateOfficeScheduleAsync(ClaimsPrincipal currentEmployee, OfficeScheduleInsertDto officeScheduleInsertDto)
    {
        if (currentEmployee.IsAdmin() && currentEmployee.IsNotInOffice(officeScheduleInsertDto.OfficeId))
            return new Response(OfficeNotAssignedMessage);

        _officeScheduleRepository.Insert(officeScheduleInsertDto.MapToOfficeSchedule());
        await _officeScheduleRepository.SaveAsync();

        return new Response
        {
            Success = true,
            Message = CreateResourceMessage
        };
    }

    public async Task<IEnumerable<OfficeScheduleGetAllDto>> GetAllOfficeSchedulesAsync()
    { 
        var offices = await _officeScheduleRepository.GetAllOfficeSchedulesAsync() as List<OfficeScheduleGetAllDto>;
        foreach (var office in offices)
            office.Schedules = AddMissingSchedules(office.Schedules);
        return offices;
    }

    public async Task<IEnumerable<OfficeScheduleShowDto>> GetHomePageSchedulesAsync()
    { 
        var offices = await _officeScheduleRepository.GetHomePageSchedulesAsync() as List<OfficeScheduleShowDto>;
        foreach (var office in offices)
            office.Schedules = AddMissingSchedules(office.Schedules, OfficeClosedMessage);
        return offices;
    }

    /// <summary>
    /// Agrega los horarios que faltan.
    /// </summary>
    /// <param name="schedules">Los horarios actuales.</param>
    /// <param name="notAvailable">Un valor opcional que indica sí el horario no está disponible.</param>
    private List<OfficeScheduleDto> AddMissingSchedules(List<OfficeScheduleDto> schedules, string notAvailable = NotAvailableMessage)
    {
        if (schedules.Count() == WeekDaysType.MaxWeekDay)
            return schedules;

        var consult = (from weekDay in WeekDaysType.WeekDays
                      orderby weekDay.Key
                      where (from schedule in schedules
                             where schedule.WeekDayId == weekDay.Key
                             select schedule) is null

                      select new OfficeScheduleDto
                      {
                          WeekDayId = weekDay.Key,
                          WeekDayName = weekDay.Value,
                          Schedule = notAvailable
                      }).ToList();

        return consult;
    }

    public async Task<IEnumerable<OfficeScheduleGetDto>> GetOfficeScheduleByOfficeIdAsync(int officeId)
        => await _officeScheduleRepository.GetOfficeScheduleByOfficeIdAsync(officeId);

    public async Task<Response> UpdateOfficeScheduleAsync(int scheduleId, ClaimsPrincipal currentEmployee, OfficeScheduleUpdateDto officeScheduleUpdateDto)
    {
        var officeSchedule = await _officeScheduleRepository.GetOfficeScheduleByIdAsync(scheduleId);
        if (officeSchedule is null)
            return new Response(ResourceNotFoundMessage);

        if (currentEmployee.IsAdmin() && currentEmployee.IsNotInOffice(officeSchedule.OfficeId))
            return new Response(OfficeNotAssignedMessage);

        officeScheduleUpdateDto.MapToOfficeSchedule(officeSchedule);
        await _officeScheduleRepository.SaveAsync();

        return new Response
        {
            Success = true,
            Message = UpdateResourceMessage
        };
    }
}
