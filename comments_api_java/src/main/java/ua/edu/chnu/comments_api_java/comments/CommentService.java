package ua.edu.chnu.comments_api_java.comments;

import lombok.RequiredArgsConstructor;
import org.springframework.stereotype.Service;

import java.util.List;

@RequiredArgsConstructor
@Service
public class CommentService {
    private final CommentRepository repository;

    public Comment create(Comment comment) {
        return repository.save(comment);
    }

    public List<Comment> readAll() {
        return repository.findAll();
    }

    public Comment read(int id) {
        return repository.findById(id).orElseThrow(() -> new CommentNotFoundException(id));
    }

    public void update(int id, Comment comment) {
        Comment updated = read(id);
        updated.setAuthor(comment.getAuthor());
        updated.setContent(comment.getContent());
        repository.save(updated);
    }

    public void delete(int id) {
        Comment comment = read(id);
        repository.delete(comment);
    }
}