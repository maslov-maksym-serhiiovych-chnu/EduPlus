package ua.edu.chnu.courses.controllers;

import lombok.RequiredArgsConstructor;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;
import ua.edu.chnu.courses.models.Course;
import ua.edu.chnu.courses.services.CourseService;

import java.util.List;

@RequiredArgsConstructor
@RestController
@RequestMapping("/api/courses")
public class CourseController {
    private final CourseService service;

    @GetMapping
    public ResponseEntity<List<Course>> readAll() {
        var courses = service.readAll();
        return ResponseEntity.ok(courses);
    }

    @GetMapping("{id}")
    public ResponseEntity<Course> read(@PathVariable int id) {
        Course course = service.read(id);
        return ResponseEntity.ok(course);
    }

    @PostMapping
    public ResponseEntity<Course> create(@RequestBody Course course) {
        Course created = service.create(course);
        return ResponseEntity.status(HttpStatus.CREATED).body(created);
    }

    @PutMapping("{id}")
    public ResponseEntity<Course> update(@PathVariable int id, @RequestBody Course course) {
        service.update(id, course);

        return ResponseEntity.noContent().build();
    }

    @DeleteMapping("{id}")
    public ResponseEntity<Course> delete(@PathVariable int id) {
        service.delete(id);

        return ResponseEntity.noContent().build();
    }
}
