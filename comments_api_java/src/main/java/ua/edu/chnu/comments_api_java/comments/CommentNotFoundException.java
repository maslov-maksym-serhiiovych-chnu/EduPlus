package ua.edu.chnu.comments_api_java.comments;

public class CommentNotFoundException extends RuntimeException {
    public CommentNotFoundException(int id) {
        super("comment not found by id: " + id);
    }
}