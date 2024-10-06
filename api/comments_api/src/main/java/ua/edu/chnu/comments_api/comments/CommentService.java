package ua.edu.chnu.comments_api.comments;

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

    public Comment read(String id) {
        return repository.findById(id).orElseThrow(() -> new CommentNotFoundException("comment not found by id: " + id));
    }

    public void update(String id, Comment comment) {
        Comment updated = read(id);
        updated.setContent(comment.getContent());
        updated.setCourseId(comment.getCourseId());
        repository.save(updated);
    }

    public void delete(String id) {
        Comment comment = read(id);
        repository.delete(comment);
    }
}