package ua.edu.chnu.courses_api.courses;

import lombok.RequiredArgsConstructor;
import org.springframework.stereotype.Service;

import java.util.List;

@RequiredArgsConstructor
@Service
public class CourseService {
    private final CourseRepository repository;
    
    public Course create(Course course) {
        return repository.save(course);
    }
    
    public List<Course> readAll() {
        return repository.findAll();
    }
    
    public Course read(int id) {
        return repository.findById(id).orElseThrow(() -> new CourseNotFoundException("course not found by id: " + id));
    }
    
    public void update(int id, Course course) {
        Course updated = read(id);
        updated.setName(course.getName());
        updated.setDescription(course.getDescription());
        repository.save(updated);
    }
    
    public void delete(int id) {
        Course course = read(id);
        repository.delete(course);
    }
}