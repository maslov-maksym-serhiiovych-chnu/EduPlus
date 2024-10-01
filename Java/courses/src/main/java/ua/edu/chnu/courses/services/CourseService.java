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

    public List<Course> readAll() {
        return repository.findAll();
    }

    public Course read(int id) {
        return repository.findById(id).orElseThrow(() -> new CourseNotFoundByIdException(id));
    }

    public void update(int id, Course course) {
        Course updated = read(id);

        updated.setName(course.getName());
        updated.setDescription(course.getDescription());

        repository.save(updated);
    }

    public void delete(int id) {
        repository.delete(read(id));
    }
}
