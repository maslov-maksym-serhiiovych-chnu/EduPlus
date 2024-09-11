package ua.edu.chnu.comments.controllers;

import lombok.RequiredArgsConstructor;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;
import ua.edu.chnu.comments.models.Comment;
import ua.edu.chnu.comments.repositories.CommentRepository;

import java.util.Optional;

@RestController
@RequestMapping("/api/comments")
@RequiredArgsConstructor
public class CommentController {
    private final CommentRepository repository;

    @GetMapping
    public ResponseEntity<Iterable<Comment>> getAll() {
        var comments = repository.findAll();
        return ResponseEntity.ok(comments);
    }

    @GetMapping("{id}")
    public ResponseEntity<Comment> get(@PathVariable int id) {
        Optional<Comment> comment = repository.findById(id);
        return comment.map(ResponseEntity::ok).orElseGet(() -> ResponseEntity.notFound().build());
    }

    @PostMapping
    public ResponseEntity<Comment> create(@RequestBody Comment comment) {
        Comment saved = repository.save(comment);
        return ResponseEntity.status(HttpStatus.CREATED).body(saved);
    }

    @PutMapping("{id}")
    public ResponseEntity<Void> update(@PathVariable int id, @RequestBody Comment comment) {
        if (!repository.existsById(id)) {
            return ResponseEntity.notFound().build();
        }

        Comment updated = new Comment(id, comment.getAuthor(), comment.getContent());
        repository.save(updated);
        
        return ResponseEntity.noContent().build();
    }

    @DeleteMapping("{id}")
    public ResponseEntity<Void> delete(@PathVariable int id) {
        Comment comment = repository.findById(id).orElse(null);
        if (comment == null) {
            return ResponseEntity.notFound().build();
        }

        repository.delete(comment);
        
        return ResponseEntity.noContent().build();
    }
}
