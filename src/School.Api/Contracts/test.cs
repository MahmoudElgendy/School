namespace School.Api.Contracts
{
    public record CreateStudentRequest(
    string FirstName,
    string LastName,
    string Email,
    DateTime DateOfBirth
);

    public record UpdateStudentRequest(
        string FirstName,
        string LastName,
        string Email,
        DateTime DateOfBirth
    );

    public record StudentResponse(
        Guid Id,
        string FirstName,
        string LastName,
        string Email,
        DateTime DateOfBirth
    );
}
