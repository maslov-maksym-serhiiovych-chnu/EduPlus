package ua.edu.chnu.comments_api.comments;

import lombok.RequiredArgsConstructor;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RequiredArgsConstructor
@RestController
@RequestMapping("api/comments")
public class CommentController {
    private final CommentService service;

    @GetMapping
    public ResponseEntity<List<Comment>> readAll() {
        var courses = service.readAll();
        return ResponseEntity.ok(courses);
    }

    @GetMapping("{id}")
    public ResponseEntity<Comment> read(@PathVariable String id) {
        Comment course = service.read(id);
        return ResponseEntity.ok(course);
    }

    @PostMapping
    public ResponseEntity<Comment> create(@RequestBody Comment course) {
        Comment created = service.create(course);
        return ResponseEntity.status(HttpStatus.CREATED).body(created);
    }

    @PutMapping("{id}")
    public ResponseEntity<Void> update(@PathVariable String id, @RequestBody Comment course) {
        service.update(id, course);
        return ResponseEntity.noContent().build();
    }

    @DeleteMapping("{id}")
    public ResponseEntity<Void> delete(@PathVariable String id) {
        service.delete(id);
        return ResponseEntity.noContent().build();
    }
}