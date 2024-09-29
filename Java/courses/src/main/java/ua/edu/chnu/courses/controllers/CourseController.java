package ua.edu.chnu.courses.controllers;


import lombok.RequiredArgsConstructor;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;
import ua.edu.chnu.courses.dtos.CourseDTO;
import ua.edu.chnu.courses.models.Course;
import ua.edu.chnu.courses.services.CourseService;

import java.util.List;

@RequiredArgsConstructor
@RestController
@RequestMapping("/api/courses")
public class CourseController {
    private final CourseService service;

    @GetMapping
    public ResponseEntity<List<CourseDTO>> readAll() {
        var courseDTOs = service.readAll()
                .stream()
                .map(CourseController::toDTO)
                .toList();
        return ResponseEntity.ok(courseDTOs);
    }

    @GetMapping("{id}")
    public ResponseEntity<CourseDTO> read(@PathVariable int id) {
        Course course = service.read(id);

        CourseDTO courseDTO = toDTO(course);
        return ResponseEntity.ok(courseDTO);
    }

    @PostMapping
    public ResponseEntity<CourseDTO> create(@RequestBody CourseDTO courseDTO) {
        Course course = toModel(courseDTO);

        Course created = service.create(course);

        CourseDTO createdDTO = toDTO(created);
        return ResponseEntity.status(HttpStatus.CREATED).body(createdDTO);
    }

    @PutMapping("{id}")
    public ResponseEntity<CourseDTO> update(@PathVariable int id, @RequestBody CourseDTO courseDTO) {
        Course course = toModel(courseDTO);

        service.update(id, course);

        return ResponseEntity.noContent().build();
    }

    @DeleteMapping("{id}")
    public ResponseEntity<CourseDTO> delete(@PathVariable int id) {
        service.delete(id);

        return ResponseEntity.noContent().build();
    }

    public static Course toModel(CourseDTO courseDTO) {
        if (courseDTO == null) {
            return null;
        }

        Course course = new Course();
        course.setName(courseDTO.getName());
        course.setDescription(courseDTO.getDescription());

        return course;
    }

    public static CourseDTO toDTO(Course course) {
        if (course == null) {
            return null;
        }

        CourseDTO courseDTO = new CourseDTO();
        courseDTO.setName(course.getName());
        courseDTO.setDescription(course.getDescription());

        return courseDTO;
    }
}
