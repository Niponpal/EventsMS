using EventsMS.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EventsMS.Repository;

public interface IStudentRegistrationRepository
{
    Task<IEnumerable<StudentRegistration>> GetAllStudentRegistrationAsync(CancellationToken cancellationToken);
    Task<StudentRegistration?> GetStudentRegistrationByIdAsync(long id, CancellationToken cancellationToken);
    Task<StudentRegistration> AddStudentRegistrationAsync(StudentRegistration  studentRegistration, CancellationToken cancellationToken);
    Task<StudentRegistration?> UpdateStudentRegistrationAsync(StudentRegistration  studentRegistration, CancellationToken cancellationToken);
    Task<StudentRegistration> DeleteStudentRegistrationAsync(long id, CancellationToken cancellationToken);
    IEnumerable<SelectListItem> Dropdown();
    Task<List<StudentRegistration>> GetMyRegistrationsAsync(string email, CancellationToken cancellationToken);
    Task<bool> IsEmailAlreadyRegistered(string email, long eventId, CancellationToken cancellationToken);

}
