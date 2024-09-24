package ua.edu.chnu.courses.handlers;

import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.ControllerAdvice;
import org.springframework.web.bind.annotation.ExceptionHandler;
import ua.edu.chnu.courses.exceptions.CourseNotFoundByIdException;

import java.util.HashMap;
import java.util.Map;

@ControllerAdvice
public class CourseExceptionHandler {
    @ExceptionHandler(CourseNotFoundByIdException.class)
    public ResponseEntity<Map<String, String>> handleCourseNotFoundByIdException(CourseNotFoundByIdException exception) {
        var errors = new HashMap<String, String>();
        errors.put("error", exception.getMessage());

        return new ResponseEntity<>(errors, HttpStatus.NOT_FOUND);
    }
}
