package ua.edu.chnu.comments.controllers;

import lombok.AllArgsConstructor;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;
import ua.edu.chnu.comments.dtos.CommentDTO;
import ua.edu.chnu.comments.services.CommentService;

import java.util.List;

@AllArgsConstructor
@RestController
@RequestMapping("/api/comments")
public class CommentController {
    private final CommentService service;

    @GetMapping
    public ResponseEntity<List<CommentDTO>> getAll() {
        var comments = service.getAll();
        return ResponseEntity.ok(comments);
    }

    @GetMapping("{id}")
    public ResponseEntity<CommentDTO> get(@PathVariable int id) {
        CommentDTO comment = service.get(id);
        return ResponseEntity.ok(comment);
    }

    @PostMapping
    public ResponseEntity<CommentDTO> create(@RequestBody CommentDTO comment) {
        CommentDTO saved = service.create(comment);
        return ResponseEntity.status(HttpStatus.CREATED).body(saved);
    }

    @PutMapping("{id}")
    public ResponseEntity<Void> update(@PathVariable int id, @RequestBody CommentDTO comment) {
        service.update(id, comment);
        return ResponseEntity.noContent().build();
    }

    @DeleteMapping("{id}")
    public ResponseEntity<Void> delete(@PathVariable int id) {
        service.delete(id);
        return ResponseEntity.noContent().build();
    }
}
