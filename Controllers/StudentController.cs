

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/api/students/")]
public class StudentController : ControllerBase
{
    private readonly IStudentService _studentService;
    public StudentController(IStudentService studentService)
    {
        _studentService = studentService;
    }
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] StudentCreateInfo studentCreateInfo)
    {
        Student student = new Student()
        {
            FirstName = studentCreateInfo.firstName,
            LastName = studentCreateInfo.lastName,
            Email = studentCreateInfo.email,
            Age = studentCreateInfo.age
        };
        if (student == null)
            return BadRequest();

        bool res = await _studentService.Create(student);

        if (res == false)
            return BadRequest(ApiResponse<bool>.Fail(null, res));

        return Ok(ApiResponse<bool>.Success(null, res));
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update([FromBody] StudentUpdateInfo studentUpdateInfo)
    {
        Student student = new Student
        {
            Id = studentUpdateInfo.id,
            FirstName = studentUpdateInfo.firstName,
            LastName = studentUpdateInfo.lastName,
            Email = studentUpdateInfo.email,
            Age = studentUpdateInfo.age
        };

        bool res = await _studentService.Update(student);

        if (res == false)
            return BadRequest(ApiResponse<bool>.Fail(null, res));

        return Ok(ApiResponse<bool>.Success(null, res));
    }


    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        Student? student = await _studentService.GetById(id);
        if (student == null)
            return NotFound(ApiResponse<Student>.Fail(null, student));

        StudentGetInfo studentGetInfo = new StudentGetInfo
        {
            id = student!.Id,
            firstName = student.FirstName,
            lastName = student.LastName,
            email = student.Email,
            age = student.Age
        };

        return Ok(ApiResponse<StudentGetInfo>.Success(null, studentGetInfo));
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAll()
    {
        IEnumerable<Student?> students = await _studentService.GetAll();
        if (students == null)
            return NotFound(ApiResponse<IEnumerable<Student?>>.Fail(null, students));

        IEnumerable<StudentGetInfo> studentGetInfos = from s in students
                                                      select new StudentGetInfo
                                                      {
                                                          id = s.Id,
                                                          firstName = s.FirstName,
                                                          lastName = s.LastName,
                                                          email = s.Email,
                                                          age = s.Age
                                                      };
        return Ok(ApiResponse<IEnumerable<StudentGetInfo>>.Success(null, studentGetInfos));
    }


    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        bool res = await _studentService.Delete(id);
        if (res == false)
            return BadRequest(ApiResponse<bool>.Fail(null, res));

        return Ok(ApiResponse<bool>.Success(null, res));
    }
}