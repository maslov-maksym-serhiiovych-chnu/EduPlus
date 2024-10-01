package ua.edu.chnu.comments.controllers;

import lombok.RequiredArgsConstructor;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;
import ua.edu.chnu.comments.models.Comment;
import ua.edu.chnu.comments.services.CommentService;

import java.util.List;

@RequiredArgsConstructor
@RestController
@RequestMapping("/api/comments")
public class CommentController {
    private final CommentService service;

    @GetMapping
    public ResponseEntity<List<Comment>> readAll() {
        var comments = service.readAll();
        return ResponseEntity.ok(comments);
    }

    @GetMapping("{id}")
    public ResponseEntity<Comment> read(@PathVariable int id) {
        Comment comment = service.read(id);
        return ResponseEntity.ok(comment);
    }

    @PostMapping
    public ResponseEntity<Comment> create(@RequestBody Comment comment) {
        Comment created = service.create(comment);
        return ResponseEntity.status(HttpStatus.CREATED).body(created);
    }

    @PutMapping("{id}")
    public ResponseEntity<Comment> update(@PathVariable int id, @RequestBody Comment comment) {
        service.update(id, comment);

        return ResponseEntity.noContent().build();
    }

    @DeleteMapping("{id}")
    public ResponseEntity<Comment> delete(@PathVariable int id) {
        service.delete(id);

        return ResponseEntity.noContent().build();
    }
}
