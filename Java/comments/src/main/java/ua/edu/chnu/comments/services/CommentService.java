package ua.edu.chnu.comments.services;

import lombok.RequiredArgsConstructor;
import org.springframework.stereotype.Service;
import ua.edu.chnu.comments.exceptions.CommentNotFoundByIdException;
import ua.edu.chnu.comments.models.Comment;
import ua.edu.chnu.comments.repositories.CommentRepository;

@Service
@RequiredArgsConstructor
public class CommentService {
    private final CommentRepository repository;
    
    public void create(Comment comment) {
        repository.save(comment);
    }
    
    public Iterable<Comment> getAll() {
        return repository.findAll();
    }
    
    public Comment get(int id) {
        return repository.findById(id).orElse(null);
    }
    
    public void update(int id, Comment comment) {
        if (!repository.existsById(id)) {
            throw new CommentNotFoundByIdException(id);
        }
        
        Comment updated = new Comment(id, comment.getAuthor(), comment.getContent());
        repository.save(updated);
    }
    
    public void delete(int id) {
        Comment comment = get(id);
        if (comment == null) {
            throw new CommentNotFoundByIdException(id);
        }
        
        repository.delete(comment);
    }
}
