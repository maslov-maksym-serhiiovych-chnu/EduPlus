package ua.edu.chnu.courses_api_java.courses;

import lombok.extern.slf4j.Slf4j;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.ControllerAdvice;
import org.springframework.web.bind.annotation.ExceptionHandler;

@Slf4j
@ControllerAdvice
public class CourseExceptionHandler {
    @ExceptionHandler(CourseNotFoundException.class)
    public ResponseEntity<String> handleCourseNotFoundByIdException(CourseNotFoundException exception) {
        String message = exception.getMessage();
        log.error(message);
        return ResponseEntity.status(HttpStatus.NOT_FOUND).body(message);
    }
}