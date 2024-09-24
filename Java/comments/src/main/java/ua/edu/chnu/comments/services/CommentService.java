package ua.edu.chnu.comments.services;

import lombok.RequiredArgsConstructor;
import org.springframework.stereotype.Service;
import ua.edu.chnu.comments.exceptions.CommentNotFoundByIdException;
import ua.edu.chnu.comments.models.Comment;
import ua.edu.chnu.comments.repositories.CommentRepository;

import java.util.List;

@RequiredArgsConstructor
@Service
public class CommentService {
    private final CommentRepository repository;

    public Comment create(Comment created) {
        return repository.save(created);
    }

    public List<Comment> getAll() {
        return repository.findAll();
    }

    public Comment get(int id) {
        return repository.findById(id).orElseThrow(() -> new CommentNotFoundByIdException(id));
    }

    public Comment update(int id, Comment comment) {
        Comment updated = get(id);

        updated.setAuthor(comment.getAuthor());
        updated.setContent(comment.getContent());

        return repository.save(updated);
    }

    public Comment delete(int id) {
        Comment deleted = get(id);

        repository.delete(deleted);
        return deleted;
    }
}
