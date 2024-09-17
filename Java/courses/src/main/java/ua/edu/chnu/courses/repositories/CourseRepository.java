package ua.edu.chnu.courses.repositories;

import org.springframework.data.repository.CrudRepository;
import org.springframework.stereotype.Repository;
import ua.edu.chnu.courses.models.Course;

@Repository
public interface CourseRepository extends CrudRepository<Course, Integer> {
}
