package ua.edu.chnu.courses_api_java.courses;

public class CourseNotFoundException extends RuntimeException {
    public CourseNotFoundException(int id) {
        super("course not found by id: " + id);
    }
}