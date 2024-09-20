package ua.edu.chnu.comments.handlers;

import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.ControllerAdvice;
import org.springframework.web.bind.annotation.ExceptionHandler;
import ua.edu.chnu.comments.exceptions.CommentNotFoundByIdException;

import java.util.HashMap;
import java.util.Map;

@ControllerAdvice
public class CommentExceptionHandler {
    @ExceptionHandler(CommentNotFoundByIdException.class)
    public ResponseEntity<Map<String, String>> handleCommentNotFoundByIdException(CommentNotFoundByIdException exception) {
        var errorResponse = new HashMap<String, String>();
        errorResponse.put("error", exception.getMessage());
        
        return new ResponseEntity<>(errorResponse, HttpStatus.NOT_FOUND);
    }
}
