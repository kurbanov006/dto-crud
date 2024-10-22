


using Microsoft.EntityFrameworkCore;

public class StudentService : IStudentService
{
    private readonly AppDbContext _appDbContext;
    public StudentService(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    public async Task<bool> Create(Student student)
    {
        try
        {
            if (student == null) return false;

            int maxIdStudent = (from s in _appDbContext.Students
                                orderby s.Id descending
                                select s.Id).FirstOrDefault();

            student.Id = maxIdStudent + 1;

            await _appDbContext.Students.AddAsync(student);
            await _appDbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message);
            throw;
        }
    }

    public async Task<bool> Delete(int id)
    {
        try
        {
            Student? student = await _appDbContext.Students.FindAsync(id);
            if (student == null) return false;

            _appDbContext.Students.Remove(student);
            await _appDbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message);
            throw;
        }
    }

    public async Task<IEnumerable<Student?>> GetAll()
    {
        try
        {
            return await _appDbContext.Students.ToListAsync();
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message);
            throw;
        }
    }

    public async Task<Student?> GetById(int id)
    {
        try
        {
            Student? student = await _appDbContext.Students.FindAsync(id);
            if (student == null) return null;
            return student;
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message);
            throw;
        }
    }

    public async Task<bool> Update(Student student)
    {
        try
        {
            Student? updateStudent = await _appDbContext.Students.FindAsync(student.Id);
            if (updateStudent == null) return false;
            updateStudent.FirstName = student.FirstName;
            updateStudent.LastName = student.LastName;
            updateStudent.Email = student.Email;
            updateStudent.Age = student.Age;
            await _appDbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message);
            throw;
        }
    }
}