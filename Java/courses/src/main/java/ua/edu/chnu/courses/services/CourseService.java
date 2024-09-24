package ua.edu.chnu.courses.services;

import lombok.RequiredArgsConstructor;
import org.springframework.stereotype.Service;
import ua.edu.chnu.courses.exceptions.CourseNotFoundByIdException;
import ua.edu.chnu.courses.models.Course;
import ua.edu.chnu.courses.repositories.CourseRepository;

import java.util.List;

@RequiredArgsConstructor
@Service
public class CourseService {
    private final CourseRepository repository;

    public Course create(Course created) {
        return repository.save(created);
    }

    public List<Course> getAll() {
        return repository.findAll();
    }

    public Course get(int id) {
        return repository.findById(id).orElseThrow(() -> new CourseNotFoundByIdException(id));
    }

    public Course update(int id, Course course) {
        Course updated = get(id);

        updated.setName(course.getName());
        updated.setDescription(course.getDescription());

        return repository.save(updated);
    }

    public Course delete(int id) {
        Course deleted = get(id);

        repository.delete(deleted);
        return deleted;
    }
}
