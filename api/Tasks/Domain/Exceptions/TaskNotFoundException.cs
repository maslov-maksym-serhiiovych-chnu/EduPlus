namespace Domain.Exceptions;

public class TaskNotFoundException(string message) : Exception(message);