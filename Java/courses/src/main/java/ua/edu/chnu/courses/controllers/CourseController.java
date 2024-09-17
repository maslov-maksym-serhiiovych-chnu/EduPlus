package ua.edu.chnu.courses.controllers;

import lombok.RequiredArgsConstructor;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;
import ua.edu.chnu.courses.models.Course;
import ua.edu.chnu.courses.repositories.CourseRepository;

import java.util.Optional;

@RestController
@RequestMapping("/api/courses")
@RequiredArgsConstructor
public class CourseController {
    private final CourseRepository repository;

    @GetMapping
    public ResponseEntity<Iterable<Course>> getAll() {
        var courses = repository.findAll();
        return ResponseEntity.ok(courses);
    }

    @GetMapping("{id}")
    public ResponseEntity<Course> get(@PathVariable int id) {
        Optional<Course> course = repository.findById(id);
        return course.map(ResponseEntity::ok).orElseGet(() -> ResponseEntity.notFound().build());
    }

    @PostMapping
    public ResponseEntity<Course> create(@RequestBody Course course) {
        Course saved = repository.save(course);
        return ResponseEntity.status(HttpStatus.CREATED).body(saved);
    }

    @PutMapping("{id}")
    public ResponseEntity<Void> update(@PathVariable int id, @RequestBody Course course) {
        if (!repository.existsById(id)) {
            return ResponseEntity.notFound().build();
        }

        Course updated = new Course(id, course.getName(), course.getDescription());
        repository.save(updated);

        return ResponseEntity.noContent().build();
    }

    @DeleteMapping("{id}")
    public ResponseEntity<Void> delete(@PathVariable int id) {
        Course course = repository.findById(id).orElse(null);
        if (course == null) {
            return ResponseEntity.notFound().build();
        }

        repository.delete(course);

        return ResponseEntity.noContent().build();
    }
}
