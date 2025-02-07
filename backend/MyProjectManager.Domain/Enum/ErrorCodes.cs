namespace MyProjectManager.Domain.Enum;

public enum ErrorCodes
{
    // 0 - 10 => Project
    ProjectsNotFound = 0,
    ProjectNotFound = 1,
    ProjectAlreadyExists = 2,

    // 11 - 20 => Task
    TaskNotFound = 11,
    TaskAlreadyExists = 12,

    // 21 - 30 => User
    UserNotFound = 21,
    UserAlreadyExists = 22,

    // 31 - 40 => Registration
    PasswordNotEqualsPasswordConfirm = 31,
    PasswordIsWrong = 32,

    // other
    InternalServerError = 41,
}
