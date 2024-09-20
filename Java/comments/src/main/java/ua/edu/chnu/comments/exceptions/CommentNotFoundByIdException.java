package ua.edu.chnu.comments.exceptions;

public class CommentNotFoundByIdException extends RuntimeException {
    public CommentNotFoundByIdException(int id) {
        super("comment not found with id " + id);
    }
}
