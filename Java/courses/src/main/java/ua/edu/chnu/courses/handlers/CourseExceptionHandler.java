package ua.edu.chnu.courses.handlers;

import lombok.extern.slf4j.Slf4j;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.ControllerAdvice;
import org.springframework.web.bind.annotation.ExceptionHandler;
import ua.edu.chnu.courses.exceptions.CourseNotFoundByIdException;

@Slf4j
@ControllerAdvice
public class CourseExceptionHandler {
    @ExceptionHandler(CourseNotFoundByIdException.class)
    public ResponseEntity<String> handleCourseNotFoundByIdException(CourseNotFoundByIdException exception) {
        log.error(exception.getMessage());

        return ResponseEntity.status(HttpStatus.NOT_FOUND).body(exception.getMessage());
    }
}
