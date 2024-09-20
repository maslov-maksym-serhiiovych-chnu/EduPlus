package ua.edu.chnu.comments.controllers;

import lombok.RequiredArgsConstructor;
import lombok.extern.slf4j.Slf4j;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;
import ua.edu.chnu.comments.dtos.CommentDTO;
import ua.edu.chnu.comments.exceptions.CommentNotFoundByIdException;
import ua.edu.chnu.comments.services.CommentService;

import java.util.List;

@Slf4j
@RestController
@RequestMapping("/api/comments")
@RequiredArgsConstructor
public class CommentController {
    private final CommentService service;

    @GetMapping
    public ResponseEntity<List<CommentDTO>> getAll() {
        var comments = service.getAll();
        return ResponseEntity.ok(comments);
    }

    @GetMapping("{id}")
    public ResponseEntity<CommentDTO> get(@PathVariable int id) {
        try {
            CommentDTO comment = service.get(id);
            return ResponseEntity.ok(comment);
        } catch (CommentNotFoundByIdException exception) {
            return ResponseEntity.notFound().build();
        }
    }

    @PostMapping
    public ResponseEntity<CommentDTO> create(@RequestBody CommentDTO comment) {
        CommentDTO saved = service.create(comment);
        return ResponseEntity.status(HttpStatus.CREATED).body(saved);
    }

    @PutMapping("{id}")
    public ResponseEntity<Void> update(@PathVariable int id, @RequestBody CommentDTO comment) {
        try {
            service.update(id, comment);
            return ResponseEntity.noContent().build();
        } catch (CommentNotFoundByIdException exception) {
            return ResponseEntity.notFound().build();
        }
    }

    @DeleteMapping("{id}")
    public ResponseEntity<Void> delete(@PathVariable int id) {
        try {
            service.delete(id);
            return ResponseEntity.noContent().build();
        } catch (CommentNotFoundByIdException exception) {
            return ResponseEntity.notFound().build();
        }
    }
}
