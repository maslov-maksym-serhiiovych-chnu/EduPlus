package ua.edu.chnu.comments_api.comments;

import lombok.RequiredArgsConstructor;
import org.springframework.stereotype.Service;
import ua.edu.chnu.comments_api.courses.CourseClient;

import java.util.List;

@RequiredArgsConstructor
@Service
public class CommentService {
    private final CommentRepository repository;
    private final CourseClient client;

    public Comment create(Comment comment) {
        client.read(comment.getCourseId());

        return repository.save(comment);
    }

    public List<Comment> readAll() {
        return repository.findAll();
    }

    public Comment read(String id) {
        return repository.findById(id).orElseThrow(() -> new CommentNotFoundException("comment not found by id: " + id));
    }

    public void update(String id, Comment comment) {
        int courseId = comment.getCourseId();
        client.read(courseId);

        Comment updated = read(id);
        updated.setContent(comment.getContent());
        updated.setCourseId(courseId);
        repository.save(updated);
    }

    public void delete(String id) {
        Comment comment = read(id);
        repository.delete(comment);
    }
}