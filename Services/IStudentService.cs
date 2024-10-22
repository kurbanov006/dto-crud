public interface IStudentService
{
    Task<bool> Create(Student student);
    Task<bool> Update(Student student);
    Task<bool> Delete(int id);
    Task<IEnumerable<Student?>> GetAll();
    Task<Student?> GetById(int id);
}