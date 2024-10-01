package ua.edu.chnu.comments.handlers;

import lombok.extern.slf4j.Slf4j;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.ControllerAdvice;
import org.springframework.web.bind.annotation.ExceptionHandler;
import ua.edu.chnu.comments.exceptions.CommentNotFoundByIdException;

@Slf4j
@ControllerAdvice
public class CommentExceptionHandler {
    @ExceptionHandler(CommentNotFoundByIdException.class)
    public ResponseEntity<String> handleCommentNotFoundByIdException(CommentNotFoundByIdException exception) {
        log.error(exception.getMessage());
        
        return ResponseEntity.status(HttpStatus.NOT_FOUND).body(exception.getMessage());
    }
}
