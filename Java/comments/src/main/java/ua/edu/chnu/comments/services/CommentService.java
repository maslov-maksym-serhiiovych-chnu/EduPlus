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

    public List<Comment> readAll() {
        return repository.findAll();
    }

    public Comment read(int id) {
        return repository.findById(id).orElseThrow(() -> new CommentNotFoundByIdException(id));
    }

    public void update(int id, Comment comment) {
        Comment updated = read(id);

        updated.setAuthor(comment.getAuthor());
        updated.setContent(comment.getContent());

        repository.save(updated);
    }

    public void delete(int id) {
        repository.delete(read(id));
    }
}
