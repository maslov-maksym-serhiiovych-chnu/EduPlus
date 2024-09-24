package ua.edu.chnu.courses.exceptions;

public class CourseNotFoundByIdException extends RuntimeException {
    public CourseNotFoundByIdException(int id) {
        super("course not found by id: " + id);
    }
}
