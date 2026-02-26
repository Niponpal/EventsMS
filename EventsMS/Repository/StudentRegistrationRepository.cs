using EventsMS.Data;
using EventsMS.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EventsMS.Repository;

public class StudentRegistrationRepository : IStudentRegistrationRepository
{
    private readonly ApplicationDbContext _context;
    public StudentRegistrationRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<StudentRegistration> AddStudentRegistrationAsync(StudentRegistration studentRegistration, CancellationToken cancellationToken)
    {
        var data = await _context.studentRegistrations.AddAsync(studentRegistration, cancellationToken);
        if (data != null)
        {
            await _context.SaveChangesAsync(cancellationToken);
            return studentRegistration; // PhotoPath included
        }
        return null;
    }

    public async Task<StudentRegistration> DeleteStudentRegistrationAsync(long id, CancellationToken cancellationToken)
    {
        var data= await _context.studentRegistrations.FindAsync(id, cancellationToken);
        if (data != null)
        {
            _context.studentRegistrations.Remove(data);
            await _context.SaveChangesAsync(cancellationToken);
            return data;
        }
        return null;
    }

    public IEnumerable<SelectListItem> Dropdown()
    {
      var data = _context.studentRegistrations.Select(x => new SelectListItem
        {
            Text = x.FullName,
            Value = x.Id.ToString()
        }).ToList();
        return data;
    }

    public async Task<IEnumerable<StudentRegistration>> GetAllStudentRegistrationAsync(CancellationToken cancellationToken)
    {
       var data = await _context.studentRegistrations.ToListAsync(cancellationToken);
        if (data != null)
        {
            return data;
        }
        return null;
    }

    public async Task<StudentRegistration?> GetStudentRegistrationByIdAsync(long id, CancellationToken cancellationToken)
    {
       var data = await  _context.studentRegistrations.FindAsync(id, cancellationToken);
        if (data != null)
        {
            return data;
        }
        return null;
    }

    public async Task<StudentRegistration?> UpdateStudentRegistrationAsync(StudentRegistration studentRegistration, CancellationToken cancellationToken)
    {
       var data = await _context.studentRegistrations.FindAsync(studentRegistration.Id, cancellationToken);
        if (data != null)
        {
            data.FullName = studentRegistration.FullName;
            data.PhoneNumber = studentRegistration.PhoneNumber;
            data.Email = studentRegistration.Email;
            data.IdCardNumber = studentRegistration.IdCardNumber;
            data.Department = studentRegistration.Department;
            data.PhotoPath = studentRegistration.PhotoPath;
            data.PaymentStatus = studentRegistration.PaymentStatus;
            data.EventId = studentRegistration.EventId;
            data.UserId = studentRegistration.UserId;
            _context.studentRegistrations.Update(data);
            await _context.SaveChangesAsync(cancellationToken);
            return data;
        }
        return null;
    }

    

}
