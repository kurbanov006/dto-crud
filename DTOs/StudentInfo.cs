
public readonly record struct StudentCreateInfo(
    string firstName,
    string lastName,
    string email,
    int age
    );

public readonly record struct StudentUpdateInfo(
    int id,
    string firstName,
    string lastName,
    string email,
    int age
    );

public readonly record struct StudentGetInfo(
    int id,
    string firstName,
    string lastName,
    string email,
    int age
);